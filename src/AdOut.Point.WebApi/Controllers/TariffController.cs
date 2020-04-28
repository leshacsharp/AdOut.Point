using AdOut.Point.Model.Api;
using AdOut.Point.Model.Interfaces.Context;
using AdOut.Point.Model.Interfaces.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AdOut.Point.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TariffController : ControllerBase
    {
        private readonly ITariffManager _tariffManager;
        private readonly ICommitProvider _commitProvider;

        public TariffController(
            ITariffManager tariffManager,
            ICommitProvider commitProvider)
        {
            _tariffManager = tariffManager;
            _commitProvider = commitProvider;   
        }   

        [HttpPost]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTariff(CreateTariffModel createModel)
        {
            var validationResult = await _tariffManager.ValidateTariff(createModel.AdPointId, createModel.StartTime, createModel.EndTime);
            if (!validationResult.IsSuccessed)
            {
                var validationErrors = string.Join("\n", validationResult.Errors);
                return BadRequest(validationErrors);
            }

            await _tariffManager.CreateAsync(createModel);
            await _commitProvider.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut]
        [Route("update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTariff(UpdateTariffModel updateModel)
        {
            var validationResult = await _tariffManager.ValidateTariff(updateModel.AdPointId, updateModel.StartTime, updateModel.EndTime);
            if (!validationResult.IsSuccessed)
            {
                var validationErrors = string.Join("\n", validationResult.Errors);
                return BadRequest(validationErrors);
            }

            await _tariffManager.UpdateAsync(updateModel);
            await _commitProvider.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteTariff(int id)
        {
            await _tariffManager.DeleteAsync(id);
            await _commitProvider.SaveChangesAsync();

            return NoContent();
        }
    }
}