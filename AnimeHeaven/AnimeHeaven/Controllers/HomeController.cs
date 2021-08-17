namespace AnimeHeaven.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AnimeHeaven.Services.Products;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    using static WebConstants.Cache;

    public class HomeController : Controller
    {
        private readonly IProductService products;
        private readonly IMemoryCache cache;

        public HomeController(
            IProductService products,
            IMemoryCache cache)
        {
            this.products = products;
            this.cache = cache;
        }

        public IActionResult Index()
        {
            var mostRecentProducts = this.cache.Get<List<ProductServiceModel>>(MostRecenetProductsCacheKey);

            if (mostRecentProducts == null)
            {
                mostRecentProducts = this.products.GetRecentProducts().ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.cache.Set(MostRecenetProductsCacheKey, mostRecentProducts, cacheOptions);
            }

            return View(mostRecentProducts);
        }

        public IActionResult Error() => View();
    }
}
