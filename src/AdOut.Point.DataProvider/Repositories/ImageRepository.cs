using AdOut.Point.Model.Database;
using AdOut.Point.Model.Interfaces.Context;
using AdOut.Point.Model.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AdOut.Point.DataProvider.Repositories
{
    public class ImageRepository : BaseRepository<Image>, IImageRepository
    {
        public ImageRepository(IDatabaseContext context) 
            : base(context)
        {
        }

        public Task<Image> GetByIdAsync(string imageId)
        {
            return Context.Images.SingleOrDefaultAsync(i => i.Id == imageId);
        }
    }
}
