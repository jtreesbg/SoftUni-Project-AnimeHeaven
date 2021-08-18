namespace AnimeHeaven.Test.Controllers
{
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using AnimeHeaven.Controllers;
    using AnimeHeaven.Services.Profile;

    public class ShoppingCartControllerTest
    {
        [Fact]
        public void MyCartShouldReturnView()
            => MyController<ShoppingCartController>
                .Instance()
                .WithUser(user => user.WithIdentifier(TestUser.Identifier))
                .Calling(c => c.MyCart())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ProfileShoppingCartServiceModel>());

        [Theory]
        [InlineData(1)]
        public void AddShouldReturnView(int id)
           => MyController<ShoppingCartController>
               .Instance()
               .WithUser(user => user.WithIdentifier(TestUser.Identifier))
               .Calling(c => c.Add(id))
               .ShouldReturn()
               .Redirect(redirect => redirect
                   .To<ShoppingCartController>(c => c.MyCart()));

        [Theory]
        [InlineData(1)]
        public void RemoveShouldReturnView(int id)
           => MyController<ShoppingCartController>
               .Instance()
               .WithUser(user => user.WithIdentifier(TestUser.Identifier))
               .Calling(c => c.RemoveFromShoppingCart(id))
               .ShouldReturn()
               .Redirect(redirect => redirect
                   .To<ShoppingCartController>(c => c.MyCart()));

        [Fact]
        public void BuyProductsShouldReturnView()
           => MyController<ShoppingCartController>
               .Instance()
               .WithUser(user => user.WithIdentifier(TestUser.Identifier))
               .Calling(c => c.BuyProducts())
               .ShouldReturn()
               .View();

    }
}
