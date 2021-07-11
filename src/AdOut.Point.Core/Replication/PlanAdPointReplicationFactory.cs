using AdOut.Extensions.Communication;
using AdOut.Extensions.Communication.Interfaces;
using AdOut.Extensions.Context;
using AdOut.Point.Model.Database;
using AdOut.Point.Model.Interfaces.Repositories;

namespace AdOut.Point.Core.Replication
{
    public class PlanAdPointReplicationFactory : IReplicationHandlerFactory<PlanAdPoint>
    {
        private readonly IPlanAdPointRepository _planAdPointRepository;
        private readonly ICommitProvider _commitProvider;

        public PlanAdPointReplicationFactory(
            IPlanAdPointRepository planAdPointRepository,
            ICommitProvider commitProvider)
        {
            _planAdPointRepository = planAdPointRepository;
            _commitProvider = commitProvider;
        }

        public IReplicationHandler<PlanAdPoint> CreateReplicationHandler(EventAction action)
        {
            return action switch
            {
                EventAction.Created => new PlanAdPointCreatedHandler(_planAdPointRepository, _commitProvider),
                _ => null
            };
        }
    }
}
