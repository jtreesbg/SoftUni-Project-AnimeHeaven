namespace AnimeHeaven.Services.Products
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AnimeHeaven.Data;
    using AnimeHeaven.Data.Models;
    using AnimeHeaven.Models;

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
                    CategoryName = p.Category.Name,
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

        public IEnumerable<ProductServiceModel> ByUser(string userId)
            => GetProducts(this.data
                    .Products
                    .Where(c => c.Seller.UserId == userId));

        public IEnumerable<ProductCatergoryServiceModel> AllCategories()
        => this.data
                .Categories
                .Select(c => new ProductCatergoryServiceModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();

        private IEnumerable<ProductServiceModel> GetProducts(IQueryable<Product> productQuery)
            => productQuery
               .Select(p => new ProductServiceModel
               {
                   Id = p.Id,
                   Year = p.Year,
                   ImageUrl = p.ImageUrl,
                   CategoryName = p.Category.Name,
                   AnimeOrigin = p.AnimeOrigin,
                   Name = p.Name,
                   Price = p.Price,
               })
               .ToList();

        public ProductDetailsServiceModel Details(int id)
        => this.data
            .Products
            .Where(p => p.Id == id)
            .Select(p => new ProductDetailsServiceModel
            {
                Id = p.Id,
                Year = p.Year,
                ImageUrl = p.ImageUrl,
                CategoryName = p.Category.Name,
                AnimeOrigin = p.AnimeOrigin,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                SellerId = p.Seller.Id,
                SellerName = p.Seller.Name,
                UserId = p.Seller.UserId
            })
            .FirstOrDefault();

        public bool CategoryExists(int categoryId)
        => this.data
            .Categories
            .Any(c => c.Id == categoryId);

        public int Create(string name, double price, string animeOrigin, string description, string imageUrl, int year, int categoryId, int sellerId)
        {
            var productData = new Product
            {
                Name = name,
                Price = price,
                AnimeOrigin = animeOrigin,
                Description = description,
                ImageUrl = imageUrl,
                Year = year,
                CategoryId = categoryId,
                SellerId = sellerId
            };

            this.data.Products.Add(productData);
            this.data.SaveChanges();

            return productData.Id;
        }

        public bool Edit(int id, string name, double price, string animeOrigin, string description, string imageUrl, int year, int categoryId, int sellerId)
        {
            var productData = this.data.Products.Find(id);

            if (productData.SellerId != sellerId)
            {
                return false;
            }

            productData.Name = name;
            productData.Price = price;
            productData.AnimeOrigin = animeOrigin;
            productData.Description = description;
            productData.ImageUrl = imageUrl;
            productData.Year = year;
            productData.CategoryId = categoryId;

            this.data.SaveChanges();

            return true;
        }
    }
}
