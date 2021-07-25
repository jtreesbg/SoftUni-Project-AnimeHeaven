namespace AnimeHeaven.Models.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ProductsSearchQueryModel
    {
        public ProductsSorting Sorting { get; init; }

        [Display(Name = "Search by text:")]
        public string SearchTerm { get; init; }

        public IEnumerable<string> Categories { get; set; }

        public string Category { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
