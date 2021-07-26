namespace AnimeHeaven.Models.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AnimeHeaven.Services.Products;

    public class ProductsSearchQueryModel
    {
        public const int ProductsPerPage = 6;

        public int TotalProducts { get; set; }

        public ProductsSorting Sorting { get; init; }

        [Display(Name = "Search by text:")]
        public string SearchTerm { get; init; }

        public int CurrentPage { get; init; } = 1;

        public string Category { get; set; }

        public IEnumerable<string> Categories { get; set; }

        public IEnumerable<ProductServiceModel> Products { get; set; }
    }
}
