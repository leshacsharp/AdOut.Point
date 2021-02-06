using AdOut.Extensions.Context;
using AdOut.Point.Model.Interfaces.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AdOut.Point.WebApi.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageManager _imageManager;
        private readonly ICommitProvider _commitProvider;

        public ImageController(
            IImageManager imageManager,
            ICommitProvider commitProvider)
        {
            _imageManager = imageManager;
            _commitProvider = commitProvider;
        }

        [HttpPost]
        [Route("image")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadImage(IFormFile image, string adPointId)
        {
            await _imageManager.AddImageToAdPointAsync(image, adPointId);
            await _commitProvider.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("image")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteImage(string id)
        {
            await _imageManager.DeleteImageFromAdPointAsync(id);
            await _commitProvider.SaveChangesAsync();

            return NoContent();
        }
    }
}