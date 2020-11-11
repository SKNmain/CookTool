using CookTool.Shared.Models;
using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Server.Repositories
{
    public class CategoryTypesRepository : IRepository<CategoryType>
    {
        private IDatabase db = DatabaseConnection.Instance;
        public IList<CategoryType> GetAllRecords()
        {
            return db.Fetch<CategoryType>(SqlConstants.ALL_CATEGORY_TYPES);
        }

        public CategoryType GetRecordById(int id)
        {
            return db.SingleById<CategoryType>(id);
        }

        public CategoryType GetRecordByName(string typename)
        {
            return db.Fetch<CategoryType>(SqlConstants.CATEGORY_TYPE_BY_NAME(typename)).First();
        }

        public void AddRecord(CategoryType record)
        {
            throw new NotImplementedException();
        }

        public void DeleteRecord(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateRecord(int id, CategoryType record)
        {
            throw new NotImplementedException();
        }
    }
}
