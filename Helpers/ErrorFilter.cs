using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsApi.Helpers
{
    public class ErrorFilter: ExceptionFilterAttribute
    {
        private readonly ILogger<ErrorFilter> logger;

        public ErrorFilter(ILogger<ErrorFilter> logger)
        {
            this.logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            logger.LogError(context.Exception, context.Exception.Message);


            base.OnException(context);
        }

    }
}
