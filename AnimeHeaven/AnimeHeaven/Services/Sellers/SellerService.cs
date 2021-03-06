namespace AnimeHeaven.Services.Sellers
{
    using System.Linq;
    using AnimeHeaven.Data;
    using AnimeHeaven.Data.Models;

    public class SellerService : ISellerService
    {
        private readonly AnimeHeavenDbContext data;

        public SellerService(AnimeHeavenDbContext data)
        {
            this.data = data;
        }

        public bool IsSeller(string userId)
            => this.data
                .Sellers
                .Any(d => d.UserId == userId);

        public int IdByUser(string userId)
            => this.data
                .Sellers
                .Where(d => d.UserId == userId)
                .Select(d => d.Id)
                .FirstOrDefault();

        public void SaveSellerInDb(Seller seller)
        {
            this.data.Sellers.Add(seller);
            this.data.SaveChanges();
        }

        public Customer GetCustomer(string userId)
        {
            return this.data.Users.Where(u => u.Id == userId).FirstOrDefault();
        }
    }
}
