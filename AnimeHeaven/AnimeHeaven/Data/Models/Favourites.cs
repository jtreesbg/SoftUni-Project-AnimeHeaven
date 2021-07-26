namespace AnimeHeaven.Data.Models
{
    public class Favourites
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
