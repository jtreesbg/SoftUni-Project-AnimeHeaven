namespace AnimeHeaven.Test
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using MyTested.AspNetCore.Mvc;
    using AnimeHeaven.Test.Mocks;
    using AnimeHeaven.Services.Products;
    using AnimeHeaven.Services.Profile;
    using AnimeHeaven.Services.Sellers;
   
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
