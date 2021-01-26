using CookTool.Shared.Models;
using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Server.Repositories
{
    public class BlackListedTokensRepository : IRepository<BlackListedToken>
    {
        private IDatabase db = DatabaseConnection.Instance;

        public void AddRecord(BlackListedToken record)
        {
            db.Insert(record);
        }

        public void DeleteRecord(int id)
        {
            throw new NotImplementedException();
        }

        public IList<BlackListedToken> GetAllRecords()
        {
            return db.Fetch<BlackListedToken>(SqlConstants.ALL_BLACKLISTED_TOKENS);
        }

        public BlackListedToken GetRecordById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateRecord(int id, BlackListedToken record)
        {
            throw new NotImplementedException();
        }
    }
}
