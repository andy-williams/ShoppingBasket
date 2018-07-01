namespace ShoppingBasket
{
    public class LineItem
    {
        public LineItem(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public decimal Price { get; }
        public string Name { get; }
    }
}