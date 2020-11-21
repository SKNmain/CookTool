using Blazored.LocalStorage;
using CookTool.Shared.Authentication;
using CookTool.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace CookTool.Client
{
    public class HttpClientRepository : IHttpClientRepository
    {
        private readonly HttpClient _client;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public HttpClientRepository(HttpClient client, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
        {
            _client = client;
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<LoginResult> Login(LoginModel loginModel)
        {
         
            var response = await _client.PostAsJsonAsync("auth/login", loginModel);
            var loginResult = await response.Content.ReadFromJsonAsync<LoginResult>();

            if (!response.IsSuccessStatusCode)
            {
                return loginResult;
            }

            await _localStorage.SetItemAsync("authToken", loginResult.Token);

            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginModel.Email, loginResult.Nickname, loginResult.Image);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Token);

            return loginResult;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _client.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<RegisterResult> Register(RegisterModel registerModel)
        {
            var result = await _client.PostAsJsonAsync("auth/register", registerModel);
            var response = await result.Content.ReadFromJsonAsync<RegisterResult>();

            return response;
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
            var user = await userData.Content.ReadFromJsonAsync<User>();
            return user;
        }

        public async Task<User> GetCurrentUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            
            User user = null;
            var userAuth = authState.User;
            if (userAuth.Identity.IsAuthenticated)
            {
                var userData = await _client.PostAsJsonAsync($"users/current", userAuth.Identity.Name);
                user = await userData.Content.ReadFromJsonAsync<User>();
            } 
            return user;
        }
    }
}
