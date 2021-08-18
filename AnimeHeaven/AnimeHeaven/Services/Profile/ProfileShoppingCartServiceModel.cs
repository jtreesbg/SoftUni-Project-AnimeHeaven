namespace AnimeHeaven.Services.Profile
{
    using System.Collections.Generic;
    using AnimeHeaven.Data.Models;
    public class ProfileShoppingCartServiceModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public double TotalPrice { get; set; }
    }
}
