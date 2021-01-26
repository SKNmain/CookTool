using CookTool.Server.Helpers;
using CookTool.Server.Repositories;
using CookTool.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MeasurementUnitsController : Controller
    {
        private readonly MeasurementUnitsRepository measurementUnitsRepository = new MeasurementUnitsRepository();
        private List<MeasurementUnit> measurementUnits;
        public List<MeasurementUnit> GetMeasurementUnits()
        {
            AuthHelper.CheckTokenBlackListed(Request);

            if (measurementUnits != null) return measurementUnits;
            measurementUnits = (List<MeasurementUnit>)measurementUnitsRepository.GetAllRecords();
            return measurementUnits;
        }
    }
}
