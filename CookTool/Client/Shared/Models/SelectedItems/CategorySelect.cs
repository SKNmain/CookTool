using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Client.Shared.Models.SelectedItems
{
    public class CategorySelect
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }

        public CategorySelect(int id, string name, bool isSelected)
        {
            this.Id = id;
            this.Name = name;
            this.IsSelected = isSelected;
        }
    }
}
