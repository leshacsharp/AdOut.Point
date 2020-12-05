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
        private readonly IMapper _mapper;

        public PlanAdPointCreatedConsumer(
            IServiceScopeFactory serviceScopeFactory,
            IMapper mapper)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _mapper = mapper;
        }

        protected override Task HandleAsync(PlanAdPointCreatedEvent deliveredEvent)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var planAdPointRepository = scope.ServiceProvider.GetRequiredService<IPlanAdPointRepository>();
            var commitProvider = scope.ServiceProvider.GetRequiredService<ICommitProvider>();

            var planAdPoint = _mapper.Map<PlanAdPoint>(deliveredEvent);
            planAdPointRepository.Create(planAdPoint);

            return commitProvider.SaveChangesAsync();
        }
    }
}
