using AdOut.Extensions.Communication;
using AdOut.Extensions.Communication.Interfaces;
using AdOut.Extensions.Context;
using AdOut.Point.Model.Database;
using AdOut.Point.Model.Interfaces.Repositories;

namespace AdOut.Point.Core.Replication
{
    public class PlanReplicationFactory : IReplicationHandlerFactory<Plan>
    {
        private readonly IPlanRepository _planRepository;
        private readonly ICommitProvider _commitProvider;

        public PlanReplicationFactory(
            IPlanRepository planRepository,
            ICommitProvider commitProvider)
        {
            _planRepository = planRepository;
            _commitProvider = commitProvider;
        }

        public IReplicationHandler<Plan> CreateReplicationHandler(EventAction action)
        {
            return action switch
            {
                EventAction.Created => new PlanCreatedHandler(_planRepository, _commitProvider),
                _ => null
            };
        }
    }
}
