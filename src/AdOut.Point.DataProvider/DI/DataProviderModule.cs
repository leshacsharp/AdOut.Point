using AdOut.Point.DataProvider.Context;
using AdOut.Point.DataProvider.Repositories;
using AdOut.Point.Model.Interfaces.Context;
using AdOut.Point.Model.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AdOut.Point.DataProvider.DI
{
    public static class DataProviderModule
    {
        public static void AddDataProviderModule(this IServiceCollection services)
        {
            services.AddScoped<IDatabaseContext, AdPointContext>();
            services.AddScoped<ICommitProvider, CommitProvider>();

            services.AddScoped<IAdPointRepository, AdPointRepository>();
            services.AddScoped<ITariffRepository, TariffRepository>();
            services.AddScoped<IAdPointDayOffRepository, AdPointDayOffRepository>();
        }
    }
}
