namespace AnimeHeaven.Services.Sellers
{
    using AnimeHeaven.Data;
    using System.Linq;

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
    }
}
