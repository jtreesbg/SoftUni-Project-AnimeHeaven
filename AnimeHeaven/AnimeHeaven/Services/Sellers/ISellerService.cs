namespace AnimeHeaven.Services.Sellers
{
    public interface ISellerService
    {
        public bool IsSeller(string userId);

        public int IdByUser(string userId);
    }
}
