namespace AnimeHeaven.Test.Routing
{
    using AnimeHeaven.Controllers;
    using AnimeHeaven.Models.Products;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    public class ProfileControllerTest
    {
        private const int id = 3;

        [Fact]
        public void AccountRoutingShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Profile/Account")

                .To<ProfileController>(c => c.Account());
        [Fact]
        public void AddToFavouritesRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap($"Profile/AddToFavourites/{id}")
            .To<ProfileController>(c => c.AddToFavourites(id));

        [Fact]
        public void RemoveFavouriteRouteShouldBeMapped()
           => MyRouting
               .Configuration()
               .ShouldMap($"Profile/RemoveFavourite/{id}")
           .To<ProfileController>(c => c.RemoveFavourite(id));

        [Fact]
        public void FavouritesRoutingShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Profile/Favourites")
                .To<ProfileController>(c => c.Favourites(With.Any<ProductsSearchQueryModel>()));
    }
}
