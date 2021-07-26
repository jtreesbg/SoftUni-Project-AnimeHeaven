namespace AnimeHeaven.Controllers.Api
{
    using Microsoft.AspNetCore.Mvc;
    using AnimeHeaven.Models.Api.Products;
    using AnimeHeaven.Services.Products;

    [ApiController]
    [Route("api/products")]
    public class ProductApiController : ControllerBase
    {
        private readonly IProductService products;

        public ProductApiController(IProductService products)
        {
            this.products = products;
        }

        [HttpGet]
        public ProductQueryServiceModel All([FromQuery] AllProductsApiRequestModel query)
        {
            return this.products.All(
                query.Category,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                query.ProductsPerPage);
        }
    }
}
