using System.Linq;
using AdOut.Point.Core.DI;
using AdOut.Point.DataProvider.Context;
using AdOut.Point.DataProvider.DI;
using AdOut.Point.EventBroker.DI;
using AdOut.Point.Model;
using AdOut.Point.Model.Events;
using AdOut.Point.Model.Interfaces.Infrastructure;
using AdOut.Point.Model.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace AdOut.Point.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<AdPointContext>(options => 
                     options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));

            services.AddDataProviderModule();
            services.AddCoreModule();
            services.AddEventBrokerModule();

            services.Configure<RabbitConnection>(Configuration.GetSection(nameof(RabbitConnection)));

            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new OpenApiInfo { Title = "AdOut.Point API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IEventBroker eventBroker)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(setup =>
            {
                setup.SwaggerEndpoint("/swagger/v1/swagger.json", "AdOut.Point API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var modelAssembly = typeof(Constants).Assembly;
            var eventTypes = modelAssembly.GetTypes().Where(t => t.BaseType == typeof(IntegrationEvent));
            eventBroker.Configure(eventTypes);
        }
    }
}
