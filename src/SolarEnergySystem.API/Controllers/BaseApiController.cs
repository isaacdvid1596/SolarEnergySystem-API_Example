using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SolarEnergySystem.Core;
using SolarEnergySystem.Core.Enums;

namespace SolarEnergySystem.API.Controllers
{
    public class BaseApiController : ControllerBase
    {
        public IActionResult GetResult<T>(ServiceResult<T> result)
        {
            return result.ResponseCode switch
            {
                ResponseCode.Success => Ok(result.Result),
                ResponseCode.Error => BadRequest(result.Error),
                ResponseCode.InternalServerError => StatusCode(500, result.Error),
                ResponseCode.NotFound => NotFound(result.Error),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}