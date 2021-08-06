namespace AnimeHeaven.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using AnimeHeaven.Data.Models;
    using AnimeHeaven.Models.Sellers;
    using AnimeHeaven.Infrastructure;
    using AnimeHeaven.Services.Sellers;

    public class SellersController : Controller
    {
        private readonly ISellerService sellers;

        public SellersController(ISellerService sellers)
            => this.sellers = sellers;

        [Authorize]
        public IActionResult Become() => View();

        [HttpPost]
        [Authorize]
        public IActionResult Become(BecomeSellerFormModel seller)
        {
            if (this.User.IsAdmin())
            {
                return RedirectToAction("All", "Products");
            }

            var userId = this.User.GetId();

            var customer = this.sellers.GetCustomer(userId);

            var userIdAlreadySeller = this.sellers.IsSeller(userId);

            if (userIdAlreadySeller)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(seller);
            }

            var sellerData = new Seller
            {
                UserId = userId,
                PhoneNumber = seller.PhoneNumber,
                Address = seller.Address,
                Email = customer.Email,
                Username = customer.UserName
            };

            this.sellers.SaveSellerInDb(sellerData);

            return RedirectToAction("All", "Products");
        }
    }
}