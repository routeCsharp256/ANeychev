using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OzonEdu.Infrastructure.Filters;
using OzonEdu.Infrastructure.Interceptors;
using OzonEdu.Infrastructure.StartupFilters;

namespace OzonEdu.Infrastructure.Extensions
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder AddInfrastructure(this IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IStartupFilter, LiveStartupFilter>();
                services.AddSingleton<IStartupFilter, ReadyStartupFilter>();
                services.AddSingleton<IStartupFilter, VersionStartupFilter>();
                services.AddSingleton<IStartupFilter, RequestLoggingStartupFilter>();

                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1",
                        new OpenApiInfo
                            {Title = Assembly.GetEntryAssembly()?.GetName().Name ?? "no name", Version = "v1"});
                    options.CustomSchemaIds(x => x.FullName);
                });

                services.AddControllers(options => options.Filters.Add<GlobalExceptionFilter>());

                services.AddGrpc(options => options.Interceptors.Add<LoggingInterceptor>());
            });
            return builder;
        }
    }
}