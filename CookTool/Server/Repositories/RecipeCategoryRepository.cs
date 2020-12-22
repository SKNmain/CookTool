using CookTool.Shared.Models;
using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Server.Repositories
{
    public class RecipeCategoryRepository : IRepository<RecipeCategory>
    {
        private IDatabase db = DatabaseConnection.Instance;

        public IList<RecipeCategory> GetAllRecords()
        {
            return db.Fetch<RecipeCategory>(SqlConstants.ALL_RECIPE_CATEGORIES);
        }

        public RecipeCategory GetRecordById(int id)
        {
            throw new NotImplementedException();
        }

        public void AddRecord(RecipeCategory record)
        {
            db.Insert(record);
        }

        public void DeleteRecord(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteRecord(int recipeid, int categoryid)
        {
            db.Execute($"DELETE FROM recipecategory WHERE recipeid = {recipeid} AND categoryid = {categoryid}");
        }

        public void UpdateRecord(int id, RecipeCategory record)
        {
            throw new NotImplementedException();
        }
    }
}
