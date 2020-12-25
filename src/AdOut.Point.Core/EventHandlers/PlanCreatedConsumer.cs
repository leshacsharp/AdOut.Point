using AdOut.Extensions.Communication;
using AdOut.Extensions.Context;
using AdOut.Point.Model.Database;
using AdOut.Point.Model.Events;
using AdOut.Point.Model.Interfaces.Repositories;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace AdOut.Point.Core.EventHandlers
{
    public class PlanCreatedConsumer : BaseConsumer<PlanCreatedEvent>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMapper _mapper;

        public PlanCreatedConsumer(
            IServiceScopeFactory serviceScopeFactory,
            IMapper mapper)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _mapper = mapper;
        }

        protected override Task HandleAsync(PlanCreatedEvent deliveredEvent)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var planRepository = scope.ServiceProvider.GetRequiredService<IPlanRepository>();
            var commitProvider = scope.ServiceProvider.GetRequiredService<ICommitProvider>();

            var plan = _mapper.Map<Plan>(deliveredEvent);
            planRepository.Create(plan);

            return commitProvider.SaveChangesAsync();
        }
    }
}
