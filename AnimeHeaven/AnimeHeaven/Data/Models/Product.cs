namespace AnimeHeaven.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Products;
    public class Product
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        [MaxLength(AnimeOriginMaxLength)]
        public string AnimeOrigin { get; set; }

        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; }

        [Required]
        public int Year { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; init; }

        public int SellerId { get; init; }

        public Seller Seller { get; init; }

        public ICollection<Favourites> Favourites { get; set; }
    }
}