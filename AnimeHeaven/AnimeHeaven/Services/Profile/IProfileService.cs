namespace AnimeHeaven.Services.Profile
{
    using System.Collections.Generic;
    using AnimeHeaven.Data.Models;

    public interface IProfileService
    {
        public bool IsSeller(string userId);

        public Customer GetCustomerDetails(string userId);

        public Seller GetSellerDetails(string userId);

        public List<Product> GetProducts(string userId);

        public int GetSellerId(string userId);

        public Product GetProduct(int id);
        public bool AddProductToUserFavourite(string userId, int productId);
    }
}