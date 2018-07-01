using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingBasket
{
    public class DiscountService : IDiscountService
    {
        public IList<LineItem> GetDiscountItems(IList<LineItem> items)
        {
            var discounts = new List<LineItem>();

            var butterCount = items.Count(x => x.Name == "Butter");
            var breadDiscountsAvailable = butterCount > 0 ? butterCount / 2 : 0;
            var breadCount = items.Count(x => x.Name == "Bread");

            var breadDiscounts = breadCount > breadDiscountsAvailable
                ? breadDiscountsAvailable
                : breadCount;

            var breadPrice = items.FirstOrDefault(x => x.Name == "Bread")?.Price;
            for (var i = 0; i < breadDiscounts; i++)
            {
                discounts.Add(new LineItem("2xButter - Bread 50% Off", -(breadPrice.Value * 0.5m)));
            }

            return discounts;
        }
    }

    public interface IDiscountService
    {
        IList<LineItem> GetDiscountItems(IList<LineItem> items);
    }
}