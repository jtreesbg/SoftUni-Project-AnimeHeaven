namespace AnimeHeaven.Services.Profile
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using AnimeHeaven.Data;
    using AnimeHeaven.Data.Models;
    using AnimeHeaven.Models;
    using AnimeHeaven.Services.Products;

    public class ProfileService : IProfileService
    {
        private readonly AnimeHeavenDbContext data;
        private readonly IConfigurationProvider mapper;

        public ProfileService(AnimeHeavenDbContext data, IConfigurationProvider mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public Product GetProduct(int id)
        {
            return this.data.Products.Where(p => p.Id == id).FirstOrDefault();
        }

        public List<Product> GetSellerProducts(string userId)
        {
            var id = GetSellerId(userId);

            return this.data
                .Products
                .Where(p => p.SellerId == id)
                .ToList();
        }

        //Move to favourite service
        public bool AddProductToUserFavourite(string userId, int productId)
        {
            var fav = new Favourite()
            {
                UserId = userId,
                ProductId = productId
            };

            this.data.Favourites.Add(fav);
            this.data.SaveChanges();

            return true;
        }

        public IEnumerable<Product> GetCustomerFavouriteProducts(string userId)
            => this.data
                .Favourites
                .Where(f => f.UserId == userId)
                .Select(f => f.Product);


        //Move to shopping cart service
        public bool AddProductToShoppingCart(string userId, int productId)
        {
            var fav = new ShoppingCart()
            {
                UserId = userId,
                ProductId = productId
            };

            this.data.ShoppingCarts.Add(fav);
            this.data.SaveChanges();

            return true;
        }

        public ProductQueryServiceModel All(
            string category,
            string searchTerm,
            ProductsSorting sorting,
            int currentPage,
            int productsPerPage,
            string userId)
        {
            var productsQuery = this.GetCustomerFavouriteProducts(userId).AsQueryable();

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


        public bool IsSeller(string userId)
        => this.data
                .Sellers
                .Any(d => d.UserId == userId);

        public int GetSellerId(string userId)
             => this.data
                   .Sellers
                   .Where(s => s.UserId == userId)
                   .Select(s => s.Id)
                   .FirstOrDefault();

        public Seller GetSellerDetails(string userId)
        => this.data
                 .Sellers
                 .Where(c => c.UserId == userId)
                 .FirstOrDefault();

        public Customer GetCustomerDetails(string userId)
            => this.data.Customers.Where(c => c.Id == userId).FirstOrDefault();

    }
}
