using CookTool.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CookTool.Client
{
    public class HttpClientRepository : IHttpClientRepository
    {
        private readonly HttpClient _client;
        public HttpClientRepository(HttpClient client)
        {
            _client = client;
        }

        public async Task<Dictionary<string, List<Category>>> GetCategories()
        {
            var data = await _client.GetAsync("categories");
            var categories =  await data.Content.ReadFromJsonAsync<Dictionary<string, List<Category>>>();
            return categories;
        }

        public async Task<Recipe> GetRecipe(string id)
        {
            var recipeData = await _client.GetAsync($"recipes/{id}");
            var recipe = await recipeData.Content.ReadFromJsonAsync<Recipe>();
            return recipe;
        }

        public async Task<List<Category>> GetRecipeCategories(string id)
        {
            var data = await _client.GetAsync($"recipes/{id}/categories");
            var categories = await data.Content.ReadFromJsonAsync<List<Category>>();
            return categories;
        }

        public async Task<Dictionary<string, List<Ingredient>>> GetRecipeIngredients(string id)
        {
            var data = await _client.GetAsync($"recipes/{id}/ingredients");
            var ingredients = await data.Content.ReadFromJsonAsync<Dictionary<string, List<Ingredient>>>();
            return ingredients;
        }

        public async Task<List<Recipe>> GetRecipes()
        {
            var recipeData = await _client.GetAsync("recipes");
            var recipes = await recipeData.Content.ReadFromJsonAsync<List<Recipe>>();
            return recipes;
        }

        public async Task<List<Tip>> GetTips()
        {
            var tipData = await _client.GetAsync("tips");
            var tips = await tipData.Content.ReadFromJsonAsync<List<Tip>>();
            return tips;
        }

        public async Task<User> GetUser(string id)
        {
            var userData = await _client.GetAsync($"users/{id}");
            Console.WriteLine(userData);
            var user = await userData.Content.ReadFromJsonAsync<User>();
            return user;
        }
    }
}
