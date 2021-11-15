using FilmsApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsApi.Helpers
{
    public static class FactoryEnv
    {
        public static IStockerFile FileStorangeServices(IServiceProvider ServiceProvider)
        {
            var env = ServiceProvider.GetRequiredService<IWebHostEnvironment>();

            if (env.IsDevelopment())
            {
                var httContexAccesor = ServiceProvider.GetRequiredService<IHttpContextAccessor>();
                return new StockFilesLocal(env, httContexAccesor);
            }
            else
            {
                var Configuration = ServiceProvider.GetRequiredServices<IConfiguration>();
                return new StockFilesAzure(Configuration);
            }
        }

    }
}
