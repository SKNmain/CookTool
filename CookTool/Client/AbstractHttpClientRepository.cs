using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CookTool.Client.Shared.Models;
using CookTool.Shared.Authentication;
using CookTool.Shared.Models;

namespace CookTool.Client
{
    public abstract class AbstractHttpClientRepository
    {
        private readonly HttpClient _client;
        public AbstractHttpClientRepository(HttpClient client)
        {
            _client = client;
        }

        // --- AUTHENTICATION --- //

        public abstract Task<RegisterResult> Register(RegisterModel registerModel);
        public abstract Task<LoginResult> Login(LoginModel loginModel);
        public abstract Task Logout();

        // --- RECIPE --- //

        public async Task<List<Recipe>> GetRecipes()
        {
            var recipeData = await _client.GetAsync("recipes");
            var recipes = await recipeData.Content.ReadFromJsonAsync<List<Recipe>>();
            return recipes;
        }

        public async Task<List<Recipe>> GetPublicAndUserRecipes()
        {
            var currentUser = await GetCurrentUser();
            var recipeData = await _client.GetAsync($"recipes/{currentUser.Id}/publicandmine");
            var recipes = await recipeData.Content.ReadFromJsonAsync<List<Recipe>>();
            return recipes;
        }

        public async Task<Recipe> GetRecipe(string id)
        {
            var recipeData = await _client.GetAsync($"recipes/{id}");
            var recipe = await recipeData.Content.ReadFromJsonAsync<Recipe>();
            return recipe;
        }

        public async Task<List<Recipe>> GetUserRecipes()
        {
            var currentUser = await GetCurrentUser();
            var recipeData = await _client.GetAsync($"recipes/{currentUser.Id}/recipes");
            var recipes = await recipeData.Content.ReadFromJsonAsync<List<Recipe>>();
            return recipes;
        }

        public async Task<List<Recipe>> GetFilteredRecipes(string keyWord, string filters)
        {
            var recipeData = await _client.GetAsync($"recipes/{keyWord}/{filters}/filteredrecipes");
            var recipes = await recipeData.Content.ReadFromJsonAsync<List<Recipe>>();
            return recipes;
        }

        public async Task<Dictionary<string, List<Ingredient>>> GetRecipeIngredients(string id)
        {
            var data = await _client.GetAsync($"recipes/{id}/ingredients");
            var ingredients = await data.Content.ReadFromJsonAsync<Dictionary<string, List<Ingredient>>>();
            return ingredients;
        }

        public async Task<List<Ingredient>> GetRecipeIngredientsWithoutGrouping(string id)
        {
            var data = await _client.GetAsync($"recipes/{id}/ingredients/nogrouping");
            var ingredients = await data.Content.ReadFromJsonAsync<List<Ingredient>>();
            return ingredients;
        }

        public async Task<List<Category>> GetRecipeCategories(string id)
        {
            var data = await _client.GetAsync($"recipes/{id}/categories");
            var categories = await data.Content.ReadFromJsonAsync<List<Category>>();
            return categories;
        }

        public async Task DeleteUserRecipe(int recipeid)
        {
            await _client.DeleteAsync($"recipes/{recipeid}");
        }

        public async Task CreateUserRecipe(Recipe recipe, List<Category> categories, List<Ingredient> ingredients)
        {
            var currentUser = await GetCurrentUser();
            recipe.UserId = currentUser.Id;
            var data = await _client.PostAsJsonAsync("recipes", recipe);
            int id = await data.Content.ReadFromJsonAsync<int>();
            await _client.PostAsJsonAsync($"recipes/{id}/categories", categories);
            await _client.PostAsJsonAsync($"recipes/{id}/ingredients", ingredients);
        }

        public async Task UpdateUserRecipe(Recipe recipe, List<Category> categories, List<Ingredient> ingredients)
        {
            await _client.PutAsJsonAsync($"recipes/{recipe.Id}", recipe);
            await _client.PutAsJsonAsync($"recipes/{recipe.Id}/ingredients", ingredients);
            await _client.PutAsJsonAsync($"recipes/{recipe.Id}/categories", categories);
        }

        public async Task<List<Recipe>> GetRandomRecipes()
        {
            var currentUser = await GetCurrentUser();
            var recipeData = await _client.GetAsync($"recipes/random/{currentUser.Id}");
            var recipes = await recipeData.Content.ReadFromJsonAsync<List<Recipe>>();
            return recipes;
        }

        // --- RECIPE LIST --- //

        public async Task<List<RecipeList>> GetUserRecipeLists()
        {
            var currentUser = await GetCurrentUser();
            var data = await _client.GetAsync($"recipelists/{currentUser.Id}/recipelists");
            var recipelists = await data.Content.ReadFromJsonAsync<List<RecipeList>>();
            return recipelists;
        }

        public async Task<RecipeList> GetFavouriteRecipeList()
        {
            var currentUser = await GetCurrentUser();
            var data = await _client.GetAsync($"recipelists/{currentUser.Id}/favlist");
            var recipelist = await data.Content.ReadFromJsonAsync<RecipeList>();
            return recipelist;
        }

        public async Task<RecipeList> GetUserRecipeList(string title)
        {
            var currentUser = await GetCurrentUser();
            var data = await _client.GetAsync($"recipelists/{currentUser.Id}/{title}");
            var recipelist = await data.Content.ReadFromJsonAsync<RecipeList>();
            return recipelist;
        }

        public async Task<List<Recipe>> GetRecipeListRecipes(int recipelistid)
        {
            var data = await _client.GetAsync($"recipelists/{recipelistid}/recipes");
            var recipelistrecipes = await data.Content.ReadFromJsonAsync<List<Recipe>>();
            return recipelistrecipes;
        }

