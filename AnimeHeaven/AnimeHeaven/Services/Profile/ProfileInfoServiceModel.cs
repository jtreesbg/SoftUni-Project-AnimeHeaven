using AnimeHeaven.Data.Models;
using System.Collections.Generic;

namespace AnimeHeaven.Services.Profile
{
    public class ProfileInfoServiceModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
        public string ImageUrl { get; set; } = " https://d3n8a8pro7vhmx.cloudfront.net/imaginebetter/pages/313/meta_images/original/blank-profile-picture-973460_1280.png?1614051091";
        public int ProductsForSale { get; set; }
        public List<Product> Products { get; set; }
    }
}
