using CookTool.Shared.Models;
using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Server.Repositories
{
    public class CategoriesRepository : IRepository<Category>
    {
        private IDatabase db = DatabaseConnection.Instance;

        public IList<Category> GetAllRecords()
        {
            return db.Fetch<Category>(SqlConstants.ALL_CATEGORIES);
        }

        public IList<Category> GetAllRecordsByCategoryTypeName(string name)
        {
            return db.Fetch<Category>(SqlConstants.ALL_CATEGORY_TYPE_CATEGORIES(name));
        }

        public Category GetRecordById(int id)
        {
            return db.SingleById<Category>(id);
        }

        public void AddRecord(Category record)
        {
            throw new NotImplementedException();
        }

        public void UpdateRecord(int id, Category record)
        {
            throw new NotImplementedException();
        }

        public void DeleteRecord(int id)
        {
            throw new NotImplementedException();
        }
    }
}
