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
            throw new NotImplementedException();
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

        public void UpdateRecord(int id, RecipeCategory record)
        {
            throw new NotImplementedException();
        }
    }
}
