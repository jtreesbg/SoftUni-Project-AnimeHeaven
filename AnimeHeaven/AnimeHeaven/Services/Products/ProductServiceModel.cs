using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimeHeaven.Services.Products
{
    public class ProductServiceModel
    {
        public int Id { get; set; }

        public string Name { get; init; }

        public string AnimeOrigin { get; set; }

        public double Price { get; init; }

        public string ImageUrl { get; init; }

        public string Category { get; init; }
    }
}
