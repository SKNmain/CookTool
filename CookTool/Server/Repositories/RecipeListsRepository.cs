using CookTool.Shared.Models;
using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Server.Repositories
{
    public class RecipeListsRepository : IRepository<RecipeList>
    {
        private IDatabase db = DatabaseConnection.Instance;

        public IList<RecipeList> GetAllRecords()
        {
            throw new NotImplementedException();
        }

        public IList<RecipeList> GetUserRecords(int userId)
        {
            return db.Fetch<RecipeList>(SqlConstants.USER_RECIPE_LISTS(userId));
        }

        public RecipeList GetRecordById(int id)
        {
            return db.SingleById<RecipeList>(id);
        }
        public RecipeList GetRecordByTitle(int userId, string title)
        {
            return db.Fetch<RecipeList>(SqlConstants.USER_RECIPE_LIST_BY_TITLE(userId, title)).First();
        }

        public RecipeList GetFavRecipeList(int userId)
        {
            return db.Fetch<RecipeList>(SqlConstants.USER_RECIPE_LIST_FAVE(userId)).First();
        }

        public IList<Recipe> GetRecipeListRecipes(int id)
        {
            return db.Fetch<Recipe>(SqlConstants.RECIPE_LIST_RECIPES(id));
        }

        public void AddRecord(RecipeList record)
        {
            db.Insert(record);
        }

        public void UpdateRecord(int id, RecipeList record)
        {
            record.Id = id;
            db.Update(record);
        }

        public void DeleteRecord(int id)
        {
            db.Delete<RecipeList>(id);
        }
    }
}
