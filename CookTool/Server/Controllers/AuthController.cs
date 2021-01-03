using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CookTool.Server.Helpers;
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
        private readonly RecipeListsRepository recipeListsRepository = new RecipeListsRepository();
        private readonly WeekMenusRepository weekMenuRepository = new WeekMenusRepository();

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("register")]
        public RegisterResult Register([FromBody] RegisterModel model)
        {
            var newUser = new User { Nickname = model.Nickname, Email = model.Email, Password = CryptHelper.Encrypt(model.Password) };
        
            try{ 
                usersRepository.AddRecord(newUser);
                var id = usersRepository.GetRecordByEmail(newUser.Email).Id;
                PrepareFaveRecipeList(id);
                PrepareWeekMenu(id);
            }
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

            if(user == null || !AuthHelper.IsPasswordCorrect(model.Password, user.Password))
            {
                return new LoginResult { Successful = false, Error = "Email or password are invalid"};
            }

            var claims = new[] { new Claim(ClaimTypes.Name, model.Email) };

            return new LoginResult { Successful = true, Token = AuthHelper.GenerateToken(claims, _configuration), Nickname = user.Nickname, Image = Convert.ToBase64String(user.Image) };
        }

        private void PrepareFaveRecipeList(int userid)
        {
            var recipeList = new RecipeList();
            recipeList.Title = "Fave";
            recipeList.UserId = userid;
            recipeListsRepository.AddRecord(recipeList);
        }

        private void PrepareWeekMenu(int userid)
        {
            var weekMenu = new WeekMenu();
            weekMenu.UserId = userid;
            weekMenu.WeekMenuData = WeekMenuHelper.PrepareWeekMenuData();
            weekMenuRepository.AddRecord(weekMenu);
        }
    }
}
