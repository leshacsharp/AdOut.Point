using AdOut.Extensions.Communication;
using AdOut.Extensions.Communication.Interfaces;
using AdOut.Extensions.Context;
using AdOut.Point.Core.Managers;
using AdOut.Point.Core.Replication;
using AdOut.Point.Core.Services;
using AdOut.Point.DataProvider.Context;
using AdOut.Point.DataProvider.Repositories;
using AdOut.Point.Model.Database;
using AdOut.Point.Model.Interfaces.Context;
using AdOut.Point.Model.Interfaces.Managers;
using AdOut.Point.Model.Interfaces.Repositories;
using AdOut.Point.Model.Interfaces.Services;
using AdOut.Point.Model.Settings;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace AdOut.Point.WebApi.Configuration
{
    public static class StartupConfiguration
    {
        public static void AddDataProviderServices(this IServiceCollection services)
        {
            services.AddScoped<IDatabaseContext>(p => p.GetRequiredService<AdPointContext>());
            services.AddScoped<ICommitProvider, CommitProvider<AdPointContext>>();

            services.AddScoped<IAdPointRepository, AdPointRepository>();
            services.AddScoped<ITariffRepository, TariffRepository>();
            services.AddScoped<IAdPointDayOffRepository, AdPointDayOffRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IDayOffRepository, DayOffRepository>();
            services.AddScoped<IPlanRepository, PlanRepository>();
            services.AddScoped<IPlanAdPointRepository, PlanAdPointRepository>();
        }

        public static void AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAdPointManager, AdPointManager>();
            services.AddScoped<ITariffManager, TariffManager>();
            services.AddScoped<IImageManager, ImageManager>();
            services.AddScoped<IAdPointDayOffManager, AdPointDayOffManager>();

            var awsConfig = new AWSS3Config();
            configuration.Bind(nameof(AWSS3Config), awsConfig);
            var awsCredentials = new BasicAWSCredentials(awsConfig.AccessKey, awsConfig.SecretKey);
            var regionEndpoint = RegionEndpoint.GetBySystemName(awsConfig.RegionEndpointName);
            var awsClient = new AmazonS3Client(awsCredentials, regionEndpoint);

            services.AddScoped<IContentStorage>(p => new AWSS3Storage(awsClient, awsConfig.BucketName));

            services.AddScoped<IReplicationHandlerFactory<Plan>, PlanReplicationFactory>();
            services.AddScoped<IReplicationHandlerFactory<PlanAdPoint>, PlanAdPointReplicationFactory>();
            services.AddSingleton<IBasicConsumer, ReplicationConsumer<Plan>>();
            services.AddSingleton<IBasicConsumer, ReplicationConsumer<PlanAdPoint>>();

            services.AddAutoMapper(c =>
            {
                
            });
        }
    }
}
