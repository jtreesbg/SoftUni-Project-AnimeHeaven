namespace AnimeHeaven.Data.Models
{
    public class ShoppingCart
    {
        public string UserId { get; set; }

        public Customer User { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
