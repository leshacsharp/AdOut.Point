using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdOut.Extensions.Context;
using AdOut.Point.Model.Api;
using AdOut.Point.Model.Interfaces.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdOut.AdPoint.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IAdPointManager _adPointManager;
        private readonly ICommitProvider _commitProvider;

        public HomeController(
            IAdPointManager adPointManager,
            ICommitProvider commitProvider)
        {
            _adPointManager = adPointManager;
            _commitProvider = commitProvider;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var time = new TimeSpan(22, 0, 0);
            return Ok(time);
        }

        [HttpPost]
        [Route("create-adpoint")]
        public async Task<IActionResult> CreateAdPoint(CreateAdPointModel createModel)
        {
            _adPointManager.Create(createModel);
            await _commitProvider.SaveChangesAsync();

            return Ok();
        }
    }
}