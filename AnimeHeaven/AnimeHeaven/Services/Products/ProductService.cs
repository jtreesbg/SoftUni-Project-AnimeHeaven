namespace AnimeHeaven.Services.Products
{
    using System.Collections.Generic;
    using System.Linq;
    using AnimeHeaven.Data;
    using AnimeHeaven.Data.Models;
    using AnimeHeaven.Models;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    public class ProductService : IProductService
    {
        private readonly AnimeHeavenDbContext data;
        private readonly IConfigurationProvider mapper;

        public ProductService(AnimeHeavenDbContext data, IConfigurationProvider mapper)
        {
            this.data = data;
            this.mapper = mapper;
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
                .ProjectTo<ProductServiceModel>(this.mapper)
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
               .ProjectTo<ProductServiceModel>(this.mapper)
               .ToList();

        public ProductDetailsServiceModel Details(int id)
        => this.data
            .Products
            .Where(p => p.Id == id)
            .ProjectTo<ProductDetailsServiceModel>(this.mapper)
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

        public bool IsBySeller(int productId, int sellerId)
            => this.data
                .Products
                .Any(p => p.Id == productId && p.SellerId == sellerId);

        public IEnumerable<ProductServiceModel> GetRecentProducts()
        => this.data
                .Products
                .OrderByDescending(p => p.Id)
                .ProjectTo<ProductServiceModel>(this.mapper)
                .Take(3)
                .ToList();

    }
}
