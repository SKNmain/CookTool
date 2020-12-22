using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Client.Shared.Models.SelectedItems
{
    public class RecipeSelect
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsSelected { get; set; }
        public bool IsDisabled { get; set; }

        public RecipeSelect(int id, string name, bool isSelected, bool isDisabled)
        {
            this.Id = id;
            this.Title = name;
            this.IsSelected = isSelected;
            this.IsDisabled = isDisabled;
        }
    }
}
