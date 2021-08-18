namespace AnimeHeaven.Test.Controllers
{
    using MyTested.AspNetCore.Mvc;
    using Shouldly;
    using System.Linq;
    using Xunit;
    using AnimeHeaven.Controllers;
    using AnimeHeaven.Data.Models;
    using AnimeHeaven.Models;
    using AnimeHeaven.Models.Products;
    using AnimeHeaven.Services.Products;

    using static Data.Data;
    public class ProductsControllerTest
    {
        [Fact]
        public void MyShouldGetCorrectProductsAndReturnView()
             => MyController<ProductsController>
                .Instance()
                .WithUser(u => u.WithIdentifier(UserId))
                .Calling(c => c.My())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldHave()
                .Data(data => data
                .WithSet<Product>(set =>
                {
                    set.ShouldNotBeNull();
                }))
                .AndAlso()
                .ShouldReturn()
                .View();

        [Fact]
        public void GetAddAdminShouldBeRedirectedToAllProducts()
          => MyController<ProductsController>
           .Instance()
           .WithUser(u => u.WithIdentifier(UserId).InRole("Administrator"))
           .Calling(c => c.Add())
           .ShouldHave()
           .ActionAttributes(a => a
               .RestrictingForAuthorizedRequests())
           .AndAlso()
           .ShouldReturn()
           .Redirect(redirect => redirect
                .To<ProductsController>(c => c.All(With.Any<ProductsSearchQueryModel>())));

        [Fact]
        public void GetAddNonSellerShouldBeRedirectedToBecomeSeller()
            => MyController<ProductsController>
                .Instance()
                .WithUser(user => user.WithIdentifier("1"))
                .Calling(c => c.Add())
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<SellersController>(c => c.Become()));

        [Fact]
        public void CustomerAddShouldRedirectToBecome()
         => MyController<ProductsController>
          .Instance()
          .WithUser(u => u.WithIdentifier(UserId))
          .Calling(c => c.Add())
          .ShouldHave()
          .ActionAttributes(a => a
              .RestrictingForAuthorizedRequests())
          .AndAlso()
          .ShouldReturn()
          .Redirect(redirect => redirect
           .To<SellersController>(c => c.Become()));

        [Fact]
        public void GetAddSellerShouldBeRedirectedToCorrectView()
           => MyController<ProductsController>
               .Instance()
               .WithUser(user => user.WithIdentifier(TestUser.Identifier))
               .WithData(new Seller
               {
                   UserId = TestUser.Identifier
               })
               .Calling(c => c.Add())
               .ShouldReturn()
               .View(result => result
                    .WithModelOfType<ProductFormModel>()
                    .Passing(model =>
                    {
                        model.Categories.FirstOrDefault(c => c.Id == 1).ShouldNotBeNull();
                    }));

        [Theory]
        [InlineData(null)]
        public void PostAddAdminShouldBeRedirectedToAllProducts(ProductFormModel productFormModel)
         => MyController<ProductsController>
          .Instance()
          .WithUser(u => u.WithIdentifier(UserId).InRole("Administrator"))
          .Calling(c => c.Add(productFormModel))
          .ShouldHave()
          .ActionAttributes(a => a
              .RestrictingForAuthorizedRequests())
          .AndAlso()
          .ShouldReturn()
          .Redirect(redirect => redirect
           .To<ProductsController>(c => c.All(With.Any<ProductsSearchQueryModel>())));

        [Theory]
        [InlineData(null)]
        public void PostAddNonSellerShouldBeRedirectedToBecomeSeller(ProductFormModel productFormModel)
         => MyController<ProductsController>
          .Instance()
          .WithUser(u => u.WithIdentifier(UserId))
          .Calling(c => c.Add(productFormModel))
          .ShouldHave()
          .ActionAttributes(a => a
                .RestrictingForAuthorizedRequests()
                .RestrictingForHttpMethod(HttpMethod.Post))
          .AndAlso()
          .ShouldReturn()
          .Redirect(redirect => redirect
           .To<SellersController>(c => c.Become()));

        [Theory]
        [InlineData(10)]
        public void PostAddProductWithInvalidCategoryShouldReturnView(int cateogryId)
            => MyController<ProductsController>
            .Instance(instance => instance
            .WithUser())
                .Calling(c => c.Add(new ProductFormModel
                {
                    CategoryId = cateogryId
                }))
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View();

        [Theory]
        [InlineData("test", 20, "test", "testtesttest", "https://www.looper.com/img/gallery/50-best-anime-movies-of-all-time/intro-1627065635.jpg", 2002, 1)]
        public void PostAddProductShouldCreateAndRedirectToAllProducts(
                 string name,
                 double price,
                 string animeOrigin,
                 string description,
                 string imageUrl,
                 int year,
                 int categoryId)
            => MyController<ProductsController>
            .Instance(instance => instance
            .WithUser())
                .Calling(c => c.Add(new ProductFormModel
                {
                    Name = name,
                    Price = price,
                    AnimeOrigin = animeOrigin,
                    Description = description,
                    ImageUrl = imageUrl,
                    Year = year,
                    CategoryId = categoryId,
                }))
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<ProductsController>(c => c.All(With.Any<ProductsSearchQueryModel>())));

        [Theory]
        [InlineData("test", "test", ProductsSorting.Anime, 1)]
        public void AllProductsShouldReturnView(
                    string category,
                    string searchTerm,
                    ProductsSorting sorting,
                    int currPage
                    )
            => MyController<ProductsController>
                .Instance()
                .Calling(c => c.All(new ProductsSearchQueryModel()
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

        [Theory]
        [InlineData(1)]
        public void DetailsProductShouldGetCorrectItemAndReturnView(int id)
            => MyController<ProductsController>
                .Instance()
                .Calling(c => c.Details(id))
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(HttpMethod.Get))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ProductDetailsServiceModel>());

        [Theory]
        [InlineData(1)]
        public void DeleteProductShouldReturnView(int id)
           => MyController<ProductsController>
               .Instance()
               .Calling(c => c.Delete(id))
               .ShouldHave()
               .ActionAttributes(attrs => attrs
                   .RestrictingForAuthorizedRequests())
               .AndAlso()
               .ShouldReturn()
               .Redirect(redirect => redirect
                    .To<ProductsController>(c => c.My()));

        [Theory]
        [InlineData(1)]
        public void GetEditCustomerIsRedirectedToSellersBecome(int id)
            => MyController<ProductsController>
                .Instance(instance => instance
                    .WithUser(user => user.WithIdentifier("test")))
                .Calling(c => c.Edit(id))
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                   .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<SellersController>(c => c.Become()));

        [Theory]
        [InlineData(0)]
        public void GetEditUserIdIsDifferentAndShouldReturnUnauthorized(int id)
           => MyController<ProductsController>
               .Instance(instance => instance
                   .WithUser(user => user.WithIdentifier(TestUser.Identifier)))
               .Calling(c => c.Edit(id))
               .ShouldHave()
               .ActionAttributes(attrs => attrs
                  .RestrictingForAuthorizedRequests())
               .AndAlso()
               .ShouldReturn()
               .Unauthorized();

        [Theory]
        [InlineData(1)]
        public void GetEditSellerShouldReturnView(int id)
           => MyController<ProductsController>
               .Instance(instance => instance
                   .WithUser(user => user.WithIdentifier(TestUser.Identifier)))
               .Calling(c => c.Edit(id))
               .ShouldHave()
               .ActionAttributes(attrs => attrs
                  .RestrictingForAuthorizedRequests())
               .AndAlso()
               .ShouldReturn()
               .View(view => view.WithModelOfType<ProductFormModel>());

        [Theory]
        [InlineData(0, "test", 20, "test", "test", "https://www.looper.com/img/gallery/50-best-anime-movies-of-all-time/intro-1627065635.jpg", 2021, 1)]
        public void PostEditCustomerShouldBeRedirectedToSellersBecome(int id, string name, double price, string anime, string description, string imageUrl, int year, int categoryId)
           => MyController<ProductsController>
               .Instance(instance => instance
                   .WithUser(user => user.WithIdentifier("test")))
               .Calling(c => c.Edit(id, new ProductFormModel
               {
                   Name = name,
                   AnimeOrigin = anime,
                   Year = year,
                   Price = price,
                   Description = description,
                   ImageUrl = imageUrl,
                   CategoryId = categoryId
               }))
               .ShouldHave()
               .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
               .AndAlso()
               .ShouldReturn()
               .Redirect(redirect => redirect
                    .To<SellersController>(c => c.Become()));

        [Theory]
        [InlineData(0, "test", 20, "test", "test", "https://www.looper.com/img/gallery/50-best-anime-movies-of-all-time/intro-1627065635.jpg", 2021, 10)]
        public void PostEditInvalidModelStateShouldBeRedirectedToView(int id, string name, double price, string anime, string description, string imageUrl, int year, int categoryId)
           => MyController<ProductsController>
               .Instance(instance => instance
                   .WithUser(user => user.WithIdentifier(TestUser.Identifier)))
               .Calling(c => c.Edit(id, new ProductFormModel
               {
                   Name = name,
                   AnimeOrigin = anime,
                   Year = year,
                   Price = price,
                   Description = description,
                   ImageUrl = imageUrl,
                   CategoryId = categoryId
               }))
               .ShouldHave()
               .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
               .AndAlso()
               .ShouldHave()
               .InvalidModelState()
               .AndAlso()
               .ShouldReturn()
               .View(view => view
                        .WithModelOfType<ProductFormModel>());

        [Theory]
        [InlineData(0, "test", 20, "test", "test", "https://www.looper.com/img/gallery/50-best-anime-movies-of-all-time/intro-1627065635.jpg", 2021, 1)]
        public void PostEditIsNotBySellerShouldRedirectToUnauthorized(int id, string name, double price, string anime, string description, string imageUrl, int year, int categoryId)
              => MyController<ProductsController>
                  .Instance(instance => instance
                      .WithUser(user => user.WithIdentifier(TestUser.Identifier)))
                  .Calling(c => c.Edit(id, new ProductFormModel
                  {
                      Name = name,
                      AnimeOrigin = anime,
                      Year = year,
                      Price = price,
                      Description = description,
                      ImageUrl = imageUrl,
                      CategoryId = categoryId
                  }))
                  .ShouldHave()
                  .ActionAttributes(attrs => attrs
                       .RestrictingForHttpMethod(HttpMethod.Post)
                       .RestrictingForAuthorizedRequests())
                  .AndAlso()
                  .ShouldReturn()
                  .Unauthorized();

        [Theory]
        [InlineData(1, "true", 20, "true", "true", "https://www.true.bg/true.jpg", 2021, 1)]
        public void PostEditShouldWorkAndRedirectToAll(int id, string name, double price, string anime, string description, string imageUrl, int year, int categoryId)
              => MyController<ProductsController>
                  .Instance(instance => instance
                      .WithUser(user => user.WithIdentifier(TestUser.Identifier)))
                  .Calling(c => c.Edit(id, new ProductFormModel
                  {
                      Name = name,
                      AnimeOrigin = anime,
                      Year = year,
                      Price = price,
                      Description = description,
                      ImageUrl = imageUrl,
                      CategoryId = categoryId
                  }))
                  .ShouldHave()
                  .ActionAttributes(attrs => attrs
                       .RestrictingForHttpMethod(HttpMethod.Post)
                       .RestrictingForAuthorizedRequests())
                  .AndAlso()
                  .ShouldReturn()
                  .Redirect(redirect => redirect
                    .To<ProductsController>(c => c.All(With.Any<ProductsSearchQueryModel>())));

        [Theory]
        [InlineData(1, "false", 20, "true", "true", "https://www.true.bg/true.jpg", 2021, 1)]
        public void PostEditCanNotEditShouldReturnBadRequest(int id, string name, double price, string anime, string description, string imageUrl, int year, int categoryId)
             => MyController<ProductsController>
                 .Instance(instance => instance
                     .WithUser(user => user.WithIdentifier(TestUser.Identifier)))
                 .Calling(c => c.Edit(id, new ProductFormModel
                 {
                     Name = name,
                     AnimeOrigin = anime,
                     Year = year,
                     Price = price,
                     Description = description,
                     ImageUrl = imageUrl,
                     CategoryId = categoryId
                 }))
                 .ShouldHave()
                 .ActionAttributes(attrs => attrs
                      .RestrictingForHttpMethod(HttpMethod.Post)
                      .RestrictingForAuthorizedRequests())
                 .AndAlso()
                 .ShouldReturn()
                 .BadRequest();
    }
}