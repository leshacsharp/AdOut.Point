﻿using AdOut.Point.Model.Api;
using AdOut.Point.Model.Dto;
using AdOut.Point.Model.Interfaces.Context;
using AdOut.Point.Model.Interfaces.Managers;
using AdOut.Point.WebApi.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AdOut.Point.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdPointController : ControllerBase
    {
        private readonly IAdPointManager _adPointManager;
        private readonly IAdPointDayOffManager _adPointDayOffManager;
        private readonly ICommitProvider _commitProvider;

        public AdPointController(
            IAdPointManager adPointManager,
            IAdPointDayOffManager adPointDayOffManager,
            ICommitProvider commitProvider)
        {
            _adPointManager = adPointManager;
            _adPointDayOffManager = adPointDayOffManager;
            _commitProvider = commitProvider;
        }

        [HttpPost]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> CreateAdPoint(CreateAdPointModel createModel)
        {
            var userId = User.GetUserId();
            _adPointManager.Create(createModel, userId);

            await _commitProvider.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(AdPointDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAdPoint(string id)
        {
            var adPoint = await _adPointManager.GetByIdAsync(id);
            if (adPoint == null)
            {
                return NotFound();
            }

            return Ok(adPoint);
        }

        [HttpPost]
        [Route("add-dayoff")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddDayOffToAdPoint(string dayOffId, string adPointId)
        {
            await _adPointDayOffManager.AddDayOffToAdPointAsync(dayOffId, adPointId);
            await _commitProvider.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("delete-dayoff")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteDayOffFromAdPoint(string dayOffId, string adPointId)
        {
            await _adPointDayOffManager.DeleteDayOffFromAdPointAsync(dayOffId, adPointId);
            await _commitProvider.SaveChangesAsync();

            return NoContent();
        }
    }
}