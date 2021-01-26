using BlazorInputFile;
using CookTool.Shared.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Client.Shared.Helpers
{
    public class ImageHelper
    {
        public static async Task ReadImage(IFileListEntry[] files, IFileListEntry file, Recipe recipe)
        {
            file = files.FirstOrDefault();
            using (var ms = new MemoryStream())
            {
                await file.Data.CopyToAsync(ms);
                recipe.Image = ms.ToArray();
            }
        }

        public static async Task ReadImage(IFileListEntry[] files, IFileListEntry file, Tip tip)
        {
            file = files.FirstOrDefault();
            using (var ms = new MemoryStream())
            {
                await file.Data.CopyToAsync(ms);
                tip.Image = ms.ToArray();
            }
        }

        public static async Task ReadImage(IFileListEntry[] files, IFileListEntry file, User user)
        {
            file = files.FirstOrDefault();
            using (var ms = new MemoryStream())
            {
                await file.Data.CopyToAsync(ms);
                user.Image = ms.ToArray();
            }
        }

        public static string ImageStyle = "width:45px; height:45px; overflow:hidden;";

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
