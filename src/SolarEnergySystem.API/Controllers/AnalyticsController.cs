using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SolarEnergySystem.Core.Interfaces;
using SolarEnergySystem.Infrastructure;

namespace SolarEnergySystem.API.Controllers
{
    [ApiController]
    [Route("panels")]
    public class AnalyticsController : BaseApiController
    {
        private readonly SolarEnergySystemDatabaseContext _context;
        private readonly IPanelService _panelService;

        public AnalyticsController(SolarEnergySystemDatabaseContext context, IPanelService panelService)
        {
            _context = context;
            _panelService = panelService;
        }

        [Route("{panelId}/analytics/day")]
        public IActionResult Get(string panelId)
        {
            var result = _panelService.GetHourlyReport(panelId);
            return GetResult(result);
        }

        //agregar mediciones electricas
        [HttpPost("{panelId}/analytics")]
        public IActionResult Post(string panelId, [FromBody] double electricityReading)
        {
            var result = _panelService.RegisterElectricityReading(panelId,electricityReading);
            return GetResult(result);
        }
    }
}
