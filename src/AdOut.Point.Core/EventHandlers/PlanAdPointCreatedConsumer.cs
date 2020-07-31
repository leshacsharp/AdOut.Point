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
    public class PlanAdPointCreatedConsumer : BaseConsumer<PlanAdPointCreatedEvent>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public PlanAdPointCreatedConsumer(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override Task HandleAsync(PlanAdPointCreatedEvent deliveredEvent)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var planAdPointRepository = scope.ServiceProvider.GetRequiredService<IPlanAdPointRepository>();
                var commitProvider = scope.ServiceProvider.GetRequiredService<ICommitProvider>();

                var mapperCfg = new MapperConfiguration(cfg => cfg.CreateMap(deliveredEvent.GetType(), typeof(PlanAdPoint)));
                var mapper = new Mapper(mapperCfg);

                var plan = mapper.Map<PlanAdPoint>(deliveredEvent);
                planAdPointRepository.Create(plan);

                return commitProvider.SaveChangesAsync();
            }
        }
    }
}
