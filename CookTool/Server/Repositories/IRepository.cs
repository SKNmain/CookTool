using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Server.Repositories
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetAllRecords();
        T GetRecordById(int id);
        void AddRecord(T record);
        void UpdateRecord(int id, T record);
        void DeleteRecord(int id);
    }
}
