using System;
using System.Collections.Generic;
using System.Text;

namespace CookTool.Shared.Authentication
{
    public class LoginResult
    {
        public bool Successful { get; set; }
        public string Error { get; set; }
        public string Token { get; set; }
        public string Nickname { get; set; }
        public string Image { get; set; }
    }
}