        public async Task AddRecipeToRecipeList(RecipeListRecipe recipelistrecipe)
        {
            await _client.PostAsJsonAsync("recipelists/recipe", recipelistrecipe);
        }

        public async Task RemoveRecipeFromRecipeList(int recipelistid, int recipeid)
        {
            await _client.DeleteAsync($"recipelists/{recipelistid}/recipe/{recipeid}");
        }

        public async Task AddRecipeList(RecipeList recipelist)
        {
            var currentUser = await GetCurrentUser();
            recipelist.UserId = currentUser.Id;
            await _client.PostAsJsonAsync("recipelists", recipelist);
        }

        public async Task EditRecipeList(int id, RecipeList recipeList)
        {
            await _client.PutAsJsonAsync($"recipelists/{id}", recipeList);
        }

        public async Task DeleteRecipeList(int id)
        {
            await _client.DeleteAsync($"recipelists/{id}");
        }

        // --- TIP --- //

        public async Task<List<Tip>> GetTips()
        {
            var tipData = await _client.GetAsync("tips");
            var tips = await tipData.Content.ReadFromJsonAsync<List<Tip>>();
            return tips;
        }

        public async Task<List<Tip>> GetUserTips()
        {
            var currentUser = await GetCurrentUser();
            var tipData = await _client.GetAsync($"tips/{currentUser.Id}/tips");
            var tips = await tipData.Content.ReadFromJsonAsync<List<Tip>>();
            return tips;
        }

        public async Task<List<Tip>> GetFilteredTips(string keyWord)
        {
            var tipData = await _client.GetAsync($"tips/{keyWord}/filteredtips");
            var tips = await tipData.Content.ReadFromJsonAsync<List<Tip>>();
            return tips;
        }

        public async Task<Tip> GetTip(string id)
        {
            var tipData = await _client.GetAsync($"tips/{id}");
            var tip = await tipData.Content.ReadFromJsonAsync<Tip>();
            return tip;
        }

        public async Task CreateUserTip(Tip tip)
        {
            var currentUser = await GetCurrentUser();
            tip.UserId = currentUser.Id;
            await _client.PostAsJsonAsync("tips", tip);
        }

        public async Task UpdateUserTip(Tip tip)
        {
            await _client.PutAsJsonAsync($"tips/{tip.Id}", tip);
        }

        public async Task DeleteUserTip(int tipid)
        {
            await _client.DeleteAsync($"tips/{tipid}");
        }

        // --- INGREDIENT --- //

        public async Task<Dictionary<string, List<UserIngredient>>> GetUserIngredients()
        {
            var currentUser = await GetCurrentUser();
            var iData = await _client.GetAsync($"ingredients/{currentUser.Id}/ingredients");
            var ings = await iData.Content.ReadFromJsonAsync<Dictionary<string, List<UserIngredient>>>();
            return ings;
        }

        public async Task<List<UserIngredient>> GetUserIngredientsWithoutUnits()
        {
            var currentUser = await GetCurrentUser();
            var iData = await _client.GetAsync($"ingredients/{currentUser.Id}/ingredientsnounits");
            var ings = await iData.Content.ReadFromJsonAsync<List<UserIngredient>>();
            return ings;
        }

        public async Task DeleteUserIngredient(int ingid)
        {
            await _client.DeleteAsync($"ingredients/{ingid}");
        }

        public async Task AddUserIngredient(IngredientItem item)
        {
            var currentUser = await GetCurrentUser();
            UserIngredient ui = new UserIngredient();
            ui.Name = item.Name;
            ui.Quantity = item.Quantity;
            ui.MeasurementUnitId = item.MeasurementUnitId;
            ui.UserId = currentUser.Id;
            await _client.PostAsJsonAsync("ingredients", ui);
        }

        // --- WEEK MENU --- //

        public async Task<WeekMenu> GetUserWeekMenu()
        {
            var currentUser = await GetCurrentUser();
            var weekMenuData = await _client.GetAsync($"weekmenus/{currentUser.Id}");
            var weekMenu = await weekMenuData.Content.ReadFromJsonAsync<WeekMenu>();
            return weekMenu;
        }

        public async Task UpdateWeekMenu(WeekMenu weekMenu)
        {
            await _client.PutAsJsonAsync($"weekmenus/{weekMenu.Id}", weekMenu);
        }

        // --- USER --- //

        public async Task<User> GetUser(string id)
        {
            var userData = await _client.GetAsync($"users/{id}");
            var user = await userData.Content.ReadFromJsonAsync<User>();
            return user;
        }

        public async Task UpdateUser(User user)
        {
            await _client.PutAsJsonAsync($"users/{user.Id}", user);
        }

        public async Task<ChangePasswordResult> ChangeUserPassword(ChangePasswordModel model)
        {
            var currentUser = await GetCurrentUser();
            var data = await _client.PutAsJsonAsync($"users/{currentUser.Id}/changepassword", model);
            var result = await data.Content.ReadFromJsonAsync<ChangePasswordResult>();
            return result;
        }

        public abstract Task<User> GetCurrentUser();

        // --- OTHER --- //

        public async Task<List<Category>> GetCategories()
        {
            var data = await _client.GetAsync("categories");
            var categories = await data.Content.ReadFromJsonAsync<List<Category>>();
            return categories;
        }

        public async Task<Dictionary<string, List<Category>>> GetCategoriesWithType()
        {
            var data = await _client.GetAsync("categories/withtype");
            var categories = await data.Content.ReadFromJsonAsync<Dictionary<string, List<Category>>>();
            return categories;
        }

        public async Task<List<MeasurementUnit>> GetMeasurementUnits()
        {
            var data = await _client.GetAsync("measurementunits");
            var units = await data.Content.ReadFromJsonAsync<List<MeasurementUnit>>();
            return units;
        }
    }
}
