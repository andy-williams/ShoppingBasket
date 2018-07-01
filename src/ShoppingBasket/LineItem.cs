namespace ShoppingBasket
{
    public class LineItem
    {
        public LineItem(string sku, string name, decimal price)
        {
            Sku = sku;
            Name = name;
            Price = price;
        }

        public decimal Price { get; }
        public string Sku { get; }
        public string Name { get; }
    }
}