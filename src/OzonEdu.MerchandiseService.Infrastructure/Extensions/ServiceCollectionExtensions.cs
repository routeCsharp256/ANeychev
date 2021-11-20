using System.Reflection;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchandiseService.Domain.Contracts;
using OzonEdu.MerchandiseService.HttpClients.EmployeeService.Interfaces;
using OzonEdu.MerchandiseService.HttpClients.StockApiService.Interfaces;
using OzonEdu.MerchandiseService.HttpClients.Stubs;
using OzonEdu.MerchandiseService.Infrastructure.Jobs;
using OzonEdu.MerchandiseService.Infrastructure.Repositories.Implementation;
using OzonEdu.MerchandiseService.Infrastructure.Repositories.Infrastructure;
using OzonEdu.MerchandiseService.Infrastructure.Repositories.Infrastructure.Interfaces;
using OzonEdu.MerchandiseService.Infrastructure.Services;
using OzonEdu.MerchandiseService.Infrastructure.Services.Interfaces;
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

            services.AddScoped<IApplicationService, ApplicationService>();

            // === Stubs
            services.AddScoped<IEmployeeHttpClient, EmployeeHttpClientStub>();
            services.AddScoped<IStockApiHttpClient, StockApiHttpClientStub>();

            // === Repo

            return services;
        }

        /// <summary>
        /// Подключение БД
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDatabaseComponents(this IServiceCollection services)
        {
            
            services.AddScoped<IDbConnectionFactory<NpgsqlConnection>, NpgsqlConnectionFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IChangeTracker, ChangeTracker>();

            return services;
        }

        /// <summary>
        /// Добавление в DI контейнер инфраструктурных репозиториев
        /// </summary>
        /// <param name="services">Объект IServiceCollection</param>
        /// <returns>Объект <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddInfrastructureRepositories(this IServiceCollection services)
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IMerchPackRepository, MerchPackRepository>();
            services.AddScoped<IMerchRequestRepository, MerchRequestRepository>();

            return services;
        }
    }
}