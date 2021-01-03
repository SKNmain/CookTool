
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
    public class WeekMenusController : ControllerBase
    {
        private readonly WeekMenusRepository weekMenuRepository = new WeekMenusRepository();

        [HttpGet("{userid}")]
        [Authorize]
        public WeekMenu GetUserWeekMenu(int userid)
        {
            return weekMenuRepository.GetRecordByUserId(userid);
        }

        [HttpPost]
        public void Post([FromBody] WeekMenu weekMenu)
        {
            weekMenuRepository.AddRecord(weekMenu);
        }

        [HttpPut("{id}")]
        [Authorize]
        public void Put(int id, [FromBody] WeekMenu weekMenu)
        {
            weekMenuRepository.UpdateRecord(id, weekMenu);
        }
    }
}
