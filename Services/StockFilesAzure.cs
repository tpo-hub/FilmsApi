using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsApi.Services
{
    public class StockFilesAzure : IStockerFile
    {

        private readonly string ConnectionString;
        public StockFilesAzure(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("AzureStorange");
        }
        public async Task DeleteFile(string route, string container)
        {
            if(string.IsNullOrEmpty(route))
            {
                return;
            }

            var client = new BlobContainerClient(ConnectionString, container);

            await client.CreateIfNotExistsAsync();

            var file = Path.GetFileName(route);
            var blob = client.GetBlobClient(file);
            await blob.DeleteAsync();
        }

        public async Task<string> EditFile(byte[] content, string route,
            string extension, string container, string contentType)
        {
            await DeleteFile(route, container);
            return await StockFile(content, extension, container, contentType);
        }

        public async Task<string> StockFile(byte[] content, string extension, string container, string contentType)
        {
            var client = new BlobContainerClient(ConnectionString, container );
           
            await client.CreateIfNotExistsAsync();

            client.SetAccessPolicy(PublicAccessType.Blob);

            var FileName = $"{Guid.NewGuid()}{extension}";
            var blob = client.GetBlobClient(FileName);

            var blobUploadOptions = new BlobUploadOptions();
            var blobHttpHeader = new BlobHttpHeaders();
            
            blobHttpHeader.ContentType = contentType;
            blobUploadOptions.HttpHeaders = blobHttpHeader;

            await blob.UploadAsync(new BinaryData(content), blobUploadOptions);

            return blob.Uri.ToString();
        }
    }
}
