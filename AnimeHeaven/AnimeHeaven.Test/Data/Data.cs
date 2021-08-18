namespace AnimeHeaven.Test.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AnimeHeaven.Data.Models;

    public class Data
    {
        public static string UserId => "1b99c696-64f5-443a-ae1e-8o4t8wk7c1gr";

        public static Seller Seller = new Seller()
        {
            UserId = "test",
            Address = "Zapad 2",
            PhoneNumber = "0878999999",
            Email = null,
            Username = "TestUser",
        };

        public static Customer GetCustomer()
        {
            return new Customer
            {
                Id = "1b99c696-64f5-443a-ae1e-6b4a1bc8f2cb",
                UserName = "alex",
                Email = "alex@ah.a",
                PasswordHash = "sdasd324olkk34dff",
            };
        }

        public static Product GetProduct()
        {
            return new Product
            {
                Id = 1,
                AnimeOrigin = "animeOrigin",
                Category = null,
                CategoryId = 1,
                Description = "description",
                Name = "name",
                Price = 1,
                SellerId = 1,
                Seller = null,
                UserId = "1b99c696-64f5-443a-ae1e-6b4a1bc8f2cb",
                Year = 2020,
                ImageUrl = "imageUrl"
            };
        }
    }
}
