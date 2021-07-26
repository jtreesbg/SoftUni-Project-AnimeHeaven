namespace AnimeHeaven.Services.Products
{
    using System.Collections.Generic;

    public class ProductQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int ProductsPerPage { get; init; }

        public int TotalProducts { get; set; }

        public IEnumerable<ProductServiceModel> Products { get; init; }
    }
}
