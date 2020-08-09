using AdOut.Point.Model.Database;
using System.Threading.Tasks;

namespace AdOut.Point.Model.Interfaces.Repositories
{
    public interface IImageRepository : IBaseRepository<Image>
    {
        Task<Image> GetByIdAsync(string imageId);
    }
}
