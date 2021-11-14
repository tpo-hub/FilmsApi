using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsApi.Helpers
{
    public class MovieExistAttribute : Attribute, IAsyncResourceFilter
    {
        private readonly ApplicationDbContext Dbcontext;

        public MovieExistAttribute(ApplicationDbContext context)
        {
            this.Dbcontext = context;
        }
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            var movieIdObject = context.HttpContext.Request.RouteValues["movieId"];

            if (movieIdObject == null)
            {
                return;
            }

            var movieId = int.Parse(movieIdObject.ToString());

            var movieExist = await Dbcontext.Movies
             .AnyAsync(x => x.Id == movieId);

            if (!movieExist)
            {
                context.Result = new NotFoundResult();
            }
            else
            {
                await next();
            }
        }
    }

}
