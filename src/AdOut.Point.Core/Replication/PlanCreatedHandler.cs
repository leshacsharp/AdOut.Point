using AdOut.Extensions.Communication.Interfaces;
using AdOut.Extensions.Context;
using AdOut.Point.Model.Database;
using AdOut.Point.Model.Interfaces.Repositories;
using System.Threading.Tasks;

namespace AdOut.Point.Core.Replication
{
    public class PlanCreatedHandler : IReplicationHandler<Plan>
    {
        private readonly IPlanRepository _planRepository;
        private readonly ICommitProvider _commitProvider;

        public PlanCreatedHandler(
            IPlanRepository planRepository,
            ICommitProvider commitProvider)
        {
            _planRepository = planRepository;
            _commitProvider = commitProvider;
        }

        public async Task HandleAsync(Plan entity)
        {
            var dbEntity = await _planRepository.GetByIdAsync(entity.Id);
            if (dbEntity == null)
            {
                _planRepository.Create(entity);
                await _commitProvider.SaveChangesAsync(false);
            }
        }
    }
}
