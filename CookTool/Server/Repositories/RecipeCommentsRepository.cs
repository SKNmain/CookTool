using CookTool.Shared.Models;
using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Server.Repositories
{
    public class RecipeCommentsRepository : IRepository<RecipeComment>
    {
        private IDatabase db = DatabaseConnection.Instance;
        public IList<RecipeComment> GetAllRecords()
        {
            return db.Fetch<RecipeComment>(SqlConstants.ALL_RECIPE_COMMENTS);
        }

        public RecipeComment GetRecordById(int id)
        {
            return db.SingleById<RecipeComment>(id);
        }

        public IList<RecipeComment> GetUserRecords(int userid)
        {
            return db.Fetch<RecipeComment>(SqlConstants.USER_RECIPE_COMMENTS(userid));
        }

        public IList<RecipeComment> GetRecipeRecords(int recipeid)
        {
            return db.Fetch<RecipeComment>(SqlConstants.RECIPE_COMMENTS(recipeid));
        }

        public void AddRecord(RecipeComment record)
        {
            db.Insert(record);
        }

        public void DeleteRecord(int id)
        {
            db.Delete<RecipeComment>(id);
        }

        public void UpdateRecord(int id, RecipeComment record)
        {
            record.Id = id;
            db.Update(record);
        }
    }
}
