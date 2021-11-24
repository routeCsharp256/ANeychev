using System;
using System.IO;
using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
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
                services.AddSingleton<IStartupFilter, SwaggerStartupFilter>();

                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1",
                        new OpenApiInfo
                            {Title = Assembly.GetEntryAssembly()?.GetName().Name, Version = "v1"});
                    options.CustomSchemaIds(x => x.FullName);
                    var xmlFileName = Assembly.GetEntryAssembly()?.GetName().Name + ".xml";
                    var xmlFilePath =
                        Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ?? string.Empty,
                            xmlFileName);
                    options.IncludeXmlComments(xmlFilePath);
                });

                services.AddControllers(options => options.Filters.Add<GlobalExceptionFilter>());

                services.AddGrpc(options =>
                {
                    options.Interceptors.Add<ExceptionInterceptor>();
                    options.Interceptors.Add<LoggingInterceptor>();
                });
            });
            return builder;
        }
        
        public static IHostBuilder ConfigurePorts(this IHostBuilder builder)
        {
            var httpPortEnv = Environment.GetEnvironmentVariable("HTTP_PORT");
            if (!int.TryParse(httpPortEnv, out var httpPort))
            {
                httpPort = 5000;
            }

            var grpcPortEnv = Environment.GetEnvironmentVariable("GRPC_PORT");
            if (!int.TryParse(grpcPortEnv, out var grpcPort))
            {
                grpcPort = 5002;
            }
            builder.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureKestrel(
                    options =>
                    {
                        Listen(options, httpPort, HttpProtocols.Http1);
                        Listen(options, grpcPort, HttpProtocols.Http2);
                    });
            });
            return builder;
        }

        static void Listen(KestrelServerOptions kestrelServerOptions, int? port, HttpProtocols protocols)
        {
            if (port == null)
                return;

            var address = IPAddress.Any;


            kestrelServerOptions.Listen(address, port.Value, listenOptions => { listenOptions.Protocols = protocols; });
        }
    }
}