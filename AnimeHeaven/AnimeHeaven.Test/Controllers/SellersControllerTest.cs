namespace AnimeHeaven.Test.Controllers
{
    using AnimeHeaven.Controllers;
    using AnimeHeaven.Data.Models;
    using AnimeHeaven.Models.Products;
    using AnimeHeaven.Models.Sellers;
    using Microsoft.AspNetCore.Mvc;
    using MyTested.AspNetCore.Mvc;
    using Shouldly;
    using Xunit;

    using static WebConstants;
    using static Data.Data;

    public class SellersControllerTest
    {
        [Fact]
        public void GetBecomeShouldBeForAuthorizedUserAndReturView()
            => MyController<SellersController>
                .Instance()
                .Calling(c => c.Become())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();

        [Fact]
        public void AdminGetBecomeShouldRedirectView()
            => MyController<SellersController>
            .Instance()
            .WithUser(u => u.WithIdentifier(UserId).InRole("Administrator"))
          .Calling(c => c.Become(new BecomeSellerFormModel { }))
          .ShouldHave()
          .ActionAttributes(a => a
              .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldHave()
            .ActionAttributes(a => a.
                ContainingAttributeOfType<HttpPostAttribute>())
          .AndAlso()
          .ShouldReturn()
          .Redirect(redirect => redirect
           .To<ProductsController>(c => c.All(With.Any<ProductsSearchQueryModel>())));

        [Theory]
        [InlineData("0878999999", "Zapad 2")]
        public void PostBecomeShouldBeForAuthorizedUsersAndReturnRedirectWithValidModel(
            string phoneNumber,
            string address)
            => MyController<SellersController>
                .Instance(controller => controller
                    .WithUser(user => user.WithIdentifier("test")))
                .Calling(c => c.Become(new BecomeSellerFormModel
                {
                    PhoneNumber = phoneNumber,
                    Address = address,
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(data => data
                    .WithSet<Seller>(sellers =>
                    {
                        sellers.ShouldNotBeNull();
                    }))
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(GlobalMessageKey))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<ProductsController>(c => c.All(With.Any<ProductsSearchQueryModel>())));

        [Theory]
        [InlineData("0878999999", "Zapad 2")]
        public void PostBecomeSellerToBecomeSellerShouldReturnBadRequest(
            string phoneNumber,
            string address)
            => MyController<SellersController>
                .Instance(controller => controller
                    .WithUser(user => user.WithIdentifier(TestUser.Identifier)))
                .Calling(c => c.Become(new BecomeSellerFormModel
                {
                    PhoneNumber = phoneNumber,
                    Address = address,
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .BadRequest();

        [Theory]
        [InlineData("0878999999", "")]
        public void PostBecomeInvalidModelStateShouldReturnView(
            string phoneNumber,
            string address)
            => MyController<SellersController>
                .Instance(controller => controller
                    .WithUser(user => user.WithIdentifier("1")))
                .Calling(c => c.Become(new BecomeSellerFormModel
                {
                    PhoneNumber = phoneNumber,
                    Address = address,
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldHave()
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View();
    }
}