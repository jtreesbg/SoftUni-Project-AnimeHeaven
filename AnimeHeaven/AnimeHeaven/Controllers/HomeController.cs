namespace WebApplication1.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using AnimeHeaven.Models;
    using AnimeHeaven.Services.Products;

    public class HomeController : Controller
    {
        private readonly IProductService products;

        public HomeController(IProductService products)
        {
            this.products = products;
        }

        public IActionResult Index()
        {
            var recent = this.products.GetRecentProducts();

            return View(recent);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
