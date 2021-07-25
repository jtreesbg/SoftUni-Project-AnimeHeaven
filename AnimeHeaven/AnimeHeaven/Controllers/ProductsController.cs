namespace AnimeHeaven.Controllers
{
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using AnimeHeaven.Data;
    using AnimeHeaven.Data.Models;
    using AnimeHeaven.Models.Products;

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

        public IActionResult All([FromQuery] ProductsSearchQueryModel query)
        {
            var productsQuery = this.data.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Category))
            {
                productsQuery = productsQuery.Where(p => p.Category.Name == query.Category);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                productsQuery = productsQuery.Where(p =>
                    p.Name.ToLower().Contains(query.SearchTerm.ToLower()) ||
                    p.AnimeOrigin.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            productsQuery = query.Sorting switch
            {
                ProductsSorting.Anime => productsQuery.OrderBy(p => p.AnimeOrigin),
                ProductsSorting.Year => productsQuery.OrderBy(p => p.Year),
                ProductsSorting.DateCreated or _ => productsQuery.OrderByDescending(p => p.Id)
            };

            var totalProducts = productsQuery.Count();

            var products = productsQuery
                .Skip((query.CurrentPage - 1) * ProductsSearchQueryModel.ProductsPerPage)
                .Take(ProductsSearchQueryModel.ProductsPerPage)
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    AnimeOrigin = p.AnimeOrigin,
                    Price = p.Price,
                    Category = p.Category.Name,
                    ImageUrl = p.ImageUrl
                })
                .ToList();

            var categories = this.data
                .Products
                .Select(c => c.Category.Name)
                .Distinct()
                .ToList();

            query.TotalProducts = totalProducts;
            query.Categories = categories;
            query.Products = products;

            return View(query);
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
