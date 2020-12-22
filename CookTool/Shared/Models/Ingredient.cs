using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Shared.Models
{
    [TableName("Ingredients")]
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }
        public int MeasurementUnitId { get; set; }
        public int RecipeId { get; set; }
    }
}
