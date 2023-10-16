using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NB.Candle.WebRzorPage.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NB.Candle.WebRzorPage.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<EmailConfiguration> EmailConfigurations { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<FixedProductProperty> FixedProductProperties { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartInfo> CartInfos { get; set; }
        public DbSet<UserOtp> UserOtps { get; set; }
        public DbSet<ShippingMethod> ShippingMethods { get; set; }
        public DbSet<OrderMaster> OrderMasters { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<NormalDiscount> NormalDiscounts { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<OrderDiscount> OrderDiscounts { get; set; }
        public DbSet<Log> Logs { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<FixedProductProperty>().OwnsOne(o => o.ProductProperty);
            builder.Entity<Cart>().OwnsOne(o => o.ProductProperty);
            builder.Entity<OrderItem>().OwnsOne(o => o.ProductProperty);

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Vendor",
                    NormalizedName = "Vendor"
                }
                );
        }
    }
}
