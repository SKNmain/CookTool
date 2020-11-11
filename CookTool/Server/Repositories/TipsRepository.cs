using CookTool.Shared.Models;
using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Server.Repositories
{
    public class TipsRepository : IRepository<Tip>
    {
        private IDatabase db = DatabaseConnection.Instance;

        public IList<Tip> GetAllRecords()
        {
            return db.Fetch<Tip>(SqlConstants.ALL_TIPS);
        }

        public IList<Tip> GetUserRecords(int userId)
        {
            return db.Fetch<Tip>(SqlConstants.USER_TIPS(userId));
        }

        public Tip GetRecordById(int id)
        {
            return db.SingleById<Tip>(id);
        }

        public void AddRecord(Tip record)
        {
            db.Insert(record);
        }

        public void DeleteRecord(int id)
        {
            db.Delete<Tip>(id);
        }

        public void UpdateRecord(int id, Tip record)
        {
            record.Id = id;
            db.Update(record);
        }
    }
}
