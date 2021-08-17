namespace AnimeHeaven.Test.Controllers
{
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using ProductsController = Areas.Admin.Controllers.ProductsController;

    using static Data.Products;

    public class AdminProductsController
    {
        [Fact]
        public void IndexInControllerAreaLoadCorrectly()
            => MyController<ProductsController>
                .Instance(controller => controller
                    .WithData(GetTenProducts))
                .Calling(c => c.Index())
                .ShouldReturn()
                .View();
    }
}
