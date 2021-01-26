using CookTool.Client.Shared.Models;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CookTool.Client.Shared.Helpers
{
    public class BarcodeHelper
    {
        private static int INVALID_VALUE = -1;
        
        private static int VALID_STATUS_VALUE = 1;
        private static readonly string STATUS = "status";
        private static readonly string PRODUCT = "product";
        private static readonly string PRODUCT_NAME = "product_name";
        private static readonly string QUANTITY = "quantity";

        private static readonly string PRODUCT_NOT_FOUND_ERROR = "Product Not Found!";
        private static readonly string NOT_DEFINED_PROPERLY_ERROR = "Some Parameters Are Not Defined Properly!";

        private static String URL_BASE = "https://world.openfoodfacts.org/api/v0/product/";
        private static HttpClient OFFclient = new HttpClient() { BaseAddress = new Uri(URL_BASE) };

        public static async Task GenereteItemByBarcode(string barcode, IngredientItem item, BarcodeScanResult result)
        {
            var json = await PrepareResponse(barcode);

            if ((int)json.GetValue(STATUS) == VALID_STATUS_VALUE)
            {
                SetIngredientItem(ref item, ref json);

                if(item.Quantity == INVALID_VALUE || item.MeasurementUnitId == INVALID_VALUE || String.IsNullOrEmpty(item.Name))
                {
                    SetBarcodeScanResult(ref result, false, NOT_DEFINED_PROPERLY_ERROR);
                }
                else
                {
                    SetBarcodeScanResult(ref result, true, "");
                }
            }
            else
            {
                SetBarcodeScanResult(ref result, false, PRODUCT_NOT_FOUND_ERROR);
                SetIngredientItemDefault(ref item);
            }
        }

        private static async Task<JObject> PrepareResponse(string barcode)
        {
            var url = String.Format("{0}.json", barcode);
            OFFclient.DefaultRequestHeaders.Add("User-Agent", "CookTool");
            var response = await OFFclient.GetAsync(url);
            var stream = await response.Content.ReadAsStreamAsync();

            string resultString;
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                resultString = reader.ReadToEnd();
            }

            return JObject.Parse(resultString);
        }

        private static int PrepareQuantity(string quantity)
        {
            if (quantity == null) return INVALID_VALUE;
            string result = string.Empty;
            for (int i = 0; i < quantity.Length; i++)
            {
                if (Char.IsDigit(quantity[i]))
                    result += quantity[i];
            }

            if (result.Length > 0)
                return int.Parse(result);
            return INVALID_VALUE;
        }

        private static int PreprareUnit(string quantity)
        {
            if (quantity == null) return INVALID_VALUE;
            string result = string.Empty;
            for (int i = 0; i < quantity.Length; i++)
            {
                if (Char.IsLetter(quantity[i]))
                    result += quantity[i];
            }

            int value = INVALID_VALUE;
            if (MeasurementHelper.UnitsShortcuts.TryGetValue(result, out value)) return value;
            return INVALID_VALUE;
        }

        private static void SetBarcodeScanResult(ref BarcodeScanResult result, bool Success, string ErrorMessage)
        {
            result.Success = Success;
            result.Error = ErrorMessage;
        }

        private static void SetIngredientItem(ref IngredientItem item, ref JObject json)
        {
            var product = json.GetValue(PRODUCT);
            item.Name = (string)product.SelectToken(PRODUCT_NAME);
            var productQuantity = (string)product.SelectToken(QUANTITY);
            item.Quantity = PrepareQuantity(productQuantity);
            item.MeasurementUnitId = PreprareUnit(productQuantity);
        }
        
        private static void SetIngredientItemDefault(ref IngredientItem item)
        {
            item.Name = null;
            item.Quantity = INVALID_VALUE;
            item.MeasurementUnitId = INVALID_VALUE;
        }

    }
}
