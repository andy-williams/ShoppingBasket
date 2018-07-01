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
            discounts.AddRange(GetBreadDiscounts(items));
            discounts.AddRange(GetFreeMilkOffer(items));

            return discounts;
        }

        private IList<LineItem> GetFreeMilkOffer(IList<LineItem> items)
        {
            var discounts = new List<LineItem>();

            var milkCount = items.Count(x => x.Name == "Milk");
            var freeMilkAvailable = milkCount > 0 ? milkCount / 4 : 0;

            var milkPrice = items.FirstOrDefault(x => x.Name == "Milk")?.Price;
            for (var i = 0; i < freeMilkAvailable; i++)
            {
                discounts.Add(new LineItem("4xButter - 1 FREE", -(milkPrice.Value)));
            }

            return discounts;
        }

        private IList<LineItem> GetBreadDiscounts(IList<LineItem> items)
        {
            var discounts = new List<LineItem>();

            var butterCount = items.Count(x => x.Name == "Butter");
            var breadDiscountsAvailable = butterCount > 0 ? butterCount / 2 : 0;
            var breadCount = items.Count(x => x.Name == "Bread");

            var breadDiscountCount = breadCount > breadDiscountsAvailable
                ? breadDiscountsAvailable
                : breadCount;

            var breadPrice = items.FirstOrDefault(x => x.Name == "Bread")?.Price;
            for (var i = 0; i < breadDiscountCount; i++)
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