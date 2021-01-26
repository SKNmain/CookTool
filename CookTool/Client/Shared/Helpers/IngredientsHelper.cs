using CookTool.Client.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Client.Shared.Helpers
{
    public class IngredientsHelper
    {
        public static List<IngredientItem> BasicItems = new List<IngredientItem>()
        {
            new IngredientItem("wheat flour", 1, 3, 1.56),
            new IngredientItem("wheat bread flour", 1, 3, 1.67),
            new IngredientItem("wholemeal wheat flour", 1, 3, 1.72),
            new IngredientItem("wheat flour(450)", 1, 3, 1.25),
            new IngredientItem("light rye flour", 1, 3, 1.92),
            new IngredientItem("wholemeal rye flour", 1, 3, 1.85),
            new IngredientItem("light spelled flour", 1, 3, 1.79),
            new IngredientItem("wholemeal spelled flour", 1, 3, 1.72),
            new IngredientItem("potato starch", 1, 3, 1.39),
            new IngredientItem("sugar", 1, 3, 1.14),
            new IngredientItem("powdered sugar", 1, 3, 1.79),
            new IngredientItem("almonds", 1, 3, 1.56),
            new IngredientItem("almond flakes", 1, 3, 2.27),
            new IngredientItem("italian nuts", 1, 3, 2.5),
            new IngredientItem("hazelnuts", 1, 3, 1.67),
            new IngredientItem("ground italian nuts", 1, 3, 2.5),
            new IngredientItem("chopped italiannuts", 1, 3, 0.16),
            new IngredientItem("poppy seed", 1, 3, 1.56),
            new IngredientItem("ground poppy seeds", 1, 3, 2.78),
            new IngredientItem("cocoa", 1, 3, 2.5),
            new IngredientItem("milk", 1, 3, 1),
            new IngredientItem("milk powder", 1, 3, 2.5),
            new IngredientItem("butter", 1, 3, 1.04),
            new IngredientItem("water", 1, 3, 1),
            new IngredientItem("whipping cream 30%/36%", 1, 3, 0.98),
            new IngredientItem("sour cream 18%", 1, 3, 0.96),
            new IngredientItem("buttermilk", 1, 3, 0.98),
            new IngredientItem("natural yogurt", 1, 3, 0.96),
            new IngredientItem("oil", 1, 3, 1.05),
            new IngredientItem("honey", 1, 3, 0.69),
            new IngredientItem("golden syrup", 1, 3, 0.68),
            new IngredientItem("coconut shrims", 1, 3, 2.63),
            new IngredientItem("chocolate chips", 1, 3, 1.43),
            new IngredientItem("Nutella", 1, 3, 0.79),
            new IngredientItem("peanut butter", 1, 3, 0.93),
            new IngredientItem("cream cheese", 1, 3, 0.83),
            new IngredientItem("condensed milk", 1, 3, 0.78),
            new IngredientItem("oatmeal", 1, 3, 2.27),
            new IngredientItem("Pumpkin puree", 1, 3, 1),
            new IngredientItem("crushed rye", 1, 3, 1.47),
            new IngredientItem("millet", 1, 3, 1.14),
            new IngredientItem("sesame", 1, 3, 1.56),
        };
    }
}
