namespace AnimeHeaven.Test.Mocks
{
    using Moq;
    using MyTested.AspNetCore.Mvc;
    using System.Collections.Generic;
    using AnimeHeaven.Data.Models;
    using AnimeHeaven.Services.Products;
    using AnimeHeaven.Services.Profile;

    public class ProfileServiceMock
    {
        public static IProfileService Instance
        {
            get
            {
                var mock = new Mock<IProfileService>();

                mock
                    .Setup(ps => ps.IsSeller(TestUser.Identifier))
                    .Returns(true);

                mock
                    .Setup(ps => ps.IsSeller("test"))
                    .Returns(false);

                mock
                   .Setup(ps => ps.GetCustomerDetails(TestUser.Identifier))
                   .Returns(new Customer
                   {
                       FullName = "fullName",
                       UserName = TestUser.Username,
                       Id = TestUser.Identifier
                   });

                mock
                   .Setup(ps => ps.GetCustomerDetails("fail"))
                   .Returns(new Customer
                   {
                       FullName = "fail",
                       UserName = "fail",
                       Id = "fail"
                   });

                mock
                    .Setup(ps => ps.GetSellerProducts(TestUser.Identifier))
                    .Returns(new List<Product>()
                    {
                        new Product()
                        {
                            Id = 1,
                            Name = "name",
                            Description = "description",
                            CategoryId = 1,
                            SellerId = 1,
                            UserId = TestUser.Identifier
                        }
                    });

                mock
                    .Setup(ps => ps.GetSellerDetails(TestUser.Identifier))
                    .Returns(new Seller
                    {
                        Id = 1,
                        UserId = TestUser.Identifier,
                        Username = TestUser.Username,
                        Address = "address",
                        Email = "email",
                        PhoneNumber = "0888888888",
                        Products = new List<Product>() { new Product { Id = 1, Name = "name" } }
                    });

                mock
                   .Setup(ps => ps.GetSellerDetails("fail"))
                   .Returns(new Seller
                   {
                       UserId = "fail",
                       Username = "fail",
                       Address = "fail",
                       Email = "fail",
                       Id = 0,
                       PhoneNumber = "fail",
                       Products = null
                   });

                mock
                    .Setup(ps => ps.AddProductToUserFavourite(TestUser.Identifier, 1))
                    .Returns(true);

                mock
                    .Setup(ps => ps.AddProductToUserFavourite("fail", 0))
                    .Returns(false);

                mock
                  .Setup(ps => ps.RemoveProductFromFavourites(TestUser.Identifier, 1))
                  .Returns(true);

                mock
                    .Setup(ps => ps.RemoveProductFromFavourites("fail", 0))
                    .Returns(false);

                mock
                     .Setup(ps => ps.All("test", "test", Models.ProductsSorting.Anime, 1, 6, TestUser.Identifier))
                     .Returns(new ProductQueryServiceModel
                     {
                         TotalProducts = 3,
                         CurrentPage = 1,
                         Products = new List<ProductServiceModel>(),
                         ProductsPerPage = 6
                     });

                mock
                    .Setup(ps => ps.AddProductToShoppingCart("TestId", 1))
                    .Returns(true);

                mock
                    .Setup(ps => ps.AddProductToShoppingCart("fail", 0))
                    .Returns(false);

                mock
                   .Setup(ps => ps.RemoveProductFromShoppingCart("TestId", 1))
                   .Returns(true);

                mock
                    .Setup(ps => ps.RemoveProductFromShoppingCart("fail", 0))
                    .Returns(false);

                mock
                   .Setup(ps => ps.EmptyShoppingCart("TestId"))
                   .Returns(true);

                mock
                    .Setup(ps => ps.EmptyShoppingCart("fail"))
                    .Returns(false);

                return mock.Object;
            }
        }
    }
}
