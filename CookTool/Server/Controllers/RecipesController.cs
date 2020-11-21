using System;
using System.Collections.Generic;
using System.Linq;
using CookTool.Server.Repositories;
using CookTool.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace CookTool.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly RecipesRepository recipesRepository = new RecipesRepository();
        private readonly MeasurementUnitsRepository munitsRepository = new MeasurementUnitsRepository();

        public IList<Recipe> GetBestRatingRecipes()
        {
            return recipesRepository.GetBestRatingRecords();
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

        [HttpPost]
        public void Post([FromBody] Recipe recipe)
        {
            recipesRepository.AddRecord(recipe);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Recipe recipe)
        {
            if (ModelState.IsValid) recipesRepository.UpdateRecord(id, recipe);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            recipesRepository.DeleteRecord(id);
        }
    }
}
