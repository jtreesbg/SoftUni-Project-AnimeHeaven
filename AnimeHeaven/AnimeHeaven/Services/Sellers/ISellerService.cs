namespace AnimeHeaven.Services.Sellers
{
    using AnimeHeaven.Data.Models;
    public interface ISellerService
    {
        public bool IsSeller(string userId);

        public int IdByUser(string userId);

        public Customer GetCustomer(string userId);

        public void SaveSellerInDb(Seller seller);
    }
}
