using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookTool.Server.Repositories;
using CookTool.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public void Post([FromBody] User user)
        {
            usersRepository.AddRecord(user);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User user)
        {
            usersRepository.UpdateRecord(id, user);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            usersRepository.DeleteRecord(id);
        }

    }
}
