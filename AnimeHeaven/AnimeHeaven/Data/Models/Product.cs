namespace AnimeHeaven.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    using static DataConstants;
    public class Product
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(ProductMaxNameLength)]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        [MaxLength(ProductAnimeOriginMaxLength)]
        public string AnimeOrigin { get; set; }

        [MaxLength(ProductDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [MaxLength(ProductImageUrlMaxLength)]
        public string ImageUrl { get; set; }

        [Required]
        public int Year { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; init; }
    }
}