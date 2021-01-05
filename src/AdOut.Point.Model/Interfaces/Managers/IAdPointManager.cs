using AdOut.Point.Model.Api;
using AdOut.Point.Model.Dto;
using System.Threading.Tasks;

namespace AdOut.Point.Model.Interfaces.Managers
{
    public interface IAdPointManager
    {
        void Create(CreateAdPointModel createModel);
        Task DeleteAsync(string adPointId);
        Task<AdPointDto> GetByIdAsync(string adPointId);
    }
}
