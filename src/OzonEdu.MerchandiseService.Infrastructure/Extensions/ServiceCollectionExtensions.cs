using System.Reflection;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchandiseService.HttpClients.EmployeeService.Interfaces;
using OzonEdu.MerchandiseService.HttpClients.StockApiService.Interfaces;
using OzonEdu.MerchandiseService.HttpClients.Stubs;
using OzonEdu.MerchandiseService.Infrastructure.Jobs;
using OzonEdu.MerchandiseService.Infrastructure.Stubs;
using OzonEdu.MerchandiseService.Infrastructure.Workers;
using OzonEdu.MerchandiseService.Infrastructure.Workers.Interfaces;
using Quartz;

namespace OzonEdu.MerchandiseService.Infrastructure.Extensions
{
    /// <summary>
    /// Класс расширений для типа <see cref="IServiceCollection"/> для регистрации инфраструктурных сервисов
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавление в DI контейнер инфраструктурных сервисов
        /// </summary>
        /// <param name="services">Объект IServiceCollection</param>
        /// <param name="configuration"></param>
        /// <returns>Объект <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<IMerchRequestWorker, MerchRequestWorker>();
            services.AddTransient<MerchRequestJob>();
            services.AddQuartz(options =>
            {
                options.UseMicrosoftDependencyInjectionJobFactory();
                options.ScheduleJob<MerchRequestJob>(trigger => trigger.StartNow()
                    .WithIdentity(nameof(MerchRequestJob))
                    .WithCronSchedule(configuration.GetSection("Scheduler").GetSection("CronStringForJob").Value));
#if DEBUG
                options.AddJobListener<MerchRequestJobListener>();
#endif
            });

            services.AddQuartzServer(options => options.WaitForJobsToComplete = true);
            
            // === Stubs
            services.AddScoped<IEmployeeHttpClient, EmployeeHttpClientStub>();
            services.AddScoped<IStockApiHttpClient, StockApiHttpClientStub>();

            return services;
        }

        /// <summary>
        /// Добавление в DI контейнер инфраструктурных репозиториев
        /// </summary>
        /// <param name="services">Объект IServiceCollection</param>
        /// <returns>Объект <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddInfrastructureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IMerchPackRepository, MerchPackRepository>();
            services.AddScoped<IMerchRequestRepository, MerchRequestRepository>();

            return services;
        }
    }
}