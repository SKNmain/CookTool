using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Shared.Models
{
    [TableName("UserIngredients")]
    public class UserIngredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }
        public int MeasurementUnitId { get; set; }
        public int UserId { get; set; }
    }
}
