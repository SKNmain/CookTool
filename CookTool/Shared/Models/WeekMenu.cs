using Newtonsoft.Json.Linq;
using NPoco;
using NPoco.fastJSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Shared.Models
{
    [TableName("WeekMenus")]
    public class WeekMenu
    {
        public int Id { get; set; }
        public string WeekMenuData { get; set; }
        public int UserId { get; set; }
    }
}
