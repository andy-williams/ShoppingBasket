using System;

namespace ShoppingBasket
{
    public class ShoppingBasket : IShoppingBasket
    {
        public void AddItem(string sku, int quantity = 1)
        {
            throw new NotImplementedException();
        }

        public decimal GetTotal()
        {
            throw new NotImplementedException();
        }
    }

    public interface IShoppingBasket
    {
        void AddItem(string sku, int quantity = 1);
        decimal GetTotal();
    }

}
