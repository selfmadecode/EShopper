using E_Shopper.Data;
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

            var _dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

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


            if (await userManager.FindByNameAsync("storekeeper@eshopper.com") == null)
            {
                ApplicationUser storeKeeper = new ApplicationUser
                {
                    Email = "storekeeper@eshopper.com",
                    EmailConfirmed = true,
                    UserName = "storekeeper@eshopper.com",
                    FirstName = "Storekeeper",
                    LastName = "Raphael"
                };

                IdentityResult createStoreKeeper = await userManager.CreateAsync(
                storeKeeper, "StoreKeeper1@eshopper");

                if (createStoreKeeper.Succeeded)
                {
                    await userManager.AddToRoleAsync(storeKeeper, "StoreKeeper");
                }

                var newStoreKeeper = new StoreKeeper
                {
                    UserId = storeKeeper.Id                    
                };

                _dbContext.StoreKeepers.Add(newStoreKeeper);
            }

            if (await userManager.FindByNameAsync("storesupervisor@eshopper.com") == null)
            {
                ApplicationUser storeSupervisor = new ApplicationUser
                {
                    Email = "storesupervisor@eshopper.com",
                    EmailConfirmed = true,
                    UserName = "storesupervisor@eshopper.com",
                    FirstName = "Supervisor",
                    LastName = "Raphael"
                };

                var createStoreSupervisor = await userManager.CreateAsync(
                    storeSupervisor, "StoreSupervisor1@eshopper");


                if (createStoreSupervisor.Succeeded)
                {
                    await userManager.AddToRoleAsync(storeSupervisor, "Supervisor");
                }

                var newSupervisor = new Supervisor
                {
                    UserId = storeSupervisor.Id
                };

                _dbContext.Supervisors.Add(newSupervisor);
            }

            if (await userManager.FindByNameAsync("storemanager@eshopper.com") == null)
            {
                ApplicationUser productManager = new ApplicationUser
                {
                    Email = "storemanager@eshopper.com",
                    EmailConfirmed = true,
                    UserName = "storemanager@eshopper.com",
                    FirstName = "Manager",
                    LastName = "Raphael"
                };

                var createStoreManager = await userManager.CreateAsync(productManager,
                    "StoreManager1@eshopper");
                if (createStoreManager.Succeeded)
                {
                    await userManager.AddToRoleAsync(productManager, "ProductManager");
                }


                var newProductManager = new ProductManager
                {
                    UserId = productManager.Id
                };

                _dbContext.ProductManagers.Add(newProductManager);
            }

            await SeedProduct(_dbContext);

            _dbContext.SaveChanges();
        }

        private static async Task SeedProduct(ApplicationDbContext context)
        {
            var _dbContext = context;

            if (!_dbContext.Products.Any())
            {
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

                _dbContext.Products.AddRange(products);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
