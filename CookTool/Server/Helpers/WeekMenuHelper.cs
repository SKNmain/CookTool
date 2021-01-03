using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CookTool.Server.Helpers
{
    public class WeekMenuHelper
    {
        public static string PrepareWeekMenuData()
        {
            Dictionary<string, Dictionary<string, List<int>>> result = new Dictionary<string, Dictionary<string, List<int>>>();
            foreach (var day in Client.Shared.Helpers.WeekMenuHelper.WeekDays)
            {
                var item = new Dictionary<string, List<int>>();
                foreach (var time in Client.Shared.Helpers.WeekMenuHelper.TimeOfDays)
                {
                    item.Add(time, new List<int>());
                }
                result.Add(day, item);
            }
            return JsonSerializer.Serialize(result);
        }
    }
}
