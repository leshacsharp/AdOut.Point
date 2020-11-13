using AdOut.Point.Core.Content;
using AdOut.Point.Core.EventHandlers;
using AdOut.Point.Core.Managers;
using AdOut.Point.Model.Interfaces.Content;
using AdOut.Point.Model.Interfaces.Managers;
using AdOut.Point.Model.Settings;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace AdOut.Point.Core.DI
{
    public static class CoreModule
    {
        public static void AddCoreModule(this IServiceCollection services, IConfiguration configuration)
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

            //todo: take an automatic process of registration consumers from the AdOut.Planning
            //todo: need to make these services scoped
            services.AddSingleton<IBasicConsumer, PlanCreatedConsumer>();
            services.AddSingleton<IBasicConsumer, PlanAdPointCreatedConsumer>();
        }
    }
}
