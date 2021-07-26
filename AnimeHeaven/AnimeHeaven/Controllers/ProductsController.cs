namespace AnimeHeaven.Controllers
{
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using AnimeHeaven.Data;
    using AnimeHeaven.Data.Models;
    using AnimeHeaven.Models.Products;
    using Microsoft.AspNetCore.Authorization;
    using AnimeHeaven.Infrastructure;
    using AnimeHeaven.Models;
    using AnimeHeaven.Services.Products;

    public class ProductsController : Controller
    {
        private readonly IProductService products;
        private readonly AnimeHeavenDbContext data;

        public ProductsController(AnimeHeavenDbContext data, IProductService products)
        {
            this.data = data;
            this.products = products;
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.UserIsDealer())
            {
                return RedirectToAction(nameof(SellerController.Become), "Seller");
            }


            return View(new AddProductFormModel
            {
                Categories = this.GetProductCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddProductFormModel product)
        {
            var sellerId = this.data
                .Sellers
                .Where(d => d.UserId == this.User.GetId())
                .Select(d => d.Id)
                .FirstOrDefault();

            if (sellerId == 0)
            {
                return RedirectToAction(nameof(SellerController.Become), "Dealers");
            }

            if (!this.data.Categories.Any(p => p.Id == product.CategoryId))
            {
                this.ModelState.AddModelError(nameof(product.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                product.Categories = this.GetProductCategories();

                return View(product);
            }

            var productData = new Product
            {
                Name = product.Name,
                Price = product.Price,
                AnimeOrigin = product.AnimeOrigin,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Year = product.Year,
                CategoryId = product.CategoryId,
                SellerId = sellerId
            };

            this.data.Products.Add(productData);
            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");

        }

        public IActionResult All([FromQuery] ProductsSearchQueryModel query)
        {
            var queryResult = this.products.All(
                query.Category,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                ProductsSearchQueryModel.ProductsPerPage);

            var categories = this.products.AllProductsCategories();

            query.Categories = categories;
            query.TotalProducts = queryResult.TotalProducts;
            query.Products = queryResult.Products;

            return View(query);
        }

        private bool UserIsDealer()
           => this.data
               .Sellers
               .Any(d => d.UserId == this.User.GetId());

        private IEnumerable<ProductCatergoryViewModel> GetProductCategories()
            => this.data
                .Categories
                .Select(c => new ProductCatergoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();
    }
}
