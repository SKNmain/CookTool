using CookTool.Shared.Models;
using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Server.Repositories
{
    public class UserIngredientsRepository : IRepository<UserIngredient>
    {
        private IDatabase db = DatabaseConnection.Instance;

        public IList<UserIngredient> GetAllRecords()
        {
            throw new NotImplementedException();
        }

        public UserIngredient GetRecordById(int id)
        {
            throw new NotImplementedException();
        }

        public IList<UserIngredient> GetUserIngredients(int userid)
        {
            return db.Fetch<UserIngredient>(SqlConstants.USER_INGREDIENTS(userid));
        }

        public void AddRecord(UserIngredient record)
        {
            db.Insert(record);
        }

        public void DeleteRecord(int id)
        {
            db.Delete<UserIngredient>(id);
        }

        public void UpdateRecord(int id, UserIngredient record)
        {
            throw new NotImplementedException();
        }
    }
}
