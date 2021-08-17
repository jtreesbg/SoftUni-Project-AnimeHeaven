namespace AnimeHeaven.Test.Mocks
{
    using System.Collections.Generic;
    using AnimeHeaven.Services.Products;
    using Moq;
    using MyTested.AspNetCore.Mvc;

    public class ProductServiceMock
    {
        public static string UserId => "1b99c696-64f5-443a-ae1e-8o4t8wk7c1gr";

        public static IProductService Instance
        {
            get
            {
                var mock = new Mock<IProductService>();

                mock
                    .Setup(ps => ps.GetRecentProducts())
                    .Returns(new List<ProductServiceModel>()
                    {
                        new ProductServiceModel
                        {
                         Id = 1,
                         AnimeOrigin = "anime1",
                         CategoryName = "category1",
                         ImageUrl = "img1",
                         Name = "name1",
                         Price = 1,
                         Year = 2001
                        },
                        new ProductServiceModel
                        {
                         Id = 2,
                         AnimeOrigin = "anime2",
                         CategoryName = "category2",
                         ImageUrl = "img2",
                         Name = "name2",
                         Price = 2,
                         Year = 2002
                        },
                        new ProductServiceModel
                        {
                         Id = 3,
                         AnimeOrigin = "anime3",
                         CategoryName = "category3",
                         ImageUrl = "img3",
                         Name = "name3",
                         Price = 3,
                         Year = 2003
                        }
                    });

                mock
                    .Setup(ps => ps.ByUser(UserId))
                    .Returns(new List<ProductServiceModel>()
                    {
                        new ProductServiceModel()
                        {
                         Id = 1,
                         AnimeOrigin = "anime1",
                         CategoryName = "category1",
                         ImageUrl = "img1",
                         Name = "name1",
                         Price = 1,
                         Year = 2001
                        }
                    });

                mock
                    .Setup(ps => ps.AllCategories())
                    .Returns(new List<ProductCatergoryServiceModel>()
                    {
                        new ProductCatergoryServiceModel()
                        {
                             Id = 1,
                             Name = "1"
                        },
                        new ProductCatergoryServiceModel()
                        {
                             Id = 2,
                             Name = "2"
                        },
                    });

                mock
                    .Setup(ps => ps.CategoryExists(10))
                    .Returns(false);

                mock
                     .Setup(ps => ps.CategoryExists(1))
                     .Returns(true);

                mock
                     .Setup(ps => ps.All("test", "test", Models.ProductsSorting.Anime, 1, 6))
                     .Returns(new ProductQueryServiceModel
                     {
                         TotalProducts = 3,
                         CurrentPage = 1,
                         Products = new List<ProductServiceModel>(),
                         ProductsPerPage = 6
                     });

                mock
                    .Setup(ps => ps.Details(1))
                    .Returns(new ProductDetailsServiceModel()
                    {
                        Name = "name",
                        UserId = TestUser.Identifier
                    });

                mock
                    .Setup(ps => ps.Details(0))
                    .Returns(new ProductDetailsServiceModel()
                    {
                        Name = "name",
                        UserId = "null"
                    });

                mock
                  .Setup(ps => ps.Delete(1))
                  .Returns(true);

                mock
                    .Setup(ps => ps.IsBySeller(0, 0))
                    .Returns(false);

                mock
                     .Setup(ps => ps.IsBySeller(1, 1))
                     .Returns(true);

                mock
                    .Setup(ps => ps.Edit(1, "true", 20, "true", "true", "https://www.true.bg/true.jpg", 2021, 1, 1))
                    .Returns(true);

                return mock.Object;
            }
        }
    }
}