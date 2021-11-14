using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsApi.Services
{
    public interface IStockerFile
    {
        Task<string> StockFile(byte[] content, string extension, string container, string contentType);
        Task<string> EditFile(byte[] content, string route, string extension, string container, string contentType);
        Task DeleteFile(string route, string container);


    }
}
