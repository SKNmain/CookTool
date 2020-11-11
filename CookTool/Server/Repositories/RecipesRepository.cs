using CookTool.Shared.Models;
using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Server.Repositories
{
    public class RecipesRepository : IRepository<Recipe>
    {
        private IDatabase db = DatabaseConnection.Instance;

        public IList<Recipe> GetAllRecords()
        {
            return db.Fetch<Recipe>(SqlConstants.ALL_RECIPES);
        }

        public IList<Recipe> GetUserRecords(int userId)
        {
            return db.Fetch<Recipe>(SqlConstants.USER_RECIPES(userId));
        }

        public IList<Recipe> GetBestRatingRecords()
        {
            return db.Fetch<Recipe>(SqlConstants.ALL_RECIPES)
                .Where(recipe => recipe.IsPrivate == false)
                .OrderBy(recipe => recipe.Rating)
                .ToList();
        }

        public Recipe GetRecordById(int id)
        {
            return db.SingleById<Recipe>(id);
        }

        public IList<Category> GetRecipeCategories(int id)
        {
            return db.Fetch<Category>(SqlConstants.RECIPE_CATEGORIES(id));
        }

        public IList<Ingredient> GetRecipeIngredients(int id)
        {
            return db.Fetch<Ingredient>(SqlConstants.RECIPE_INGREDIENTS(id));
        }

        public void AddRecord(Recipe record)
        {
            db.Insert(record);
        }

        public void UpdateRecord(int id, Recipe record)
        {
            record.Id = id;
            db.Update(record);
        }

        public void DeleteRecord(int id)
        {
            db.Delete<Recipe>(id);
        }
    }
}
