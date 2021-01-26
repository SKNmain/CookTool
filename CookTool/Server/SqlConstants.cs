using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Server
{
    public static class SqlConstants
    {
        // --- CATEGORIES REPOSITORY --- //

        public static readonly string ALL_CATEGORIES = "SELECT c.id, c.Name, ct.Id, ct.typename FROM categories AS c inner join CategoryTypes AS ct ON c.typeid = ct.id";
        public static readonly Func<string, string> ALL_CATEGORY_TYPE_CATEGORIES = (string name) 
            => $"SELECT c.id, c.Name, ct.Id, ct.typename FROM Categories AS c INNER JOIN CategoryTypes AS ct ON c.typeid = ct.id WHERE ct.typename = '{name}'";

        // --- CATEGORY TYPES REPOSITORY --- //

        public static readonly string ALL_CATEGORY_TYPES = "SELECT * FROM categorytypes";
        public static readonly Func<string, string> CATEGORY_TYPE_BY_NAME = (typename) 
            => $"SELECT * FROM categorytypes WHERE typename = {typename}";

        // --- INGREDIENTS REPOSITORY --- //

        public static readonly string ALL_INGREDIENTS = "SELECT * FROM ingredients";
        public static readonly Func<string, string> INGREDIENT_BY_NAME = (name) 
            => $"SELECT * FROM ingredients WHERE name = {name}";

        // --- MEASUREMENT UNITS REPOSITORY --- //

        public static readonly string ALL_MEASUREMENT_UNITS = "SELECT * FROM MeasurementUnits";
        public static readonly Func<string, string> MEASUREMENT_UNIT_BY_NAME = (name)
            => $"SELECT * FROM MeasurementUnits WHERE name = {name}";

        // --- RECIPE COMMENTS REPOSITORY --- //

        public static readonly string ALL_RECIPE_COMMENTS = "SELECT * FROM recipecomments";
        public static readonly Func<int, string> USER_RECIPE_COMMENTS = (userid)
            => $"SELECT * FROM recipecomments WHERE userid = {userid}";
        public static readonly Func<int, string> RECIPE_COMMENTS = (recipeid)
            => $"SELECT * FROM recipecomments WHERE recipeid = {recipeid}";

        // --- RECIPE LISTS REPOSITORY --- //

        public static readonly Func<int, string> USER_RECIPE_LISTS = (userId)
            => $"SELECT * FROM RecipeLists WHERE userid = {userId}";
        public static readonly Func<int, string, string> USER_RECIPE_LIST_BY_TITLE = (userId, title)
            =>  $"SELECT * FROM RecipeLists WHERE userid = {userId} AND title = '{title}'";
        public static readonly Func<int, string> USER_RECIPE_LIST_FAVE = (userId)
            => $"SELECT * FROM RecipeLists WHERE userid = {userId} AND title = 'Fave'";
        public static readonly Func<int, string> RECIPE_LIST_RECIPES = (id)
            => $"SELECT * FROM Recipes AS r INNER JOIN RecipeListRecipe AS rr ON r.id = rr.recipeid WHERE rr.recipelistid = {id}";

        // --- RECIPES REPOSITORY --- //

        public static readonly string ALL_RECIPES = "SELECT * FROM recipes";
        public static readonly Func<int, string> USER_RECIPES = (userId)
            => $"SELECT * FROM Recipes WHERE userid = {userId}";
        public static readonly Func<int, string> RECIPE_CATEGORIES = (id)
            => $"SELECT * FROM categories AS c INNER JOIN recipecategory AS rc ON rc.categoryid = c.id WHERE rc.recipeid = {id}";
        public static readonly Func<int, string> RECIPE_INGREDIENTS = (id)
            => $"SELECT * FROM Ingredients AS i WHERE i.recipeid = {id}";

        // --- TIP COMMENTS REPOSITORY --- //

        public static readonly string ALL_TIP_COMMENTS = "SELECT * FROM tipcomments";
        public static readonly Func<int, string> USER_TIP_COMMENTS = (userid)
            => $"SELECT * FROM tipcomments WHERE userid = {userid}";
        public static readonly Func<int, string> TIP_COMMENTS = (tipid)
            => $"SELECT * FROM tipcomments WHERE tipid = {tipid}";

        // --- TIPS REPOSITORY --- //

        public static readonly string ALL_TIPS = "SELECT * FROM tips";
        public static readonly Func<int, string> USER_TIPS = (userId)
            => $"SELECT * FROM Tips WHERE userid = {userId}";

        // --- WEEKMENUS REPOSITORY --- //

        public static readonly string ALL_WEEK_MENUS = "SELECT * FROM weekmenus";
        public static readonly Func<int, string> USER_WEEK_MENU = (userid)
            => $"SELECT * FROM weekmenus WHERE userid = {userid}";

        // --- USERS REPOSITORY --- //

        public static readonly string ALL_USERS = "SELECT * FROM Users";
        public static readonly Func<string, string> USER_BY_NICKNAME = (nickname)
            => $"SELECT * FROM Users WHERE Nickname = {nickname}";
        public static readonly Func<string, string> USER_BY_EMAIL = (email)
            => $"SELECT * FROM Users WHERE Email = '{email}'";

        // --- USER INGREDIENTS REPOSITORY --- //

        public static readonly Func<int, string> USER_INGREDIENTS = (userid)
            => $"SELECT * FROM UserIngredients WHERE userid = {userid}";

        // --- RECIPE CATEGORY REPOSITORY --- //

        public static readonly string ALL_RECIPE_CATEGORIES = "SELECT * FROM RecipeCategory";

        public static string ALL_BLACKLISTED_TOKENS = "SELECT * FROM BlackListedTokens";
    }
}
