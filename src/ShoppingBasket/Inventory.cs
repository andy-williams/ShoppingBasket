using System.Collections.Generic;

namespace ShoppingBasket
{
    public class Inventory : IInventory
    {
        private readonly IDictionary<string, LineItem> _inventory = new Dictionary<string, LineItem>
        {
            { "butter", new LineItem("Butter", 0.80m) },
            { "milk", new LineItem("Milk", 1.15m) },
            { "bread", new LineItem("Bread", 1.00m) }
        };

        public LineItem GetItem(string sku)
        {
            return _inventory[sku];
        }
    }

    public interface IInventory
    {
        LineItem GetItem(string sku);
    }
}