namespace AnimeHeaven.Infrastructure
{
    using AutoMapper;
    using AnimeHeaven.Models.Products;
    using AnimeHeaven.Services.Products;
    using AnimeHeaven.Data.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Product, ProductServiceModel>();
            this.CreateMap<Product, ProductDetailsServiceModel>();
            this.CreateMap<ProductDetailsServiceModel, ProductFormModel>();

            this.CreateMap<Product, ProductDetailsServiceModel>()
               .ForMember(p => p.UserId, cfg => cfg.MapFrom(p => p.Seller.UserId))
               .ForMember(p => p.SellerName, cfg => cfg.MapFrom(p => p.Seller.Username));
        }
    }
}
