using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Shared.Models
{
    [TableName("CategoryTypes")]
    public class CategoryType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
    }
}
