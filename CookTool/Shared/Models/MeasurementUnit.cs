using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Shared.Models
{
    [TableName("MeasurementUnits")]
    public class MeasurementUnit
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
