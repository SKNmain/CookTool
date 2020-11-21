using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CookTool.Server.Repositories;
using CookTool.Shared.Authentication;
using CookTool.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CookTool.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UsersRepository usersRepository = new UsersRepository();

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("register")]
        public RegisterResult Register([FromBody] RegisterModel model)
        {
            var newUser = new User { Nickname = model.Nickname, Email = model.Email, Password = CryptHelper.Encrypt(model.Password) };
        
            try{ usersRepository.AddRecord(newUser); }
            catch(Exception e)
            {
                return new RegisterResult { Successful = false, Error = e.Message };
            }
            return new RegisterResult { Successful = true };
        }

        [HttpPost("login")]
        public LoginResult LogIn([FromBody] LoginModel model)
        {
            var user = usersRepository.GetRecordByEmail(model.Email);

            if(user == null || !IsPasswordCorrect(model.Password, user.Password))
            {
                return new LoginResult { Successful = false, Error = "Email or password are invalid"};
            }

            var claims = new[] { new Claim(ClaimTypes.Name, model.Email) };

            return new LoginResult { Successful = true, Token = GenerateToken(claims), Nickname = user.Nickname, Image = Convert.ToBase64String(user.Image) };
        }

        private bool IsPasswordCorrect(string inputPassword, string userPassword)
        {
            return inputPassword.Equals(CryptHelper.Decrypt(userPassword));
        }

        private string GenerateToken(Claim[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtExpiryInDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtAudience"],
                claims,
                expires: expiry,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
