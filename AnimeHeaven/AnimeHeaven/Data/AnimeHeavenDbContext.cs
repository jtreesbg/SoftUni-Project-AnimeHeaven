namespace AnimeHeaven.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using AnimeHeaven.Data.Models;

    public class AnimeHeavenDbContext : IdentityDbContext<Customer>
    {
        public AnimeHeavenDbContext(DbContextOptions<AnimeHeavenDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; init; }
        public DbSet<Category> Categories { get; init; }
        public DbSet<Seller> Sellers { get; init; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Favourite> Favourites { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<ShoppingCart>()
                .HasKey(f => new { f.ProductId, f.UserId }); 

            builder
                .Entity<Favourite>()
                .HasKey(f => new { f.ProductId, f.UserId });

            builder
                .Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Seller>()
                .HasOne<Customer>()
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