using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Client.Shared.Models
{
    public class IngredientItem
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int MeasurementUnitId { get; set; }
    }
}
