using AdOut.Extensions.Exceptions;
using AdOut.Point.Core.Helpers;
using AdOut.Point.Model.Database;
using AdOut.Point.Model.Interfaces.Content;
using AdOut.Point.Model.Interfaces.Managers;
using AdOut.Point.Model.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AdOut.Point.Core.Managers
{
    public class ImageManager : BaseManager<Image>, IImageManager
    {
        private readonly IImageRepository _imageRepository;
        private readonly IAdPointRepository _adPointRepository;
        private readonly IContentStorage _contentStorage;

        public ImageManager(
            IImageRepository imageRepository,
            IAdPointRepository adPointRepository,
            IContentStorage contentStorage)
            : base(imageRepository)
        {
            _imageRepository = imageRepository;
            _adPointRepository = adPointRepository;
            _contentStorage = contentStorage;
        }

        public async Task AddImageToAdPointAsync(IFormFile image, string adPointId)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            var adPoint = await _adPointRepository.GetByIdAsync(adPointId);
            if (adPoint == null)
            {
                throw new ObjectNotFoundException($"AdPoint with id={adPointId} was not found");
            }

            var imageStream = image.OpenReadStream();
            var imageFilePath = PathHelper.GeneratePath(Path.GetExtension(image.FileName), Model.Enum.DirectoryPath.None);

            await _contentStorage.CreateObjectAsync(imageStream, imageFilePath);

            var imageEntity = new Image()
            {
                AdPoint = adPoint,
                AddedDateTime = DateTime.UtcNow,
                Path = imageFilePath
            };

            Create(imageEntity);
        }

        public async Task DeleteImageFromAdPointAsync(string imageId)
        {
            var image = await _imageRepository.GetByIdAsync(imageId);
            if (image == null)
            {
                throw new ArgumentNullException($"Image with id={imageId} was not found");
            }

            await _contentStorage.DeleteObjectAsync(image.Path);

            Delete(image);
        }
    }
}
