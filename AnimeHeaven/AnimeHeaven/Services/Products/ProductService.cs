namespace AnimeHeaven.Services.Products
{
    using AnimeHeaven.Data;
    using AnimeHeaven.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProductService : IProductService
    {
        private readonly AnimeHeavenDbContext data;

        public ProductService(AnimeHeavenDbContext data)
        {
            this.data = data;
        }

        public ProductQueryServiceModel All(
            string category,
            string searchTerm,
            ProductsSorting sorting,
            int currentPage,
            int productsPerPage)
        {
            var productsQuery = this.data.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(category))
            {
                productsQuery = productsQuery.Where(p => p.Category.Name == category);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                productsQuery = productsQuery.Where(p =>
                    p.Name.ToLower().Contains(searchTerm.ToLower()) ||
                    p.AnimeOrigin.ToLower().Contains(searchTerm.ToLower()));
            }

            productsQuery = sorting switch
            {
                ProductsSorting.Anime => productsQuery.OrderBy(p => p.AnimeOrigin),
                ProductsSorting.Year => productsQuery.OrderBy(p => p.Year),
                ProductsSorting.DateCreated or _ => productsQuery.OrderByDescending(p => p.Id)
            };

            var totalProducts = productsQuery.Count();

            var products = productsQuery
                .Skip((currentPage - 1) * productsPerPage)
                .Take(productsPerPage)
                .Select(p => new ProductServiceModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    AnimeOrigin = p.AnimeOrigin,
                    Price = p.Price,
                    Category = p.Category.Name,
                    ImageUrl = p.ImageUrl
                })
                .ToList();

            var categories = this.data
                .Products
                .Select(c => c.Category.Name)
                .Distinct()
                .ToList();

            return new ProductQueryServiceModel
            {
                TotalProducts = totalProducts,
                CurrentPage = currentPage,
                Products = products,
                ProductsPerPage = productsPerPage
            };
        }

        public IEnumerable<string> AllProductsCategories()
        {
            return this.data
                 .Products
                 .Select(c => c.Category.Name)
                 .Distinct()
                 .ToList();
        }
    }
}
