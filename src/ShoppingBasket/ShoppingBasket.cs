using System.Collections.Generic;
using System.Linq;

namespace ShoppingBasket
{
    public class ShoppingBasket : IShoppingBasket
    {
        private readonly IDictionary<string, LineItem> _inventory;

        private readonly IList<LineItem> _basket = new List<LineItem>();

        public ShoppingBasket(IDictionary<string, LineItem> inventory)
        {
            _inventory = inventory;
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
            return _basket.Select(x => x.Price).Sum();
        }
    }

    public interface IShoppingBasket
    {
        void AddItem(string sku, int quantity = 1);
        decimal GetTotal();
    }
}
