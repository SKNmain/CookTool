using CookTool.Server.Repositories;
using CookTool.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : Controller
    {
        private readonly UserIngredientsRepository uiRepository = new UserIngredientsRepository();
        private readonly MeasurementUnitsRepository munitsRepository = new MeasurementUnitsRepository();

        [HttpGet("{id}/ingredients")]
        [Authorize]
        public IDictionary<string, IList<UserIngredient>> GetUserIngredients(int id)
        {
            var uingredients = uiRepository.GetUserIngredients(id);
            var measureUnits = munitsRepository.GetAllRecords();
            var unitIngredients = new Dictionary<string, IList<UserIngredient>>();
            foreach (var munit in measureUnits)
            {
                unitIngredients.Add(munit.Name, uingredients.Where(ingr => ingr.MeasurementUnitId == munit.Id).ToList());
            }
            return unitIngredients;
        }

        [HttpGet("{id}/ingredientsnounits")]
        [Authorize]
        public IList<UserIngredient> GetUserIngredientsNoUnits(int id)
        {
            return uiRepository.GetUserIngredients(id);
        }

        [HttpPost]
        [Authorize]
        public void Post([FromBody] UserIngredient userIngredient)
        {
            uiRepository.AddRecord(userIngredient);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public void Delete(int id)
        {
            uiRepository.DeleteRecord(id);
        }
    }
}
