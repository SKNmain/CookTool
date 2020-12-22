using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookTool.Server.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CookTool.Shared;
using CookTool.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;

namespace CookTool.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoriesRepository categoriesRepository = new CategoriesRepository();
        private readonly CategoryTypesRepository categoryTypesRepository = new CategoryTypesRepository();

        private Dictionary<string, IList<Category>> categoriesWithType;
        private List<Category> categories;

        public List<Category> GetCategories()
        {
            if (categories != null) return categories;
            categories = new List<Category>();
            foreach (var category in categoriesRepository.GetAllRecords())
            {
                categories.Add(category);
            }
            return categories;
        }

        [HttpGet("withtype")]
        public Dictionary<string, IList<Category>> GetCategoriesWithType()
        {
            if (categoriesWithType != null) return categoriesWithType;
            categoriesWithType = new Dictionary<string, IList<Category>>();
            foreach(var categoryType in categoryTypesRepository.GetAllRecords())
            {
                categoriesWithType.Add(categoryType.TypeName, categoriesRepository.GetAllRecordsByCategoryTypeName(categoryType.TypeName));
            }
            return categoriesWithType;
        }

       

        [HttpGet("{id}")]
        public Category GetCategory(int id)
        {
            return categoriesRepository.GetRecordById(id);
        }

    }
}
