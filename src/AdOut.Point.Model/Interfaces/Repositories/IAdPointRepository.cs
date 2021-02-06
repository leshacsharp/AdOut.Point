using AdOut.Extensions.Repositories;
using AdOut.Point.Model.Database;
using AdOut.Point.Model.Dto;
using System.Threading.Tasks;

namespace AdOut.Point.Model.Interfaces.Repositories
{
    public interface IAdPointRepository : IBaseRepository<AdPoint>
    {
        Task<AdPointDto> GetDtoByIdAsync(string adPointId);
    }
}
