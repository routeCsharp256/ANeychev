using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using OzonEdu.Infrastructure.Middlewares;

namespace OzonEdu.Infrastructure.StartupFilters
{
    public sealed class RequestLoggingStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.UseMiddleware<RequestLoggingMiddleware>();
                next(app);
            };
        }
    }
}