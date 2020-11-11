using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Shared.Models
{
    [TableName("Recipes")]
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Preparation { get; set; }
        public int PortionSize { get; set; }
        public DateTime PreparationTime { get; set; }
        public bool IsPrivate { get; set; }
        public string Notes { get; set; }
        public int Rating { get; set; }
        public int UserId { get; set; }
        public byte[] Image { get; set; }
    }
}
