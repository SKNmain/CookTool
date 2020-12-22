using CookTool.Server.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Server.Helpers
{
    public class RecipeHelper
    {
        public static List<int> FilterRecipeIds(List<string> userIngredients, IngredientsRepository ingredientsRepository)
        {
            //todo: później też można się pobawić w quantity
            Dictionary<int, int> recipesIngValue = new Dictionary<int, int>();
            foreach (var item in ingredientsRepository.GetAllRecords())
            {
                if (userIngredients.Contains(item.Name))
                {
                    recipesIngValue.TryGetValue(item.RecipeId, out int value);
                    recipesIngValue[item.RecipeId] = ++value;
                }
            }
            return recipesIngValue.OrderByDescending(item => item.Value).Take(TOTAL_RECIPES_NUMBER).Select(keyValue => keyValue.Key).ToList();
        }

        private const int TOTAL_RECIPES_NUMBER = 5;
    }
}
