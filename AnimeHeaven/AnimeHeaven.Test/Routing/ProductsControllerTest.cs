namespace AnimeHeaven.Test.Routing
{
    using AnimeHeaven.Controllers;
    using AnimeHeaven.Models.Products;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class ProductsControllerTest
    {
        private const int id = 3;

        [Fact]
        public void MyRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Products/My")
                .To<ProductsController>(c => c.My());

        [Fact]
        public void GetAddRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Products/Add")
                    .WithMethod(HttpMethod.Get))
                    .To<ProductsController>(c => c.Add());

        [Fact]
        public void PostAddRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(controller => controller
                   .WithPath($"/Products/Add")
                   .WithMethod(HttpMethod.Post))
                   .To<ProductsController>(c => c.Add(With.Any<ProductFormModel>()));

        [Fact]
        public void AllRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(controller => controller
                    .WithPath("/Products/All")
                    .WithMethod(HttpMethod.Get))
                    .To<ProductsController>(c => c.All(With.Any<ProductsSearchQueryModel>()));

        [Fact]
        public void GetDetailsRouteShouldBeMapped()
           => MyRouting
               .Configuration()
               .ShouldMap(controller => controller
                   .WithPath($"/Products/Details/{id}")
                   .WithMethod(HttpMethod.Get))
                   .To<ProductsController>(c => c.Details(id));

        [Fact]
        public void GetEditRouteShouldBeMapped()
           => MyRouting
               .Configuration()
               .ShouldMap(controller => controller
                   .WithPath($"/Products/Edit/{id}")
                   .WithMethod(HttpMethod.Get))
                   .To<ProductsController>(c => c.Edit(id));
        [Fact]
        public void PostEditRouteShouldBeMapped()
           => MyRouting
               .Configuration()
               .ShouldMap(controller => controller
                   .WithPath($"/Products/Edit/{id}")
                   .WithMethod(HttpMethod.Post))
                   .To<ProductsController>(c => c.Edit(id, With.Any<ProductFormModel>()));

        [Fact]
        public void DeleteRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap($"/Products/Delete/{id}")
                .To<ProductsController>(c => c.Delete(id));

    }
}
