﻿namespace AnimeHeaven.Services.Products
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


        ProductQueryServiceModel All(
            string category,
            string searchTerm,
            ProductsSorting sorting,
            int currentPage,
            int productsPerPage);

        IEnumerable<ProductServiceModel> ByUser(string userId);

        IEnumerable<ProductCatergoryServiceModel> AllCategories();

        ProductDetailsServiceModel Details(int id);

        bool CategoryExists(int categoryId);
    }
}
