using TestStack.BDDfy;
using Xunit;

namespace ShoppingBasket.Test
{
    public class ShoppingBasketTest
    {
        private readonly IShoppingBasket _shoppingBasket;

        public ShoppingBasketTest()
        {
            _shoppingBasket = new ShoppingBasket();
        }

        [Fact]
        public void It_GetsTotal()
        {
            this.Given(x => x.BasketHas(1).Bread())
                .And(x => x.BasketHas(1).Milk())
                .Then(x => x.ItGetsTotalOf(2.95m))
                .BDDfy();
        }

        private void ItGetsTotalOf(decimal total)
        {
            Assert.Equal(_shoppingBasket.GetTotal(), total);
        }

        private FluentBasketItemsBuilder BasketHas(int numberOfItems)
        {
            return new FluentBasketItemsBuilder(_shoppingBasket, numberOfItems);
        }

        private class FluentBasketItemsBuilder
        {
            private readonly IShoppingBasket _shoppingBasket;
            private readonly int _quantity;

            public FluentBasketItemsBuilder(IShoppingBasket shoppingBasket, int quantity)
            {
                _shoppingBasket = shoppingBasket;
                _quantity = quantity;
            }

            public void Milk()
            {
                AddItems("milk");
            }

            public void Butter()
            {
                AddItems("butter");
            }

            public void Bread()
            {
                AddItems("bread");
            }

            private void AddItems(string barCode)
            {
                _shoppingBasket.AddItem(barCode, _quantity);
            }
        }
    }
}
