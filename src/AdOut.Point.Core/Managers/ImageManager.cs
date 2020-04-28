using AdOut.Point.Model.Database;
using AdOut.Point.Model.Exceptions;
using AdOut.Point.Model.Interfaces.Content;
using AdOut.Point.Model.Interfaces.Managers;
using AdOut.Point.Model.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using System;
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

        public async Task AddImageToAdPointAsync(IFormFile image, int adPointId)
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
            var imageFilePath = _contentStorage.GenerateFilePath(image.FileName);

            await _contentStorage.CreateObjectAsync(imageStream, imageFilePath);

            var imageEntity = new Image()
            {
                AdPoint = adPoint,
                AddedDateTime = DateTime.UtcNow,
                Path = imageFilePath
            };

            Create(imageEntity);
        }

        public async Task DeleteImageFromAdPointAsync(int imageId)
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
