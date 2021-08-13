namespace AnimeHeaven.Infrastructure
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using AnimeHeaven.Data;
    using AnimeHeaven.Data.Models;

    using static AnimeHeaven.Areas.Admin.AdminConstants;
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);

            SeedCategoires(services);
            SeedAdministrator(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<AnimeHeavenDbContext>();

            data.Database.Migrate();
        }

        private static void SeedCategoires(IServiceProvider services)
        {
            var data = services.GetRequiredService<AnimeHeavenDbContext>();

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

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<Customer>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = AdministratorRoleName };

                    await roleManager.CreateAsync(role);

                    const string adminEmail = "admin@ah.com";
                    const string adminPassword = "123123";

                    var user = new Customer
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                        FullName = "Admin"
                    };

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}