namespace AnimeHeaven.Test
{
    using AnimeHeaven.Services.Products;
    using AnimeHeaven.Services.Profile;
    using AnimeHeaven.Services.Sellers;
    using AnimeHeaven.Test.Mocks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using MyTested.AspNetCore.Mvc;

    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration)
            : base(configuration)
        {
        }

        public void ConfigureTestServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.ReplaceTransient<IProductService>(_ => ProductServiceMock.Instance);
            services.ReplaceTransient<ISellerService>(_ => SellerServiceMock.Instance);
            services.ReplaceTransient<IProductService>(_ => ProductServiceMock.Instance);
            services.ReplaceTransient<IProfileService>(_ => ProfileServiceMock.Instance);
        }
    }
}
