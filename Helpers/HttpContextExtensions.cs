using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsApi.Helpers
{
    public static class HttpContextExtensions
    {
        public async static Task InsertPaginationParams<T>(this HttpContext httpContext, 
            IQueryable<T> queryable, int countRegisters)
        {
            double count = await queryable.CountAsync();
            double countPages = Math.Ceiling(count / countRegisters);

            httpContext.Response.Headers.Add("countPages", countPages.ToString());

        }

    }
}
