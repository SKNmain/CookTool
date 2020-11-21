using System;
using System.Collections.Generic;
using System.Text;

namespace CookTool.Shared.Authentication
{
    public class UserView
    {
        public string Nickname { get; set; }
        public byte[] Image { get; set; }

        public UserView(string Nickname, byte[] Image)
        {
            this.Nickname = Nickname;
            this.Image = Image;
        }
    }
}
