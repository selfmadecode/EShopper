using E_Shopper.Data;
using E_Shopper.Models.Entities;
using E_Shopper.Models.Enums;
using E_Shopper.Models.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shopper.Models.Services.Repository
{
    public class ProductServices : IProduct
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductServices(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        //Get all products
        public async Task AddProduct(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IList<Product>> AllProduct()
        {
            return await _dbContext.Products.Where(p => p.ProductStatus == null).ToListAsync();
        }

        public async Task<bool> StoreKeeperAssignProductToSupervisor(List<Product> products,
            string storeKeeperId, string supervisorId)
        {
            if (products is null)
            {
                // Handle this
                //throw new ArgumentNullException(nameof(products));

                return false;
            }

            // Remove product from product page
            // _dbContext.Products.RemoveRange(products);

            var storeKeeper = _dbContext.StoreKeepers.FirstOrDefault(u => u.UserId == storeKeeperId);

            var supervisor = _dbContext.Supervisors.FirstOrDefault(s => s.UserId == supervisorId);


            foreach (var product in products)
            {
                product.StoreKeeperId = storeKeeperId;
                product.SupervisorId = supervisorId;
                product.ProductStatus = ProductStatus.ProductAssignedToSupervisor;
            };

            supervisor.Products = new List<Product>();
            supervisor.Products.AddRange(products);

            _dbContext.UpdateRange(products);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IList<Product>> GetProductsAssignedToSupervisor(
            string supervisorId)
        {
            //var supervisor = _dbContext.Supervisors.FirstOrDefault(s => s.UserId == userId);
            return await _dbContext.Products.Where(s => s.SupervisorId == supervisorId).ToListAsync();
        }

        public async Task SupervisorProcessProduct(List<Product> products, Decision status,
            string projectManagerId, string storeKeeperId)
        {
            switch (status)
            {
                case Decision.Accept:
                    var projectManager = await _dbContext.ProductManagers
                        .FirstOrDefaultAsync(p => p.UserId == projectManagerId);

                    foreach (var product in products)
                    {
                        product.ProductStatus = ProductStatus.SuppervisorApproved;
                    }

                    //Assign to ProjectManager
                    projectManager.Products = new List<Product>();
                    projectManager.Products.AddRange(products);
                    break;


                case Decision.Reject:
                    foreach (var product in products)
                    {
                        product.ProductStatus = ProductStatus.SuppervisorDisapproved;
                    }
                    var storeKeeper = await _dbContext.StoreKeepers
                       .FirstOrDefaultAsync(s => s.UserId == storeKeeperId);
                    
                    //Assign back to storeKeeper
                    storeKeeper.Products = new List<Product>();
                    storeKeeper.Products.AddRange(products);

                    break;
            }
            _dbContext.UpdateRange(products);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsAssignedToProductManager(string userId,
            string supervisorId)
        {
            var supervisor = _dbContext.ProductManagers.FirstOrDefault(s => s.UserId == userId);
            return await _dbContext.Products.Where(s => s.SupervisorId == supervisorId).ToListAsync();
        }

        public async Task ProductManagerProcessProduct(List<Product> products,
            ProductStatus status, string projectManagerId, string supervisorId)
        {
            var projectManager = await _dbContext.ProductManagers
                .FirstOrDefaultAsync(p => p.UserId == projectManagerId);

            switch (status)
            {
                case ProductStatus.SuppervisorApproved:
                    foreach (var product in products)
                    {
                        product.ProductStatus = status;
                    }
                    //Assign to ProjectManager

                    projectManager.Products = new List<Product>();
                    projectManager.Products.AddRange(products);
                    // remove the product

                    break;

                case ProductStatus.SuppervisorDisapproved:
                    foreach (var product in products)
                    {
                        product.ProductStatus = status;
                    }
                    var storeKeeper = await _dbContext.StoreKeepers
                       .FirstOrDefaultAsync(s => s.UserId == supervisorId);

                    storeKeeper.Products = new List<Product>();
                    storeKeeper.Products.AddRange(products);
                    // remove the product

                    break;
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> SellApprovedProducts()
            => await _dbContext.Products
            .Where(p => p.ProductStatus == ProductStatus.ProductManagerApproved)
            .ToListAsync();

    }
}
