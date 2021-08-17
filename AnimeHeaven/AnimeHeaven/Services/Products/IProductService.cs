namespace AnimeHeaven.Services.Products
{
    using System.Collections.Generic;
    using AnimeHeaven.Models;

    public interface IProductService
    {

        int Create(
               string name,
               double price,
               string animeOrigin,
               string description,
               string imageUrl,
               int year,
               int categoryId,
               int sellerId);
        bool Edit(
               int id,
               string name,
               double price,
               string animeOrigin,
               string description,
               string imageUrl,
               int year,
               int categoryId,
               int sellerId);

        bool Delete(int id);

        ProductQueryServiceModel All(
            string category,
            string searchTerm,
            ProductsSorting sorting,
            int currentPage,
            int productsPerPage);

        IEnumerable<ProductServiceModel> ByUser(string userId);
        bool IsBySeller(int productId, int sellerId);

        IEnumerable<ProductCatergoryServiceModel> AllCategories();

        ProductDetailsServiceModel Details(int id);

        bool CategoryExists(int categoryId);

        IEnumerable<ProductServiceModel> GetRecentProducts();
    }
}
