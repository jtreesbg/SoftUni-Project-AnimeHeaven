namespace AnimeHeaven.Controllers
{
    using System.Linq;
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
            var user = this.profile.GetCustomerDetails(id);
            var products = this.profile.GetCustomerShoppingCartProducts(id);
            var totalPrice = products.Select(p => p.Price).Sum();

            var userInfo = new ProfileShoppingCartServiceModel
            {
                Id = id,
                FullName = user.FullName,
                Products = products,
                TotalPrice = totalPrice
            };

            return View(userInfo);
        }

        public IActionResult Add(int id)
        {
            var userId = this.User.GetId();

            this.profile.AddProductToShoppingCart(userId, id);

            return RedirectToAction(nameof(MyCart));
        }

        public IActionResult RemoveFromShoppingCart(int id)
        {
            var userId = this.User.GetId();

            this.profile.RemoveProductFromShoppingCart(userId, id);

            return RedirectToAction(nameof(MyCart));
        }

        public IActionResult BuyProducts()
        {
            var userId = this.User.GetId();

            this.profile.EmptyShoppingCart(userId);

            return View();
        }
    }
}
