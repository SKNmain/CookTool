using CookTool.Shared.Models;
using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Server.Repositories
{
    public class RecipeIngredientRepository : IRepository<RecipeIngredient>
    {
        private IDatabase db = DatabaseConnection.Instance;

        public IList<RecipeIngredient> GetAllRecords()
        {
            throw new NotImplementedException();
        }

        public RecipeIngredient GetRecordById(int id)
        {
            throw new NotImplementedException();
        }

        public void AddRecord(RecipeIngredient record)
        {
            db.Insert(record);
        }

        public void DeleteRecord(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateRecord(int id, RecipeIngredient record)
        {
            throw new NotImplementedException();
        }
    }
}
