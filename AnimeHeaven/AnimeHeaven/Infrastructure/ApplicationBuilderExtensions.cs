namespace AnimeHeaven.Infrastructure
{
    using System.Linq;
    using AnimeHeaven.Data;
    using AnimeHeaven.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<AnimeHeavenDbContext>();

            data.Database.Migrate();

            SeedCategoires(data);

            return app;
        }

        private static void SeedCategoires(AnimeHeavenDbContext data)
        {
            if (data.Categories.Any())
            {
                return;
            }

            data.Categories.AddRange(new[]
            {
                new Category{ Name = "Figures"},
                new Category{ Name = "Posters"},
                new Category{ Name = "Accessories"},
                new Category{ Name = "Clothing"},
                new Category{ Name = "Plush"}
            });

            data.SaveChanges();
        }
    }
}
