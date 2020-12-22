using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookTool.Server.Helpers;
using CookTool.Server.Repositories;
using CookTool.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CookTool.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipsController : ControllerBase
    {
        private readonly TipsRepository tipsRepository = new TipsRepository();

        public IList<Tip> GetBestRatingTips()
        {
            return tipsRepository.GetAllRecords();
        }

        [HttpGet("{keyWord}/filteredtips")]
        public IList<Tip> GetFilteredTips(string keyWord)
        {
            return tipsRepository.GetAllRecords().Where(tip => LevensteinDistance.FulfillRules(tip.Title, keyWord) == true).ToList();
        }

        [HttpGet("{id}")]
        public Tip GetTip(int id)
        {
            return tipsRepository.GetRecordById(id);
        }

        [HttpGet("{userid}/tips")]
        [Authorize]
        public IList<Tip> GetUserTips(int userid)
        {
            return tipsRepository.GetUserRecords(userid);
        }

        [HttpPost]
        [Authorize]
        public void Post([FromBody] Tip tip)
        {
            tipsRepository.AddRecord(tip);
        }

        [HttpPut("{id}")]
        [Authorize]
        public void Put(int id, [FromBody] Tip tip)
        {
            tipsRepository.UpdateRecord(id, tip);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public void Delete(int id)
        {
            tipsRepository.DeleteRecord(id);
        }
    }
}
