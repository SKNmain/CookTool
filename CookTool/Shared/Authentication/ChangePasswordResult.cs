using System;
using System.Collections.Generic;
using System.Text;

namespace CookTool.Shared.Authentication
{
    public class ChangePasswordResult
    {
        public bool Successful { get; set; }
        public string Error { get; set; }
    }
}
