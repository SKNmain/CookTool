using CookTool.Shared.Models;
using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Server.Repositories
{
    public class RecipeListRecipeRepository : IRepository<RecipeListRecipe>
    {
        private IDatabase db = DatabaseConnection.Instance;

        public IList<RecipeListRecipe> GetAllRecords()
        {
            throw new NotImplementedException();
        }

        public RecipeListRecipe GetRecordById(int id)
        {
            throw new NotImplementedException();
        }

        public void AddRecord(RecipeListRecipe record)
        {
            db.Insert(record);
        }

        public void DeleteSpecialRecord(int recipeid, int recipelistid)
        {
            db.Execute($"DELETE FROM recipelistrecipe WHERE recipelistid = {recipelistid} AND recipeid = {recipeid}");
        }

        public void DeleteRecord(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateRecord(int id, RecipeListRecipe record)
        {
            throw new NotImplementedException();
        }
    }
}
