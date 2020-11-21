using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CookTool.Server.Repositories;
using CookTool.Shared.Models;
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
        public User GetUserByEmail([FromBody] string email)
        {
            return usersRepository.GetRecordByEmail(email);
        }

        [HttpPost]
        public void Post([FromBody] User user)
        {
            user.Password = CryptHelper.Encrypt(user.Password);
            usersRepository.AddRecord(user);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User user)
        {
            //user.Password = Encrypt(user.Password);
            usersRepository.UpdateRecord(id, user);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            usersRepository.DeleteRecord(id);
        }
    }
}
