namespace AnimeHeaven.Test.Routing
{
    using AnimeHeaven.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    public class ShoppingCartControllerTest
    {
        private const int id = 3;

        [Fact]
        public void MyCartRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("ShoppingCart/MyCart")
            .To<ShoppingCartController>(c => c.MyCart());

        [Fact]
        public void AddRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap($"ShoppingCart/Add/{id}")
            .To<ShoppingCartController>(c => c.Add(id));

        [Fact]
        public void RemoveFromShoppingCartRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap($"ShoppingCart/RemoveFromShoppingCart/{id}")
            .To<ShoppingCartController>(c => c.RemoveFromShoppingCart(id));

        [Fact]
        public void BuyProductsRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("ShoppingCart/BuyProducts")
            .To<ShoppingCartController>(c => c.BuyProducts());
    }
}
