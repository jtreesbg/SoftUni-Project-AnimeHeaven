namespace AnimeHeaven.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using AnimeHeaven.Data.Models;

    public class AnimeHeavenDbContext : IdentityDbContext
    {
        public AnimeHeavenDbContext(DbContextOptions<AnimeHeavenDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; init; }
        public DbSet<Category> Categories { get; init; }
        public DbSet<Seller> Sellers { get; init; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Favourites> Favourites { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Favourites>()
                .HasKey(f => new { f.ProductId, f.CustomerId });

            builder
                .Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Seller>()
                .HasOne<IdentityUser>()
                .WithOne()
                .HasForeignKey<Seller>(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Product>()
                .HasOne(p => p.Seller)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}