namespace AnimeHeaven.Data
{
    using AnimeHeaven.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AnimeHeavenDbContext : IdentityDbContext
    {  
        public AnimeHeavenDbContext(DbContextOptions<AnimeHeavenDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; init; }
        public DbSet<Category> Categories { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Product>()
                .HasOne(c => c.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(p=>p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}