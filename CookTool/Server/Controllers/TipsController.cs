using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookTool.Server.Repositories;
using CookTool.Shared.Models;
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
    }
}
