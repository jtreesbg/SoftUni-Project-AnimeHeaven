namespace AnimeHeaven.Services.Profile
{
    using AnimeHeaven.Data.Models;
    using System.Collections.Generic;
    public class ProfileShoppingCartServiceModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public double TotalPrice { get; set; }
    }
}
