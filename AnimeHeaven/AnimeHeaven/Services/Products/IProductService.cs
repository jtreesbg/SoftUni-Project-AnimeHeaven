namespace AnimeHeaven.Services.Products
{
    using System.Collections.Generic;
    using AnimeHeaven.Models;

    public interface IProductService
    {
        ProductQueryServiceModel All(
            string category,
            string searchTerm,
            ProductsSorting sorting,
            int currentPage,
            int productsPerPage);

        IEnumerable<string> AllProductsCategories();
    }
}
