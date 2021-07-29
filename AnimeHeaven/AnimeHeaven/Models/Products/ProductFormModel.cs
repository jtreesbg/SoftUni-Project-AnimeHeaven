namespace AnimeHeaven.Models.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AnimeHeaven.Services.Products;

    using static Data.DataConstants.Products;
    public class ProductFormModel
    {
        [Required]
        [StringLength(MaxNameLength, MinimumLength = MinNameLength)]
        public string Name { get; init; }

        [Required]
        public double Price { get; init; }

        [Required]
        [StringLength(AnimeOriginMaxLength, MinimumLength = AnimeOriginMinLength)]
        public string AnimeOrigin { get; init; }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; init; }

        [Display(Name = "Image URL")]
        [Required]
        [Url]
        public string ImageUrl { get; init; }

        [Range(YearMinValue, YearMaxValue)]
        public int Year { get; init; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        public IEnumerable<ProductCatergoryServiceModel> Categories { get; set; }
    }
}