using CookTool.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookTool.Client.Shared.Models.SelectedItems;

namespace CookTool.Client.Shared.Helpers
{
    public class RecipeHelper
    {
        public static List<Category> PrepareCategories(List<Category> categories, List<CategorySelect> selectedCategories) 
        {
            var filtered = selectedCategories.FindAll(sc => sc.IsSelected == true);

            return categories.Where(cat => IsChosen(filtered, cat.Id) == true).ToList();
        }

        private static bool IsChosen(List<CategorySelect> filtered, int id)
        {
            return filtered.Any(fsc => fsc.Id == id);
        }

        public static bool IsDisabled(Recipe recipe, List<Recipe> recipeListRecipes)
        {
            return recipeListRecipes.Any(rec => rec.Id == recipe.Id);
        }

        public static String IsPublic(bool isPrivate)
        {
            return isPrivate ? "No" : "Yes";
        }

        public static string RenderIngredientQuantity(double quantity)
        {
            if (quantity > 0)
            {
                return quantity.ToString();
            }
            return "Any quantity";
        }

        public static string GetRecipeTitle(List<Recipe> recipes, int id)
        {
            return recipes.Find(recipe => recipe.Id == id).Title;
        }
    }
}
