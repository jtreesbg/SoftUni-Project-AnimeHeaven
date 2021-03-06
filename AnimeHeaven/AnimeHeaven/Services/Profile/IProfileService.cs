namespace AnimeHeaven.Services.Profile
{
    using System.Collections.Generic;
    using AnimeHeaven.Models;
    using AnimeHeaven.Data.Models;
    using AnimeHeaven.Services.Products;

    public interface IProfileService
    {
        public bool IsSeller(string userId);

        public Customer GetCustomerDetails(string userId);

        public Seller GetSellerDetails(string userId);

        public List<Product> GetSellerProducts(string userId);

        public int GetSellerId(string userId);

        public Product GetProduct(int id);

        public bool AddProductToUserFavourite(string userId, int productId);

        public bool RemoveProductFromFavourites(string userId, int id);

        public IEnumerable<Product> GetCustomerFavouriteProducts(string userId);

        public ProductQueryServiceModel All(
            string category,
            string searchTerm,
            ProductsSorting sorting,
            int currentPage,
            int productsPerPage,
            string userId);

        public bool AddProductToShoppingCart(string userId, int productId);

        public bool RemoveProductFromShoppingCart(string userId, int productId);
        public bool EmptyShoppingCart(string userId);

        public IEnumerable<Product> GetCustomerShoppingCartProducts(string userId);
    }
}