﻿using AdOut.Point.Model.Interfaces.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace AdOut.Point.EventBroker.DI
{
    public static class EventBrokerModule
    {
        public static void AddEventBrokerModule(this IServiceCollection services)
        {
            services.AddSingleton<IChannelManager, RabbitChannelManager>();
            services.AddScoped<IEventBroker, RabbitEventBroker>();
            services.AddScoped<IConnectionProvider, RabbitConnectionProvider>();
            services.AddScoped<IEventBrokerHelper, EventBrokerHelper>();
            services.AddScoped<IEventBinder, EventBinder>();
        }
    }
}
