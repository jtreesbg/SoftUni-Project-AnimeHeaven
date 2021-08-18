namespace AnimeHeaven.Test.Mocks
{
    using Moq;
    using MyTested.AspNetCore.Mvc;
    using AnimeHeaven.Services.Sellers;

    using static Data.Data;
    public class SellerServiceMock
    {
        public static ISellerService Instance
        {
            get
            {
                var mock = new Mock<ISellerService>();

                mock
                    .Setup(ss => ss.IsSeller(TestUser.Identifier))
                    .Returns(true);
                
                mock
                    .Setup(ss => ss.IsSeller("test"))
                    .Returns(false);

                mock
                   .Setup(ss => ss.IdByUser(TestUser.Identifier))
                   .Returns(1);

                mock
                   .Setup(ss => ss.IdByUser("test"))
                   .Returns(0);

                return mock.Object;
            }
        }
    }
}