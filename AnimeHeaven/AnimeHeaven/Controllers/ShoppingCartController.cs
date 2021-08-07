namespace AnimeHeaven.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using AnimeHeaven.Infrastructure;
    using AnimeHeaven.Services.Profile;
    using AnimeHeaven.Services.Products;

    public class ShoppingCartController : Controller
    {
        private readonly IProfileService profile;
        private readonly IProductService products;

        public ShoppingCartController(IProfileService profile, IProductService products)
        {
            this.profile = profile;
            this.products = products;
        }

        public IActionResult MyCart()
        {
            var id = this.User.GetId();
            return View();
        }

        public IActionResult Add(int id)
        {
            var userId = this.User.GetId();

            this.profile.AddProductToShoppingCart(userId, id);

            return RedirectToAction(nameof(MyCart));
        }
    }
}
