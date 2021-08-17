namespace AnimeHeaven.Test.Routing
{
    using AnimeHeaven.Controllers;
    using AnimeHeaven.Models.Sellers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class SellersControllerTest
    {
        [Fact]
        public void GetBecomeRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Sellers/Become")
                    .WithMethod(HttpMethod.Get))
                .To<SellersController>(c => c.Become());

        [Fact]
        public void PostBecomeRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Sellers/Become")
                    .WithMethod(HttpMethod.Post))
                .To<SellersController>(c => c.Become(With.Any<BecomeSellerFormModel>()));
    }
}
