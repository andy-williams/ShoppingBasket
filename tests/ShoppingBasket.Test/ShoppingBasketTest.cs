using System.Collections.Generic;
using TestStack.BDDfy;
using Xunit;

namespace ShoppingBasket.Test
{
    public class ShoppingBasketTest
    {
        private readonly IShoppingBasket _shoppingBasket;

        public ShoppingBasketTest()
        {
            var inventory = new Dictionary<string, LineItem>
            {
                { "butter", new LineItem("Butter", 0.80m) },
                { "milk", new LineItem("Milk", 1.15m) },
                { "bread", new LineItem("Bread", 1.00m) }
            };

            _shoppingBasket = new ShoppingBasket(inventory, new DiscountService());
        }

        [Fact]
        public void It_GetsTotal()
        {
            this.Given(x => x.BasketHas(1).Bread())
                .Given(x => x.BasketHas(1).Butter())
                .And(x => x.BasketHas(1).Milk())
                .Then(x => x.ItGetsTotalOf(2.95m))
                .BDDfy();
        }

        [Fact]
        public void It_GetsTotal_WithDiscountOnButter()
        {
            this.Given(x => x.BasketHas(2).Butter())
                .And(x => x.BasketHas(2).Bread())
                .Then(x => x.ItGetsTotalOf(3.10m))
                .BDDfy();
        }

        [Fact]
        public void It_GetsTotal_WithFreeMilk()
        {
            this.Given(x => x.BasketHas(4).Milk())
                .Then(x => x.ItGetsTotalOf(3.45m))
                .BDDfy();
        }

        private void ItGetsTotalOf(decimal total)
        {
            Assert.Equal(total, _shoppingBasket.GetTotal());
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
