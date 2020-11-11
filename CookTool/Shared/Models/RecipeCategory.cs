using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Shared.Models
{
    [TableName("RecipeCategory")]
    public class RecipeCategory
    {
        public int RecipeId { get; set; }
        public int CategoryId { get; set; }
    }
}
