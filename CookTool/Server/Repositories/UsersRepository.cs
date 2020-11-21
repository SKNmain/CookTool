using CookTool.Shared.Models;
using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Server.Repositories
{
    public class UsersRepository : IRepository<User>
    {
        private readonly IDatabase db = DatabaseConnection.Instance;

        public IList<User> GetAllRecords()
        {
            return db.Fetch<User>(SqlConstants.ALL_USERS);
        }

        public User GetRecordById(int id)
        {
            return db.SingleById<User>(id);
        }

        public User GetRecordByNickname(string nickname)
        {
            return db.Fetch<User>(SqlConstants.USER_BY_NICKNAME(nickname)).First();
        }

        public User GetRecordByEmail(string email)
        {
            email = email.Insert(email.IndexOf('@'), "@");
            return db.Fetch<User>(SqlConstants.USER_BY_EMAIL(email)).First();
        }

        public void AddRecord(User record)
        {
            db.Insert(record);
        }

        public void DeleteRecord(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateRecord(int id, User record)
        {
            record.Id = id;
            db.Update(record);
        }
    }
}
