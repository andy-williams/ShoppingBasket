using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingBasket
{
    public class DiscountService : IDiscountService
    {
        private readonly IInventory _inventory;

        public DiscountService(IInventory inventory)
        {
            _inventory = inventory;
        }
        
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

            var milkPrice = _inventory.GetItem("milk").Price;
            for (var i = 0; i < freeMilkAvailable; i++)
            {
                discounts.Add(new LineItem("4xButter - 1 FREE", -(milkPrice)));
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

            var breadPrice = _inventory.GetItem("bread").Price;
            for (var i = 0; i < breadDiscountCount; i++)
            {
                discounts.Add(new LineItem("2xButter - Bread 50% Off", -(breadPrice * 0.5m)));
            }

            return discounts;
        }
    }

    public interface IDiscountService
    {
        IList<LineItem> GetDiscountItems(IList<LineItem> items);
    }
}