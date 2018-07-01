using System.Collections.Generic;
using System.Linq;

namespace ShoppingBasket
{
    public class ShoppingBasket : IShoppingBasket
    {
        private readonly IDictionary<string, LineItem> _inventory;
        private readonly IDiscountService _discountService;

        private readonly IList<LineItem> _basket = new List<LineItem>();

        public ShoppingBasket(IDictionary<string, LineItem> inventory, IDiscountService discountService)
        {
            _inventory = inventory;
            _discountService = discountService;
        }

        public void AddItem(string sku, int quantity = 1)
        {
            var item = _inventory[sku];
            for (var i = 0; i < quantity; i++)
            {
                _basket.Add(item);
            }
        }

        public decimal GetTotal()
        {
            var discountItems = _discountService.GetDiscountItems(_basket);
            var basketTotal = _basket.Select(x => x.Price).Sum();
            var discountTotal = discountItems.Select(x => x.Price).Sum();

            return basketTotal + discountTotal;
        }
    }

    public interface IShoppingBasket
    {
        void AddItem(string sku, int quantity = 1);
        decimal GetTotal();
    }
}
