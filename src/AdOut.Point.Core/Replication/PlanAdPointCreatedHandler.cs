using AdOut.Extensions.Communication.Interfaces;
using AdOut.Extensions.Context;
using AdOut.Point.Model.Database;
using AdOut.Point.Model.Interfaces.Repositories;
using System.Threading.Tasks;

namespace AdOut.Point.Core.Replication
{
    public class PlanAdPointCreatedHandler : IReplicationHandler<PlanAdPoint>
    {
        private readonly IPlanAdPointRepository _planAdPointRepository;
        private readonly ICommitProvider _commitProvider;

        public PlanAdPointCreatedHandler(
            IPlanAdPointRepository planAdPointRepository,
            ICommitProvider commitProvider)
        {
            _planAdPointRepository = planAdPointRepository;
            _commitProvider = commitProvider;
        }

        public async Task HandleAsync(PlanAdPoint entity)
        {
            //todo: check a work of GetByIdAsync method
            var dbEntity = await _planAdPointRepository.GetByIdAsync(entity.AdPointId, entity.PlanId);
            if (dbEntity == null)
            {
                _planAdPointRepository.Create(entity);
                await _commitProvider.SaveChangesAsync(false);
            }
        }
    }
}
