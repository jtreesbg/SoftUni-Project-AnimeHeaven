namespace AnimeHeaven.Services.Products
{
    public class ProductDetailsServiceModel : ProductServiceModel
    {
        public string Description { get; init; }
        public int CategoryId { get; init; }
        public int SellerId { get; init; }
        public string SellerName { get; init; }
        public string UserId { get; init; }
    }
}