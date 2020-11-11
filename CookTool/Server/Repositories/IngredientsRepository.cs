using CookTool.Shared.Models;
using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Server.Repositories
{
    public class IngredientsRepository : IRepository<Ingredient>
    {
        private IDatabase db = DatabaseConnection.Instance;
        public IList<Ingredient> GetAllRecords()
        {
            return db.Fetch<Ingredient>(SqlConstants.ALL_INGREDIENTS);
        }

        public Ingredient GetRecordById(int id)
        {
            return db.SingleById<Ingredient>(id);
        }

        public Ingredient GetRecordByName(string name)
        {
            return db.Fetch<Ingredient>(SqlConstants.INGREDIENT_BY_NAME(name)).First();
        }

        public void AddRecord(Ingredient record)
        {
            db.Insert(record);
        }

        public void DeleteRecord(int id)
        {
            db.Delete<Ingredient>(id);
        }

        public void UpdateRecord(int id, Ingredient record)
        {
            record.Id = id;
            db.Update(record);
        }
    }
}
