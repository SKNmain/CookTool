using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Client.Shared
{
    public class ImageStringRenderer
    {
        private ImageStringRenderer() { }
        public static string RenderImageString(byte[] image, string defaultString)
        {
            if (image == null)
            {
                return defaultString;
            }
            return "data:image/bmp;base64, " + Convert.ToBase64String(image);
        }
    }
}
