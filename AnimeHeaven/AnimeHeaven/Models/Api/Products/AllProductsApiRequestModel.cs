namespace AnimeHeaven.Models.Api.Products
{
    using AnimeHeaven.Models;

    public class AllProductsApiRequestModel
    {
        public int ProductsPerPage { get; init; } = 8;

        public int TotalProducts { get; init; }

        public ProductsSorting Sorting { get; init; }

        public string SearchTerm { get; init; }

        public int CurrentPage { get; init; } = 1;

        public string Category { get; set; }
    }
}
