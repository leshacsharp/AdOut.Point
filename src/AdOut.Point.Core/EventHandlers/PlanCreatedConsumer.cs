using AdOut.Point.Core.EventHandlers.Base;
using AdOut.Point.Model.Database;
using AdOut.Point.Model.Events;
using AdOut.Point.Model.Interfaces.Context;
using AdOut.Point.Model.Interfaces.Repositories;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace AdOut.Point.Core.EventHandlers
{
    public class PlanCreatedConsumer : BaseConsumer<PlanCreatedEvent>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public PlanCreatedConsumer(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override Task HandleAsync(PlanCreatedEvent deliveredEvent)
        {
            //todo: delete singletone services
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var planRepository = scope.ServiceProvider.GetRequiredService<IPlanRepository>();
                var commitProvider = scope.ServiceProvider.GetRequiredService<ICommitProvider>();

                var mapperCfg = new MapperConfiguration(cfg => cfg.CreateMap(deliveredEvent.GetType(), typeof(Plan)));
                var mapper = new Mapper(mapperCfg);

                var plan = mapper.Map<Plan>(deliveredEvent);
                planRepository.Create(plan);

                return commitProvider.SaveChangesAsync();
            }
        }
    }
}
