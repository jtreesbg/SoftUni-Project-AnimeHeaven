namespace AnimeHeaven.Controllers
{
    using System.Linq;
    using System.Collections.Generic;
    using AnimeHeaven.Data;
    using AnimeHeaven.Data.Models;
    using AnimeHeaven.Models.Products;
    using Microsoft.AspNetCore.Mvc;

    public class ProductsController : Controller
    {
        private readonly AnimeHeavenDbContext data;

        public ProductsController(AnimeHeavenDbContext data)
        {
            this.data = data;
        }

        public IActionResult Add() => View(new AddProductFormModel
        {
            Categories = this.GetProductCategories()
        });

        [HttpPost]
        public IActionResult Add(AddProductFormModel product)
        {
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
                CategoryId = product.CategoryId
            };

            this.data.Products.Add(productData);
            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");

        }

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
