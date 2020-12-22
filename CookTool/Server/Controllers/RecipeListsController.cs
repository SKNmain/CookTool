using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using CookTool.Server.Repositories;
using CookTool.Shared.Models;
using System.Diagnostics;

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
            return recipeListsRepository.GetUserRecords(userid);
        }

        [HttpGet("{userid}/favlist")]
        [Authorize]
        public RecipeList GetUserFavList(int userid)
        {
            return recipeListsRepository.GetFavRecipeList(userid);
        }

        [HttpGet("{recipelistid}/recipes")]
        [Authorize]
        public IList<Recipe> GetRecipeListRecipes(int recipelistid)
        {
            return recipeListsRepository.GetRecipeListRecipes(recipelistid);
        }

        [HttpGet("{userid}/{title}")]
        [Authorize]
        public RecipeList GetUserList(int userid, string title)
        {
            return recipeListsRepository.GetRecordByTitle(userid, title);
        }

        [HttpPost("recipe")]
        [Authorize]
        public void AddRecipeToRecipeList([FromBody] RecipeListRecipe recipeListRecipe)
        {
            recipeListRecipeRepository.AddRecord(recipeListRecipe);
        }

        [HttpDelete("{recipelistid}/recipe/{recipeid}")]
        [Authorize]
        public void DeleteRecipeFromRecipeList(int recipeid, int recipelistid)
        {
            recipeListRecipeRepository.DeleteSpecialRecord(recipeid, recipelistid);
        }

        [HttpPost]
        [Authorize]
        public void Post([FromBody] RecipeList recipeList)
        {
            recipeListsRepository.AddRecord(recipeList);
        }

        [HttpPut("{id}")]
        [Authorize]
        public void Put(int id, [FromBody] RecipeList recipeList)
        {
            recipeListsRepository.UpdateRecord(id, recipeList);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public void Delete(int id)
        {
            recipeListsRepository.DeleteRecord(id);
        }
    }
}
