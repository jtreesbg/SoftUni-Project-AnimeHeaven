namespace AnimeHeaven.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using AnimeHeaven.Services.Profile;
    using AnimeHeaven.Infrastructure;
    using AnimeHeaven.Models.Products;
    using AnimeHeaven.Services.Products;

    public class ProfileController : Controller
    {
        private readonly IProfileService profile;
        private readonly IProductService products;

        public ProfileController(IProfileService profile, IProductService products)
        {
            this.profile = profile;
            this.products = products;
        }

        [Authorize]
        public IActionResult Account()
        {
            if (this.User.IsAdmin())
            {
                return RedirectToAction(nameof(ProductsController.All), "Products");
            }

            var userId = this.User.GetId();
            var isSeller = this.profile.IsSeller(userId);
            var customer = this.profile.GetCustomerDetails(userId);
            var seller = this.profile.GetSellerDetails(userId);
            var products = this.profile.GetSellerProducts(userId);

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

            return RedirectToAction(nameof(ProductsController.All), "Products");
        }

        [Authorize]
        public IActionResult Favourites([FromQuery] ProductsSearchQueryModel query)
        {
            var userId = this.User.GetId();

            var queryResult = this.profile.All(
                query.Category,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                ProductsSearchQueryModel.ProductsPerPage,
                userId);

            var categories = this.products.AllCategories().Select(c => c.Name);

            query.Categories = categories;
            query.TotalProducts = queryResult.TotalProducts;
            query.Products = queryResult.Products;

            return View(query);
        }
    }
}