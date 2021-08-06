namespace AnimeHeaven.Services.Profile
{
    using System.Collections.Generic;
    using System.Linq;
    using AnimeHeaven.Data;
    using AnimeHeaven.Data.Models;

    public class ProfileService : IProfileService
    {
        private readonly AnimeHeavenDbContext data;

        public ProfileService(AnimeHeavenDbContext data)
        {
            this.data = data;
        }

        public Product GetProduct(int id)
        {
            return this.data.Products.Where(p => p.Id == id).FirstOrDefault();
        }

        public List<Product> GetProducts(string userId)
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
        {
            var seller = this.data
                 .Sellers
                 .Where(c => c.UserId == userId)
                 .FirstOrDefault();

            return seller;
        }

        public Customer GetCustomerDetails(string userId)
        {
            var user = this.data.Customers.Where(c => c.Id == userId).FirstOrDefault();

            return user;
        }
    }
}
