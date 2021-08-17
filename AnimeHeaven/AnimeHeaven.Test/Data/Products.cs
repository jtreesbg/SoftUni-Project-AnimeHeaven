namespace AnimeHeaven.Test.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using AnimeHeaven.Data.Models;

    public static class Products
    {
        public static IEnumerable<Product> GetTenProducts
                => Enumerable.Range(0, 10).Select(i => new Product { });
    }
}
