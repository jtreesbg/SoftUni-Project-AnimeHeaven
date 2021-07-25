namespace AnimeHeaven.Models.Products
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; init; }

        public string AnimeOrigin { get; set; }

        public double Price { get; init; }

        public string ImageUrl { get; init; }

        public string Category { get; init; }
    }
}
