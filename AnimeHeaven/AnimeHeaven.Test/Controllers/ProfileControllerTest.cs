namespace AnimeHeaven.Test.Controllers
{
    using MyTested.AspNetCore.Mvc;
    using Shouldly;
    using Xunit;
    using AnimeHeaven.Controllers;
    using AnimeHeaven.Models;
    using AnimeHeaven.Models.Products;
    using AnimeHeaven.Services.Products;
    using AnimeHeaven.Services.Profile;

    using static Data.Data;

    public class ProfileControllerTest
    {
        [Fact]
        public void AccountAdminShouldRedirectToToAllProducts()
              => MyController<ProfileController>
               .Instance()
               .WithUser(u => u.WithIdentifier(UserId).InRole("Administrator"))
               .Calling(c => c.Account())
               .ShouldHave()
               .ActionAttributes(a => a
                   .RestrictingForAuthorizedRequests())
               .AndAlso()
               .ShouldReturn()
               .Redirect(redirect => redirect
                .To<ProductsController>(c => c.All(With.Any<ProductsSearchQueryModel>())));

        [Fact]
        public void CustomerShouldCreateObjectCorrectlyAndShouldReturnView()
              => MyController<ProfileController>
               .Instance()
               .WithUser(u => u.WithIdentifier(TestUser.Identifier))
               .Calling(c => c.Account())
               .ShouldHave()
               .ActionAttributes(a => a
                   .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(data => data
                    .WithSet<ProfileInfoServiceModel>(p =>
                    {
                        p.ShouldNotBeNull();
                    }))
               .AndAlso()
               .ShouldReturn()
               .View(view =>
                     view.WithModelOfType<ProfileInfoServiceModel>());

        [Theory]
        [InlineData(1)]
        public void AddToFavouritesShouldAddTheObjectIntoTheDbAndRedirectToView(int id)
              => MyController<ProfileController>
               .Instance()
               .WithUser(u => u.WithIdentifier(TestUser.Identifier))
               .Calling(c => c.AddToFavourites(id))
               .ShouldHave()
               .ActionAttributes(a => a
                   .RestrictingForAuthorizedRequests())
               .AndAlso()
               .ShouldReturn()
               .Redirect(redirect => redirect
                    .To<ProductsController>(c => c.All(With.Any<ProductsSearchQueryModel>())));

        [Theory]
        [InlineData(1)]
        public void RemoveFavouriteShouldRemoveTheObjectFromTheDbAndRedirectToView(int id)
             => MyController<ProfileController>
              .Instance()
              .WithUser(u => u.WithIdentifier(TestUser.Identifier))
              .Calling(c => c.RemoveFavourite(id))
              .ShouldHave()
              .ActionAttributes(a => a
                  .RestrictingForAuthorizedRequests())
              .AndAlso()
              .ShouldReturn()
              .Redirect(redirect => redirect
                   .To<ProfileController>(c => c.Favourites(With.Any<ProductsSearchQueryModel>())));

        [Theory]
        [InlineData("test", "test", Models.ProductsSorting.Anime, 1)]
        public void FavouritesShouldReturnAllFavouriteProductsAndReturnView(
                    string category,
                    string searchTerm,
                    ProductsSorting sorting,
                    int currPage)
                => MyController<ProfileController>
                    .Instance()
                    .WithUser(user => user.WithIdentifier(TestUser.Identifier))
                    .Calling(c => c.Favourites(new ProductsSearchQueryModel()
                    {
                        Category = category,
                        SearchTerm = searchTerm,
                        Sorting = sorting,
                        Categories = null,
                        TotalProducts = 3,
                        CurrentPage = currPage,
                        Products = new System.Collections.Generic.List<ProductServiceModel>(),
                    }))
                    .ShouldReturn()
                    .View(view => view
                        .WithModelOfType<ProductsSearchQueryModel>());

    }
}