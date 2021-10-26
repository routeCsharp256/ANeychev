using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using OzonEdu.Infrastructure.Middlewares;

namespace OzonEdu.Infrastructure.StartupFilters
{
    public class ReadyStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app => { app.Map("/ready", builder => builder.UseMiddleware<ReadyMiddleware>()); };
        }
    }
}