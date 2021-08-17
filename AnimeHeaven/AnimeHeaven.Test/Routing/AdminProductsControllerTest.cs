namespace AnimeHeaven.Test.Routing
{
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using ProductsController = Areas.Admin.Controllers.ProductsController;

    public class AdminProductsControllerTest
    {
        [Fact]
        public void IndexRoutingShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Products/")
            .To<ProductsController>(c => c.Index());
    }
}
