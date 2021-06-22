using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SolarEnergySystem.Core.Entities;
using SolarEnergySystem.Core.Interfaces;
using SolarEnergySystem.Core.Services;
using SolarEnergySystem.Infrastructure;

namespace SolarEnergySystem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PanelsController : BaseApiController
    {
        private readonly IPanelService _panelService;

        public PanelsController(IPanelService panelService)
        {
            _panelService = panelService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _panelService.GetAllPanels();
            return GetResult(result);
        }

        [HttpPost]
        public IActionResult Register(string panelId,[FromBody] panel)


    }
}
