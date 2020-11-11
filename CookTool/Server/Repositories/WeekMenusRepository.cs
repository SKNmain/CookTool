using CookTool.Shared.Models;
using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Server.Repositories
{
    public class WeekMenusRepository : IRepository<WeekMenu>
    {
        private IDatabase db = DatabaseConnection.Instance;
        public IList<WeekMenu> GetAllRecords()
        {
            return db.Fetch<WeekMenu>(SqlConstants.ALL_WEEK_MENUS);
        }

        public WeekMenu GetRecordById(int id)
        {
            return db.SingleById<WeekMenu>(id);
        }

        public WeekMenu GetRecordByUserId(int userid)
        {
            return db.Fetch<WeekMenu>(SqlConstants.USER_WEEK_MENU(userid)).First();
        }

        public void AddRecord(WeekMenu record)
        {
            db.Insert(record);
        }

        public void DeleteRecord(int id)
        {
            db.Delete<WeekMenu>(id);
        }

        public void UpdateRecord(int id, WeekMenu record)
        {
            record.Id = id;
            db.Update(record);
        }
    }
}
