namespace AnimeHeaven.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class Category
    {
        [Required]
        public int Id { get; init; }

        [Required]
        public string Name { get; set; }

        public IEnumerable<Product> Products { get; init; } = new List<Product>();
    }
}