namespace WebApplication1.Controllers
{
    using System.Linq;
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using AnimeHeaven.Models;
    using AnimeHeaven.Data;
    using AnimeHeaven.Services.Products;

    public class HomeController : Controller
    {
        private readonly AnimeHeavenDbContext data;
        private readonly IConfigurationProvider mapper;

        public HomeController(AnimeHeavenDbContext data, IConfigurationProvider mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var products = this.data
                .Products
                .OrderByDescending(c => c.Id)
                .ProjectTo<ProductServiceModel>(this.mapper)
                .Take(3)
                .ToList();

            return View(products);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
