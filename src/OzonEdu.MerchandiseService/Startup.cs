using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using OzonEdu.MerchandiseService.GrpcServices;
using OzonEdu.MerchandiseService.Infrastructure.Extensions;
using OzonEdu.MerchandiseService.Services;
using OzonEdu.MerchandiseService.Services.Interfaces;

namespace OzonEdu.MerchandiseService
{
    public sealed class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMerchService, MerchService>();
            services.AddInfrastructureServices();
            services.AddInfrastructureRepositories();
            services.AddGrpc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/",
                    async context => { await context.Response.WriteAsync("Welcome to merchandise services!!!"); });
                endpoints.MapGrpcService<MerchandiseGrpcService>();
                endpoints.MapControllers();
            });
        }
    }
}