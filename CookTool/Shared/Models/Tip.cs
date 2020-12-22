using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Shared.Models
{
    [TableName("Tips")]
    public class Tip
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public double Rating { get; set; }
        public int UserId { get; set; }
        public byte[] Image { get; set; }
    }
}
