using CookTool.Shared.Authentication;
using CookTool.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Client
{
    public interface IHttpClientRepository
    {
        Task<RegisterResult> Register(RegisterModel registerModel);
        Task<LoginResult> Login(LoginModel loginModel);
        Task Logout();
        Task<Dictionary<string, List<Category>>> GetCategories();
        Task<List<Recipe>> GetRecipes();
        Task<List<Tip>> GetTips();
        Task<Recipe> GetRecipe(string id);
        Task<Dictionary<string, List<Ingredient>>> GetRecipeIngredients(string id);
        Task<List<Category>> GetRecipeCategories(string id);
        Task<User> GetUser(string id);
        Task<User> GetCurrentUser();
    }
}
