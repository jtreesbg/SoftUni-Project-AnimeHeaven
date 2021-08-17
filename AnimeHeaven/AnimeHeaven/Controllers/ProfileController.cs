namespace AnimeHeaven.Controllers
{
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using AnimeHeaven.Services.Profile;
    using AnimeHeaven.Infrastructure;
    using AnimeHeaven.Models.Products;
    using AnimeHeaven.Services.Products;
    using AnimeHeaven.Data.Models;

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

            var seller = new Seller();
            var products = new List<Product>();

            var userId = this.User.GetId();
            var customer = this.profile.GetCustomerDetails(userId);
            var isSeller = this.profile.IsSeller(userId);

            if (isSeller == true)
            {
                seller = this.profile.GetSellerDetails(userId);
                products = this.profile.GetSellerProducts(userId);
            }

            var profileInfo = new ProfileInfoServiceModel
            {
                Id = userId,
                Name = customer.UserName,
                FullName = customer.FullName,
                ProductsForSale = isSeller ? products.Count : 0,
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
        public IActionResult RemoveFavourite(int id)
        {
            var userId = this.User.GetId();
            this.profile.RemoveProductFromFavourites(userId, id);

            return RedirectToAction(nameof(ProfileController.Favourites), "Profile");
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