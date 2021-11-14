using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsApi.Services
{
    public class StockFilesLocal : IStockerFile
    {
        private readonly IWebHostEnvironment env;
        private readonly IHttpContextAccessor accessor;

        public StockFilesLocal(IWebHostEnvironment env, IHttpContextAccessor accessor)
        {
            this.env = env;
            this.accessor = accessor;
        }

        public  Task DeleteFile(string route, string container)
        {
            if(route != null)
            {
                var FileName = Path.GetFileName(route);
                string directory = Path.Combine(env.WebRootPath, container, FileName);

                if(File.Exists(directory))
                {
                    File.Delete(directory);
                }
            }
            
            return Task.FromResult(0);
        }

        public async Task<string> EditFile(byte[] content, string route, string extension, string container, string contentType)
        {
            await DeleteFile(route, container);
            return await StockFile(content, extension, container, contentType);
        }

        public async Task<string> StockFile(byte[] content, string extension, string container, string contentType)
        {
            var FileName = $"{Guid.NewGuid()}{extension}";
            string folder = Path.Combine(env.WebRootPath, container);

            if(!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string route = Path.Combine(folder, FileName);

            await File.WriteAllBytesAsync(route, content);

            var urlActual = $"{accessor.HttpContext.Request.Scheme}://{accessor.HttpContext.Request.Host}";
            var urlToDB = Path.Combine(urlActual, container, FileName).Replace("\\","/");

            return urlToDB;

        }
    }
}
