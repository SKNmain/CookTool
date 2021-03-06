using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Shared.Models
{
    [TableName("Categories")]
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
         
        [Reference(ReferenceType.Foreign, ColumnName ="TypeId", ReferenceMemberName = "TypeId")]
        public CategoryType CategoryType { get; set; }
    }
}
