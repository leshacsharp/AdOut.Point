using AdOut.Point.Model.Api;
using AdOut.Point.Model.Database;
using AdOut.Point.Model.Dto;
using System.Threading.Tasks;

namespace AdOut.Point.Model.Interfaces.Managers
{
    public interface IAdPointManager : IBaseManager<AdPoint>
    {
        void Create(CreateAdPointModel createModel, string userId);
        Task<AdPointDto> GetByIdAsync(int adPointId);
    }
}
