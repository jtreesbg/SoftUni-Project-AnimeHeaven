namespace AnimeHeaven.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using AutoMapper;
    using AnimeHeaven.Models.Products;
    using AnimeHeaven.Infrastructure;
    using AnimeHeaven.Services.Products;
    using AnimeHeaven.Services.Sellers;

    public class ProductsController : Controller
    {
        private readonly IProductService products;
        private readonly ISellerService sellers;
        private readonly IMapper mapper;

        public ProductsController(IProductService products, ISellerService sellers, IMapper mapper)
        {
            this.products = products;
            this.sellers = sellers;
            this.mapper = mapper;
        }

        [Authorize]
        public IActionResult My()
        {
            var userId = this.User.GetId();
            var myProducts = this.products.ByUser(userId);

            return View(myProducts);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Add()
        {
            if (User.IsAdmin())
            {
                return RedirectToAction(nameof(ProductsController.All), "Products");
            }

            if (!this.sellers.IsSeller(this.User.GetId()))
            {
                return RedirectToAction(nameof(SellersController.Become), "Sellers");
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
            if (User.IsAdmin())
            {
                return RedirectToAction(nameof(ProductsController.All), "Products");
            }

            var sellerId = this.sellers.IdByUser(this.User.GetId());

            if (sellerId == 0)
            {
                return RedirectToAction(nameof(SellersController.Become), "Sellers");
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
            var model = this.products.Details(id);
            return View(model);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();

            if (!this.sellers.IsSeller(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(SellersController.Become), "Sellers");
            }

            var product = this.products.Details(id);

            if (product.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            var productForm = this.mapper.Map<ProductFormModel>(product);

            productForm.Categories = this.products.AllCategories();

            return View(productForm);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, ProductFormModel product)
        {
            var sellerId = this.sellers.IdByUser(this.User.GetId());

            if (sellerId == 0 && !User.IsAdmin())
            {
                return RedirectToAction(nameof(SellersController.Become), "Sellers");
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

            if (!this.products.IsBySeller(id, sellerId) && !User.IsAdmin())
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

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            this.products.Delete(id);

            return RedirectToAction(nameof(My));
        }
    }

}