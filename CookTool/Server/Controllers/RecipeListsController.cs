using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using CookTool.Server.Repositories;
using CookTool.Shared.Models;
using System.Diagnostics;
using CookTool.Server.Helpers;

namespace CookTool.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RecipeListsController : Controller
    {
        private readonly RecipeListsRepository recipeListsRepository = new RecipeListsRepository();
        private readonly RecipeListRecipeRepository recipeListRecipeRepository = new RecipeListRecipeRepository();

        [HttpGet("{userid}/recipelists")]
        [Authorize]
        public IList<RecipeList> GetUserRecipeLists(int userid)
        {
            AuthHelper.CheckTokenBlackListed(Request);
            return recipeListsRepository.GetUserRecords(userid);
        }

        [HttpGet("{userid}/favlist")]
        [Authorize]
        public RecipeList GetUserFavList(int userid)
        {
            AuthHelper.CheckTokenBlackListed(Request);
            return recipeListsRepository.GetFavRecipeList(userid);
        }

        [HttpGet("{recipelistid}/recipes")]
        [Authorize]
        public IList<Recipe> GetRecipeListRecipes(int recipelistid)
        {
            AuthHelper.CheckTokenBlackListed(Request);
            return recipeListsRepository.GetRecipeListRecipes(recipelistid);
        }

        [HttpGet("{userid}/{title}")]
        [Authorize]
        public RecipeList GetUserList(int userid, string title)
        {
            AuthHelper.CheckTokenBlackListed(Request);
            return recipeListsRepository.GetRecordByTitle(userid, title);
        }

        [HttpPost("recipe")]
        [Authorize]
        public void AddRecipeToRecipeList([FromBody] RecipeListRecipe recipeListRecipe)
        {
            AuthHelper.CheckTokenBlackListed(Request);
            recipeListRecipeRepository.AddRecord(recipeListRecipe);
        }

        [HttpDelete("{recipelistid}/recipe/{recipeid}")]
        [Authorize]
        public void DeleteRecipeFromRecipeList(int recipeid, int recipelistid)
        {
            AuthHelper.CheckTokenBlackListed(Request);
            recipeListRecipeRepository.DeleteSpecialRecord(recipeid, recipelistid);
        }

        [HttpPost]
        [Authorize]
        public void Post([FromBody] RecipeList recipeList)
        {
            AuthHelper.CheckTokenBlackListed(Request);
            recipeListsRepository.AddRecord(recipeList);
        }

        [HttpPut("{id}")]
        [Authorize]
        public void Put(int id, [FromBody] RecipeList recipeList)
        {
            AuthHelper.CheckTokenBlackListed(Request);
            recipeListsRepository.UpdateRecord(id, recipeList);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public void Delete(int id)
        {
            AuthHelper.CheckTokenBlackListed(Request);
            recipeListsRepository.DeleteRecord(id);
        }
    }
}
