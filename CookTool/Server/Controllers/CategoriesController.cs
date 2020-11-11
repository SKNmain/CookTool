using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookTool.Server.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CookTool.Shared;
using CookTool.Shared.Models;

namespace CookTool.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoriesRepository categoriesRepository = new CategoriesRepository();
        private readonly CategoryTypesRepository categoryTypesRepository = new CategoryTypesRepository();

        private IDictionary<string, IList<Category>> categories = new Dictionary<string, IList<Category>>();

        public IDictionary<string, IList<Category>> GetCategories()
        {
            if (categories.Count == 0) 
            {
                foreach(var categoryType in categoryTypesRepository.GetAllRecords())
                {
                    categories.Add(categoryType.TypeName, categoriesRepository.GetAllRecordsByCategoryTypeName(categoryType.TypeName));
                }
            }
            return categories;
        }

        [HttpGet("{id}")]
        public Category GetCategory(int id)
        {
            return categoriesRepository.GetRecordById(id);
        }

    }
}
