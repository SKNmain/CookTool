using NPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookTool.Shared.Models
{
    [TableName("BlacklistedTokens")]
    public class BlackListedToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
    }
}
