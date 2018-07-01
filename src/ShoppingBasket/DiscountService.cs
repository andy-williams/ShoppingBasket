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
            var milk = _inventory.GetItem("milk");

            var milkCount = items.Count(x => x.Sku == milk.Sku);
            var freeMilkAvailable = milkCount > 0 ? milkCount / 4 : 0;

            for (var i = 0; i < freeMilkAvailable; i++)
            {
                discounts.Add(new LineItem("4xmilk-1free", "4xMilk - 1 FREE", -(milk.Price)));
            }

            return discounts;
        }

        private IList<LineItem> GetBreadDiscounts(IList<LineItem> items)
        {
            var discounts = new List<LineItem>();
            var butter = _inventory.GetItem("butter");
            var bread = _inventory.GetItem("bread");

            var butterCount = items.Count(x => x.Sku == butter.Sku);
            var breadDiscountsAvailable = butterCount > 0 ? butterCount / 2 : 0;
            var breadCount = items.Count(x => x.Sku == bread.Sku);

            var breadDiscountCount = breadCount > breadDiscountsAvailable
                ? breadDiscountsAvailable
                : breadCount;

            for (var i = 0; i < breadDiscountCount; i++)
            {
                discounts.Add(new LineItem("2xbread-butter50%", "2xButter - Bread 50% Off", -(bread.Price * 0.5m)));
            }

            return discounts;
        }
    }

    public interface IDiscountService
    {
        IList<LineItem> GetDiscountItems(IList<LineItem> items);
    }
}