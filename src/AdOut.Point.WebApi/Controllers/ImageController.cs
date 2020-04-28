using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdOut.Point.Model.Interfaces.Context;
using AdOut.Point.Model.Interfaces.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdOut.Point.WebApi.Controllers
{
    [Route("api/[controller]")]
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
        [Route("upload")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadImage(IFormFile image, int adPointId)
        {
            await _imageManager.AddImageToAdPointAsync(image, adPointId);
            await _commitProvider.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteImage(int id)
        {
            await _imageManager.DeleteImageFromAdPointAsync(id);
            await _commitProvider.SaveChangesAsync();

            return NoContent();
        }
    }
}