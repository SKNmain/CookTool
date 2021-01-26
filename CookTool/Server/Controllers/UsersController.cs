using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CookTool.Server.Helpers;
using CookTool.Server.Repositories;
using CookTool.Shared.Authentication;
using CookTool.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Pkcs;

namespace CookTool.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersRepository usersRepository = new UsersRepository();

        [HttpGet("{id}")]
        public User GetUser(int id)
        {
            return usersRepository.GetRecordById(id);
        }

        [HttpPost("current")]
        [Authorize]
        public User GetUserByEmail([FromBody] string email)
        {
            AuthHelper.CheckTokenBlackListed(Request);
            return usersRepository.GetRecordByEmail(email);
        }

        [HttpPost]
        public void Post([FromBody] User user)
        {
            user.Password = CryptHelper.Encrypt(user.Password);
            usersRepository.AddRecord(user);
        }

        [HttpPut("{id}")]
        [Authorize]
        public void Put(int id, [FromBody] User user)
        {
            AuthHelper.CheckTokenBlackListed(Request);
            usersRepository.UpdateRecord(id, user);
        }

        [HttpPut("{id}/changepassword")]
        [Authorize]
        public ChangePasswordResult ChangePassword(int id, [FromBody] ChangePasswordModel model)
        {
            AuthHelper.CheckTokenBlackListed(Request);

            var user = usersRepository.GetRecordById(id);
            if (AuthHelper.IsPasswordCorrect(model.OldPassword, user.Password))
            {
                user.Password = CryptHelper.Encrypt(model.NewPassword);
                usersRepository.UpdateRecord(id, user);
                return new ChangePasswordResult() { Successful = true };
            }
            return new ChangePasswordResult() { Error = "Cannot change password!" };
        }
    }
}
