using CookTool.Client.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Client.Shared.Helpers
{
    public class IngredientsHelper
    {
        public static List<IngredientItem> BasicItems = new List<IngredientItem>()
        {
            new IngredientItem("mąka pszenna", 1, 3, 1.56),
            new IngredientItem("mąka pszenna chlebowa", 1, 3, 1.67),
            new IngredientItem("mąka pszenna razowa", 1, 3, 1.72),
            new IngredientItem("mąka krupczatka", 1, 3, 1.25),
            new IngredientItem("mąka żytnia jasna", 1, 3, 1.92),
            new IngredientItem("mąka żytnia razowa", 1, 3, 1.85),
            new IngredientItem("mąka orkiszowa jasna", 1, 3, 1.79),
            new IngredientItem("mąka orkiszowa razowa", 1, 3, 1.72),
            new IngredientItem("skrobia ziemniaczana", 1, 3, 1.39),
            new IngredientItem("cukier", 1, 3, 1.14),
            new IngredientItem("cukier puder", 1, 3, 1.79),
            new IngredientItem("migdały", 1, 3, 1.56),
            new IngredientItem("płatki migdałów", 1, 3, 2.27),
            new IngredientItem("orzechy włoskie", 1, 3, 2.5),
            new IngredientItem("orzechy laskowe", 1, 3, 1.67),
            new IngredientItem("orzechy zmielone", 1, 3, 2.5),
            new IngredientItem("orzechy posiekane", 1, 3, 0.16),
            new IngredientItem("mak", 1, 3, 1.56),
            new IngredientItem("mak zmielony", 1, 3, 2.78),
            new IngredientItem("kakao", 1, 3, 2.5),
            new IngredientItem("mleko", 1, 3, 1),
            new IngredientItem("mleko w proszku", 1, 3, 2.5),
            new IngredientItem("masło", 1, 3, 1.04),
            new IngredientItem("woda", 1, 3, 1),
            new IngredientItem("śmietanka kremowa 30% lub 36%", 1, 3, 0.98),
            new IngredientItem("kwaśna śmietana 18%", 1, 3, 0.96),
            new IngredientItem("maślanka/kefir", 1, 3, 0.98),
            new IngredientItem("jogurt naturalny", 1, 3, 0.96),
            new IngredientItem("olej", 1, 3, 1.05),
            new IngredientItem("miód", 1, 3, 0.69),
            new IngredientItem("golden syrup", 1, 3, 0.68),
            new IngredientItem("wiórki kokosowe", 1, 3, 2.63),
            new IngredientItem("chocolate chips", 1, 3, 1.43),
            new IngredientItem("nutella", 1, 3, 0.79),
            new IngredientItem("masło orzechowe", 1, 3, 0.93),
            new IngredientItem("masa kajmakowa z puszki", 1, 3, 0.83),
            new IngredientItem("mleko skondensowane słodkie", 1, 3, 0.78),
            new IngredientItem("płatki owsiane", 1, 3, 2.27),
            new IngredientItem("puree z dyni", 1, 3, 1),
            new IngredientItem("łamane żyto", 1, 3, 1.47),
            new IngredientItem("kasza jaglana", 1, 3, 1.14),
            new IngredientItem("sezan", 1, 3, 1.56),
        };
    }
}
