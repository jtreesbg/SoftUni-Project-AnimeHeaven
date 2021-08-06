namespace AnimeHeaven.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using AnimeHeaven.Services.Profile;
    using AnimeHeaven.Infrastructure;
    using Microsoft.AspNetCore.Authorization;

    public class ProfileController : Controller
    {
        private readonly IProfileService profile;

        public ProfileController(IProfileService profile)
        {
            this.profile = profile;
        }

        [Authorize]
        public IActionResult Account()
        {
            var userId = this.User.GetId();
            var isSeller = this.profile.IsSeller(userId);
            var customer = this.profile.GetCustomerDetails(userId);
            var seller = this.profile.GetSellerDetails(userId);
            var products = this.profile.GetProducts(userId);

            var profileInfo = new ProfileInfoServiceModel
            {
                Id = userId,
                Name = customer.UserName,
                FullName = customer.FullName,
                Products = products,
                ProductsForSale = products.Count,
                Email = customer.Email,
                Role = isSeller ? "Seller" : "Customer",
                Phone = isSeller ? seller.PhoneNumber : "Need to become Seller",
                Address = isSeller ? seller.Address : "Need to become Seller",
            };

            return View(profileInfo);
        }

        [Authorize]
        public IActionResult AddToFavourites(int id)
        {
            var userId = this.User.GetId();

            this.profile.AddProductToUserFavourite(userId, id);

            return Redirect("/Products/All");
        }
    }
}