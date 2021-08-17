namespace AnimeHeaven.Controllers
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using AnimeHeaven.Data.Models;
    using AnimeHeaven.Models.Sellers;
    using AnimeHeaven.Infrastructure;
    using AnimeHeaven.Services.Sellers;

    using static WebConstants;

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
                return RedirectToAction(nameof(ProductsController.All), "Products");
            }

            var userId = this.User.GetId();

            var userName = User.FindFirstValue(ClaimTypes.Name);
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

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
                Email = userEmail,
                Username = userName
            };

            this.sellers.SaveSellerInDb(sellerData);

            TempData[GlobalMessageKey] = "You have become a seller!";

            return RedirectToAction(nameof(ProductsController.All), "Products");
        }
    }
}