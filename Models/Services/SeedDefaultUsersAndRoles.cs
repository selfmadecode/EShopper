﻿using E_Shopper.Data;
using E_Shopper.Models.Entities;
using E_Shopper.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shopper.Models.Services
{
    public class SeedDefaultUsersAndRoles
    {
        public static void CreateUsers(IServiceProvider serviceProvider)
        {
            CreateUsersAsync(serviceProvider).Wait();
        }
        private static async Task CreateUsersAsync(IServiceProvider serviceProvider)
        {
            serviceProvider = serviceProvider.CreateScope().ServiceProvider;

            UserManager<ApplicationUser> userManager = serviceProvider.
                GetRequiredService<UserManager<ApplicationUser>>();

            RoleManager<IdentityRole> roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "StoreKeeper",
                "Supervisor",
                "ProductManager" };

            IdentityResult roleResult;



            foreach (var newRoleName in roleNames)
            {
                var roleExists = await roleManager.RoleExistsAsync(newRoleName);

                if (!roleExists)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(newRoleName));
                }
            }


            ApplicationUser storeKeeper = new ApplicationUser
            {
                Email = "storekeeper@eshopper.com",
                EmailConfirmed = true,
                UserName = "storekeeper@eshopper.com",
                FirstName = "Anyanwu",
                LastName = "Raphael"
            };

            ApplicationUser storeSupervisor = new ApplicationUser
            {
                Email = "storesupervisor@eshopper.com",
                EmailConfirmed = true,
                UserName = "storesupervisor@eshopper.com",
                FirstName = "Raphael",
                LastName = "Raphael"
            };
            ApplicationUser storeManager = new ApplicationUser
            {
                Email = "storemanager@eshopper.com",
                EmailConfirmed = true,
                UserName = "storemanager@eshopper.com",
                FirstName = "Raphael",
                LastName = "Raphael"
            };


            IdentityResult createStoreKeeper = await userManager.CreateAsync(
                storeKeeper, "StoreKeeper1@eshopper");

    

            if (createStoreKeeper.Succeeded)
            {
               await userManager.AddToRoleAsync(storeKeeper, "StoreKeeper");
            }

            var createStoreSupervisor = await userManager.CreateAsync(
               storeSupervisor, "StoreSupervisor1@eshopper");

            
            if (createStoreSupervisor.Succeeded)
            {
                await userManager.AddToRoleAsync(storeSupervisor, "Supervisor");
            }

            var createStoreManager = await userManager.CreateAsync(storeManager,
                "StoreManager1@eshopper");
            if (createStoreManager.Succeeded)
            {
                await userManager.AddToRoleAsync(storeManager, "ProductManager");
            }


            await SeedProduct(serviceProvider);
        }

        private static async Task SeedProduct(IServiceProvider serviceProvider)
        {
            var db = serviceProvider.GetRequiredService<ApplicationDbContext>();

            var products = new List<Product>
            {
                new Product
                {
                    Name = "Gala",
                    Amount = 23,
                    Quantity = 2,
                },
                new Product
                {
                    Name = "Charger",
                    Amount = 203,
                    Quantity = 2,
                },
                new Product
                {
                    Name = "Creame",
                    Amount = 3,
                    Quantity = 2,
                }
            };

            db.Products.AddRange(products);
            await db.SaveChangesAsync();
        }
    }
}
