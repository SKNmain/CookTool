using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Shared.Models
{
    [TableName("RecipeListRecipe")]
    public class RecipeListRecipe
    {
        public int RecipeListId { get; set; }
        public int RecipeId { get; set; }
    }
}
