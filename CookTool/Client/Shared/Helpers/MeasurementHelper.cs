using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Client.Shared.Helpers
{
    public class MeasurementHelper
    {
        private static double GLASS_MAX = 250.0;
        private static double SPOON_MAX = 15.0;
        private static double TEASPOON_MAX = 5.0;

        private static string ML = "mililiters";
        private static string G = "grams";
        private static string GL = "glasses(250ml)";
        private static string S = "spoons(15ml)";
        private static string TS = "teaspoons(5ml)";

        private static Dictionary<int, string> Values = new Dictionary<int, string>()
        {
            { 1, ML },
            { 2, G },
            { 3, GL },
            { 4, S },
            { 5, TS },
        };

        private static Dictionary<string, Func<Models.IngredientItem, Models.IngredientItem, Dictionary<string, Models.IngredientItem>>> Methods
            = new Dictionary<string, Func<Models.IngredientItem, Models.IngredientItem, Dictionary<string, Models.IngredientItem>>>()
        {
            {ML, Mililiters},
            {G, Grams},
            {GL, Glasses},
            {S, Spoons},
            {TS, Teaspoons},
        };

        public static Dictionary<string, Models.IngredientItem> ConvertUnit(Models.IngredientItem currentItem, Models.IngredientItem basicItem)
        {
            return Methods[Values[currentItem.MeasurementUnitId]].Invoke(currentItem, basicItem);
        }

        public static List<CookTool.Shared.Models.MeasurementUnit> PrepareUnits()
        {
            var result = new List<CookTool.Shared.Models.MeasurementUnit>();

            foreach (var item in Values)
            {
                var mu = new CookTool.Shared.Models.MeasurementUnit();
                mu.Id = item.Key;
                mu.Name = item.Value;
                result.Add(mu);
            }

            return result;
        }


        private static Dictionary<string, Models.IngredientItem> Mililiters(Models.IngredientItem currentItem, Models.IngredientItem basicItem)
        {
            Dictionary<string, Models.IngredientItem> items = new Dictionary<string, Models.IngredientItem>();

            items.Add(G, new Models.IngredientItem(basicItem.Name, currentItem.Quantity * basicItem.Quantity / basicItem.GMLRatio, basicItem.MeasurementUnitId, basicItem.GMLRatio));
            items.Add(S, new Models.IngredientItem(basicItem.Name, currentItem.Quantity / SPOON_MAX, basicItem.MeasurementUnitId, basicItem.GMLRatio));
            items.Add(TS, new Models.IngredientItem(basicItem.Name, currentItem.Quantity / TEASPOON_MAX, basicItem.MeasurementUnitId, basicItem.GMLRatio));
            items.Add(GL, new Models.IngredientItem(basicItem.Name, currentItem.Quantity / GLASS_MAX, basicItem.MeasurementUnitId, basicItem.GMLRatio));

            return items;
        }

        private static Dictionary<string, Models.IngredientItem> Grams(Models.IngredientItem currentItem, Models.IngredientItem basicItem)
        {
            Dictionary<string, Models.IngredientItem> items = new Dictionary<string, Models.IngredientItem>();

            var ml = new Models.IngredientItem(basicItem.Name, currentItem.Quantity / basicItem.Quantity * basicItem.GMLRatio, basicItem.MeasurementUnitId, basicItem.GMLRatio);
            items.Add(ML, ml);
            items.Add(S, new Models.IngredientItem(basicItem.Name, ml.Quantity / SPOON_MAX, basicItem.MeasurementUnitId, basicItem.GMLRatio));
            items.Add(TS, new Models.IngredientItem(basicItem.Name, ml.Quantity / TEASPOON_MAX, basicItem.MeasurementUnitId, basicItem.GMLRatio));
            items.Add(GL, new Models.IngredientItem(basicItem.Name, ml.Quantity / GLASS_MAX, basicItem.MeasurementUnitId, basicItem.GMLRatio));

            return items;
        }

        private static Dictionary<string, Models.IngredientItem> Spoons(Models.IngredientItem currentItem, Models.IngredientItem basicItem)
        {
            Dictionary<string, Models.IngredientItem> items = new Dictionary<string, Models.IngredientItem>();

            Models.IngredientItem ml = new Models.IngredientItem(basicItem.Name, currentItem.Quantity * SPOON_MAX, basicItem.MeasurementUnitId, basicItem.GMLRatio);
            items.Add(ML, ml);
            items.Add(G, new Models.IngredientItem(basicItem.Name, ml.Quantity * basicItem.Quantity / basicItem.GMLRatio, basicItem.MeasurementUnitId, basicItem.GMLRatio));
            items.Add(TS, new Models.IngredientItem(basicItem.Name, ml.Quantity / TEASPOON_MAX, basicItem.MeasurementUnitId, basicItem.GMLRatio));
            items.Add(GL, new Models.IngredientItem(basicItem.Name, ml.Quantity / GLASS_MAX, basicItem.MeasurementUnitId, basicItem.GMLRatio));

            return items;
        }

        private static Dictionary<string, Models.IngredientItem> Teaspoons(Models.IngredientItem currentItem, Models.IngredientItem basicItem)
        {
            Dictionary<string, Models.IngredientItem> items = new Dictionary<string, Models.IngredientItem>();

            Models.IngredientItem ml = new Models.IngredientItem(basicItem.Name, currentItem.Quantity * TEASPOON_MAX, basicItem.MeasurementUnitId, basicItem.GMLRatio);
            items.Add(ML, ml);
            items.Add(S, new Models.IngredientItem(basicItem.Name, ml.Quantity / SPOON_MAX, basicItem.MeasurementUnitId, basicItem.GMLRatio));
            items.Add(G, new Models.IngredientItem(basicItem.Name, ml.Quantity * basicItem.Quantity / basicItem.GMLRatio, basicItem.MeasurementUnitId, basicItem.GMLRatio));
            items.Add(GL, new Models.IngredientItem(basicItem.Name, ml.Quantity / GLASS_MAX, basicItem.MeasurementUnitId, basicItem.GMLRatio));

            return items;
        }

        private static Dictionary<string, Models.IngredientItem> Glasses(Models.IngredientItem currentItem, Models.IngredientItem basicItem)
        {
            Dictionary<string, Models.IngredientItem> items = new Dictionary<string, Models.IngredientItem>();

            Models.IngredientItem ml = new Models.IngredientItem(basicItem.Name, currentItem.Quantity * GLASS_MAX, basicItem.MeasurementUnitId, basicItem.GMLRatio);
            items.Add(ML, ml);
            items.Add(S, new Models.IngredientItem(basicItem.Name, ml.Quantity / SPOON_MAX, basicItem.MeasurementUnitId, basicItem.GMLRatio));
            items.Add(TS, new Models.IngredientItem(basicItem.Name, ml.Quantity / TEASPOON_MAX, basicItem.MeasurementUnitId, basicItem.GMLRatio));
            items.Add(G, new Models.IngredientItem(basicItem.Name, ml.Quantity * basicItem.Quantity / basicItem.GMLRatio, basicItem.MeasurementUnitId, basicItem.GMLRatio));

            return items;
        }
    }
}
