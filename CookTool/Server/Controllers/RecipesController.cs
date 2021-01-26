using System;
using System.Collections.Generic;
using System.Linq;
using CookTool.Server.Repositories;
using CookTool.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using CookTool.Server.Helpers;

namespace CookTool.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly RecipesRepository recipesRepository = new RecipesRepository();
        private readonly MeasurementUnitsRepository munitsRepository = new MeasurementUnitsRepository();
        private readonly RecipeCategoryRepository recipeCategoryRepository = new RecipeCategoryRepository();
        private readonly IngredientsRepository ingredientsRepository = new IngredientsRepository();
        private readonly CategoriesRepository categoriesRepository = new CategoriesRepository();
        private readonly UserIngredientsRepository userIngredientsRepository = new UserIngredientsRepository();

        public IList<Recipe> GetBestRatingRecipes()
        {
            return recipesRepository.GetBestRatingRecords();
        }

        [HttpGet("{userid}/publicandmine")]
        [Authorize]
        public IList<Recipe> GetPublicAndUser(int userid)
        {
            AuthHelper.CheckTokenBlackListed(Request);
            return recipesRepository.GetBestRatingRecords().Concat(recipesRepository.GetUserRecords(userid)).GroupBy(recipe => recipe.Id).Select(group => group.First()).ToArray();
        }

        [HttpGet("{keyWord}/{filters}/filteredrecipes")]
        public IList<Recipe> GetFilteredRecipes(string keyWord, string filters)
        {
            var recipes = recipesRepository.GetAllRecords();
            List<Recipe> filtered = new List<Recipe>();
            if (filters.Contains("Titles")) filtered.AddRange(recipes.Where(recipe => LevensteinDistance.FulfillRules(recipe.Title, keyWord) == true).ToList());
            if (filters.Contains("Categories"))
            {
                var categories = categoriesRepository.GetAllRecords().Where(cat => LevensteinDistance.FulfillRules(cat.Name, keyWord) == true).Select(cat => cat.Id).ToList();
                var recipecategory = recipeCategoryRepository.GetAllRecords().Where(rc => categories.Contains(rc.CategoryId) == true).Select(rc => rc.RecipeId).ToList();
                filtered.AddRange(recipes.Where(recipe => recipecategory.Contains(recipe.Id) == true).ToList());
            }
            if (filters.Contains("Ingredients"))
            {
                var ingredients = ingredientsRepository.GetAllRecords().Where(ing => LevensteinDistance.FulfillRules(ing.Name, keyWord) == true).Select(ing => ing.RecipeId).ToList();
                filtered.AddRange(recipes.Where(recipe => ingredients.Contains(recipe.Id) == true).ToList());
            }

            return filtered;
        }

        [HttpGet("{id}")]
        public Recipe GetRecipe(int id)
        {
            return recipesRepository.GetRecordById(id);
        }

        [HttpGet("{id}/categories")]
        public IList<Category> GetRecipeCategories(int id)
        {
            return recipesRepository.GetRecipeCategories(id);
        }

        [HttpGet("{id}/ingredients")]
        public IDictionary<string, IList<Ingredient>> GetRecipeIngredients(int id)
        {
            var ingredients = recipesRepository.GetRecipeIngredients(id);
            var measureUnits = munitsRepository.GetAllRecords();
            var unitIngredients = new Dictionary<string, IList<Ingredient>>();
            foreach (var munit in measureUnits)
            {
                unitIngredients.Add(munit.Name, ingredients.Where(ingr => ingr.MeasurementUnitId == munit.Id).ToList());
            }
            return unitIngredients;
        }

        [HttpGet("{id}/ingredients/nogrouping")]
        public IList<Ingredient> GetRecipeIngredientsWithoutGrouping(int id)
        {
            return recipesRepository.GetRecipeIngredients(id);
        }


        [HttpGet("random/{userid}")]
        [Authorize]
        public IList<Recipe> GetRandomRecipes(int userid)
        {
            AuthHelper.CheckTokenBlackListed(Request);
            var userIngredients = userIngredientsRepository.GetUserIngredients(userid).Select(ing => ing.Name).ToList();
            var recipes = recipesRepository.GetBestRatingRecords().Concat(recipesRepository.GetUserRecords(userid)).GroupBy(recipe => recipe.Id).Select(group => group.First()).ToArray();
            var recipeIds = RecipeHelper.FilterRecipeIds(userIngredients, ingredientsRepository);
            return recipes.Where(recipe => recipeIds.Contains(recipe.Id) == true).OrderBy(recipe => recipeIds.IndexOf(recipe.Id)).Take(5).ToList();
        }

        [HttpGet("{userid}/recipes")]
        [Authorize]
        public IList<Recipe> GetUserRecipes(int userid)
        {
            AuthHelper.CheckTokenBlackListed(Request);
            return recipesRepository.GetUserRecords(userid);
        }

        [HttpPost]
        [Authorize]
        public int Post([FromBody] Recipe recipe)
        {
            AuthHelper.CheckTokenBlackListed(Request);
            recipesRepository.AddRecord(recipe);
            return recipesRepository.GetUserRecords(recipe.UserId).Where(rec => rec.Title.Equals(recipe.Title)).First().Id;
        }

        [HttpPost("{id}/categories")]
        [Authorize]
        public void PostCategories(int id, [FromBody] List<Category> categories)
        {
            AuthHelper.CheckTokenBlackListed(Request);
            categories.ForEach(cat => {
                var recipeCategory = new RecipeCategory();
                recipeCategory.RecipeId = id;
                recipeCategory.CategoryId = cat.Id;
                recipeCategoryRepository.AddRecord(recipeCategory);
            });            
        }

        [HttpPost("{id}/ingredients")]
        [Authorize]
        public void PostIngredients(int id, [FromBody] List<Ingredient> ingredients)
        {
            AuthHelper.CheckTokenBlackListed(Request);
            ingredients.ForEach(ing => {
                ing.RecipeId = id;
                ingredientsRepository.AddRecord(ing);
            });
        }

        [HttpPut("{id}")]
        [Authorize]
        public void Put(int id, [FromBody] Recipe recipe)
        {
            AuthHelper.CheckTokenBlackListed(Request);
            recipesRepository.UpdateRecord(id, recipe);
        }

        [HttpPut("{id}/categories")]
        [Authorize]
        public void PutCategories(int id, [FromBody] List<Category> categories)
        {
            AuthHelper.CheckTokenBlackListed(Request);
            var recipeCategoryIds = recipesRepository.GetRecipeCategories(id).Select(ing => ing.Id).ToList();

            categories.ForEach(cat =>
            {
                if (!recipeCategoryIds.Contains(cat.Id))
                {
                    var recipeCategory = new RecipeCategory();
                    recipeCategory.RecipeId = id;
                    recipeCategory.CategoryId = cat.Id;
                    recipeCategoryRepository.AddRecord(recipeCategory);
                }
                recipeCategoryIds.Remove(cat.Id);
            });

            recipeCategoryIds.ForEach(catId => recipeCategoryRepository.DeleteRecord(id, catId));
        }

        [HttpPut("{id}/ingredients")]
        [Authorize]
        public void PutIngredients(int id, [FromBody] List<Ingredient> ingredients)
        {
            AuthHelper.CheckTokenBlackListed(Request);
            var recipeIngredientIds = recipesRepository.GetRecipeIngredients(id).Select(ing => ing.Id).ToList();
            ingredients.ForEach(ing =>
            {
                if (recipeIngredientIds.Contains(ing.Id))
                {
                    ingredientsRepository.UpdateRecord(ing.Id, ing);
                    recipeIngredientIds.Remove(ing.Id);
                }
                else
                {
                    ing.RecipeId = id;
                    ingredientsRepository.AddRecord(ing);
                }

            });

            recipeIngredientIds.ForEach(id => ingredientsRepository.DeleteRecord(id));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public void Delete(int id)
        {
            AuthHelper.CheckTokenBlackListed(Request);
            recipesRepository.DeleteRecord(id);
        }
    }
}
