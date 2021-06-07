using E_Shopper.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Shopper.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //Added the application user class to identityDbContext

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // configuring the length and fields of firstName, LastName in ApplicationUserClass
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .Property(e => e.FirstName)
                .HasMaxLength(250);

            builder.Entity<ApplicationUser>()
                .Property(e => e.LastName)
                .HasMaxLength(250);
        }

        public DbSet<Product>Products { get; set; }
        public DbSet<StoreKeeper>StoreKeepers { get; set; }
        public DbSet<Supervisor>Supervisors { get; set; }
        public DbSet<ProductManager>ProductManagers { get; set; }
    }
}
