using AdOut.Point.Model.Database;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AdOut.Point.Model.Interfaces.Managers
{
    public interface IImageManager
    {
        Task AddImageToAdPointAsync(IFormFile image, string adPointId);
        Task DeleteImageFromAdPointAsync(string imageId);
    }
}
