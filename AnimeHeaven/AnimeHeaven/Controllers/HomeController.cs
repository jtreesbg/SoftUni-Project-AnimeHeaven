namespace WebApplication1.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using AnimeHeaven.Models;
    using AnimeHeaven.Data;
    using AnimeHeaven.Models.Products;

    public class HomeController : Controller
    {
        private readonly AnimeHeavenDbContext data;

        public HomeController(AnimeHeavenDbContext data)
            => this.data = data;

        public IActionResult Index()
        {
            var products = this.data
                .Products
                .OrderByDescending(c => c.Id)
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    AnimeOrigin = p.AnimeOrigin,
                    Price = p.Price,
                    Category = p.Category.Name,
                    ImageUrl = p.ImageUrl
                })
                .Take(3)
                .ToList();

            return View(products);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
