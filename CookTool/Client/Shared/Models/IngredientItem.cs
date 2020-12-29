using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Client.Shared.Models
{
    public class IngredientItem
    {
        public IngredientItem() { }
        public IngredientItem(string name, double quantity, int mu, double gmlRatio)
        {
            this.Name = name;
            this.Quantity = quantity;
            this.MeasurementUnitId = mu;
            this.GMLRatio = gmlRatio;
        }
        public string Name { get; set; }
        public double Quantity { get; set; }
        public int MeasurementUnitId { get; set; }
        public double GMLRatio { get; set; }
    }
}
