using CookTool.Shared.Models;
using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Server.Repositories
{
    public class TipCommentsRepository : IRepository<TipComment>
    {
        private IDatabase db = DatabaseConnection.Instance;

        public IList<TipComment> GetAllRecords()
        {
            return db.Fetch<TipComment>(SqlConstants.ALL_TIP_COMMENTS);
        }

        public TipComment GetRecordById(int id)
        {
            return db.SingleById<TipComment>(id);
        }

        public IList<TipComment> GetUserRecords(int userid)
        {
            return db.Fetch<TipComment>(SqlConstants.USER_TIP_COMMENTS(userid));
        }

        public IList<TipComment> GetTipRecords(int tipid)
        {
            return db.Fetch<TipComment>(SqlConstants.TIP_COMMENTS(tipid));
        }

        public void AddRecord(TipComment record)
        {
            db.Insert(record);
        }

        public void DeleteRecord(int id)
        {
            db.Delete<RecipeComment>(id);
        }

        public void UpdateRecord(int id, TipComment record)
        {
            record.Id = id;
            db.Update(record);
        }
    }
}
