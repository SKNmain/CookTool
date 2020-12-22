using BlazorInputFile;
using CookTool.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Client.Shared.Helpers
{
    public class ImageHelper
    {
        public static async Task ReadImage(IFileListEntry[] files, IFileListEntry file, Recipe recipe)
        {
            file = files.FirstOrDefault();
            var data = file.Data;
            recipe.Image = new byte[(int)data.Length];
            await data.ReadAsync(recipe.Image, 0, (int)data.Length);
        }

        public static async void ReadImage(IFileListEntry[] files, IFileListEntry file, Tip tip)
        {
            file = files.FirstOrDefault();
            var data = file.Data;
            tip.Image = new byte[(int)data.Length];
            await data.ReadAsync(tip.Image, 0, (int)data.Length);
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
