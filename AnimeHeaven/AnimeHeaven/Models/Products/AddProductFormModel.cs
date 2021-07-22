namespace AnimeHeaven.Models.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;
    public class AddProductFormModel
    {
        [Required]
        [StringLength(ProductMaxNameLength, MinimumLength = ProductMinNameLength)]
        public string Name { get; init; }

        [Required]
        public double Price { get; init; }

        [Required]
        [StringLength(ProductAnimeOriginMaxLength, MinimumLength = ProductAnimeOriginMinLength)]
        public string AnimeOrigin { get; init; }

        [Required]
        [StringLength(ProductDescriptionMaxLength, MinimumLength = ProductDescriptionMinLength)]
        public string Description { get; init; }

        [Display(Name = "Image URL")]
        [Required]
        [Url]
        public string ImageUrl { get; init; }

        [Range(ProductYearMinValue, ProductYearMaxValue)]
        public int Year { get; init; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        public IEnumerable<ProductCatergoryViewModel> Categories { get; set; }
    }
}