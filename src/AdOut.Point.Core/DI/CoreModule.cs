using AdOut.Point.Core.Content;
using AdOut.Point.Core.Managers;
using AdOut.Point.Model.Interfaces.Content;
using AdOut.Point.Model.Interfaces.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace AdOut.Point.Core.DI
{
    public static class CoreModule
    {
        public static void AddCoreModule(this IServiceCollection services)
        {
            services.AddScoped<IAdPointManager, AdPointManager>();
            services.AddScoped<ITariffManager, TariffManager>();
            services.AddScoped<IImageManager, ImageManager>();
            services.AddScoped<IAdPointDayOffManager, AdPointDayOffManager>();

            services.AddScoped<IContentStorage, AWSS3Storage>();
        }
    }
}
