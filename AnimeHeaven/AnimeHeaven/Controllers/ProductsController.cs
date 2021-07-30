namespace AnimeHeaven.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using AnimeHeaven.Data;
    using AnimeHeaven.Models.Products;
    using Microsoft.AspNetCore.Authorization;
    using AnimeHeaven.Infrastructure;
    using AnimeHeaven.Services.Products;
    using AnimeHeaven.Services.Sellers;

    public class ProductsController : Controller
    {
        private readonly IProductService products;
        private readonly ISellerService sellers;
        private readonly AnimeHeavenDbContext data;

        public ProductsController(AnimeHeavenDbContext data, IProductService products, ISellerService sellers)
        {
            this.data = data;
            this.products = products;
            this.sellers = sellers;
        }

        [Authorize]
        public IActionResult My()
        {
            var myProducts = this.products.ByUser(this.User.GetId());

            return View(myProducts);
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.sellers.IsSeller(this.User.GetId()))
            {
                return RedirectToAction(nameof(SellersController.Become), "Seller");
            }

            return View(new ProductFormModel
            {
                Categories = this.products.AllCategories()
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(ProductFormModel product)
        {
            var sellerId = this.sellers.IdByUser(this.User.GetId());

            if (sellerId == 0)
            {
                return RedirectToAction(nameof(SellersController.Become), "Seller");
            }

            if (!this.products.CategoryExists(product.CategoryId))
            {
                this.ModelState.AddModelError(nameof(product.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                product.Categories = this.products.AllCategories();

                return View(product);
            }

            this.products.Create(
                 product.Name,
                 product.Price,
                 product.AnimeOrigin,
                 product.Description,
                 product.ImageUrl,
                 product.Year,
                 product.CategoryId,
                 sellerId
             );

            return RedirectToAction(nameof(All));

        }

        public IActionResult All([FromQuery] ProductsSearchQueryModel query)
        {
            var queryResult = this.products.All(
                query.Category,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                ProductsSearchQueryModel.ProductsPerPage);

            var categories = this.products.AllCategories().Select(c => c.Name);

            query.Categories = categories;
            query.TotalProducts = queryResult.TotalProducts;
            query.Products = queryResult.Products;

            return View(query);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var model = this.data.Products.Where(p => p.Id == id).FirstOrDefault();
            return View(model);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();

            if (!this.sellers.IsSeller(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(SellersController.Become), "Dealers");
            }

            var product = this.products.Details(id);

            if (product.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            return View(new ProductFormModel
            {
                Name = product.Name,
                Price = product.Price,
                AnimeOrigin = product.AnimeOrigin,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Year = product.Year,
                CategoryId = product.CategoryId,
                Categories = this.products.AllCategories()
            });
        }


        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, ProductFormModel product)
        {
            var sellerId = this.sellers.IdByUser(this.User.GetId());

            if (sellerId == 0 && !User.IsAdmin())
            {
                return RedirectToAction(nameof(SellersController.Become), "Seller");
            }

            if (!this.products.CategoryExists(product.CategoryId))
            {
                this.ModelState.AddModelError(nameof(product.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                product.Categories = this.products.AllCategories();

                return View(product);
            }

            if(!this.products.IsBySeller(id,sellerId) && !User.IsAdmin())
            {
                return Unauthorized();
            }

            var edited = this.products.Edit(
                   id,
                   product.Name,
                   product.Price,
                   product.AnimeOrigin,
                   product.Description,
                   product.ImageUrl,
                   product.Year,
                   product.CategoryId,
                   sellerId
               );


            if (!edited && !User.IsAdmin())
            {
                return BadRequest();
            }

            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }
    }

}
