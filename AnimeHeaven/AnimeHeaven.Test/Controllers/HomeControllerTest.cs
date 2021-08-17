namespace AnimeHeaven.Test.Controllers
{
    using AnimeHeaven.Controllers;
    using AnimeHeaven.Services.Products;
    using MyTested.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using Xunit;

    using static Data.Products;
    using static WebConstants.Cache;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnCorrectViewWithModel()
            => MyController<HomeController>
            .Instance(controller => controller
                .WithData(GetTenProducts))
            .Calling(c => c.Index())
            .ShouldHave()
            .MemoryCache(cache => cache
                .ContainingEntry(entry => entry
                    .WithKey(MostRecenetProductsCacheKey)
                    .WithAbsoluteExpirationRelativeToNow(TimeSpan.FromMinutes(15))
                    .WithValueOfType<List<ProductServiceModel>>()))
            .AndAlso()
            .ShouldReturn()
            .View(view => view
                .WithModelOfType<List<ProductServiceModel>>());

        [Fact]
        public void ErrorShouldReturnView()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Error())
                .ShouldReturn()
                .View();
    }
}