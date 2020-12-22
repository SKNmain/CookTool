using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Client.Shared.Models
{
    public class Filter
    {
        public string Name { get; set; }
        public bool IsSelected { get; set; }

        public Filter(string name, bool selected)
        {
            this.Name = name;
            this.IsSelected = selected;
        }
    }
}
