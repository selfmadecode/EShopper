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
        public async Task<bool> AddProduct(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public void CreateUser(ApplicationUser user, string role)
        {

        }

        public async Task<IList<Product>> AllProduct(string userId)
        {
            //The difference is that | always checks both the left and right conditions, while || only
            //checks the right-side condition if it's necessary (if the left side evaluates to false).
            return await _dbContext.Products.Where(p => p.ProductStatus == null | p.StoreKeeperId == userId).ToListAsync();
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

            var storeKeeper = _dbContext.StoreKeepers.FirstOrDefault(u => u.UserId == storeKeeperId);

            var supervisor = _dbContext.Supervisors.FirstOrDefault(s => s.UserId == supervisorId);

            if (storeKeeper == null || supervisor == null)
                return false;


            foreach (var product in products)
            {
                product.SentBy = storeKeeperId;
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

        public async Task<bool> SupervisorProcessProduct(List<Product> products, Decision status,
            string sendTo, string supervisorId, string comment)
        {
            switch (status)
            {
                case Decision.Accept:
                    var projectManager = await _dbContext.ProductManagers
                        .FirstOrDefaultAsync(p => p.UserId == sendTo);

                    if (projectManager == null)
                        return false;

                    foreach (var product in products)
                    {
                        product.ProductStatus = ProductStatus.SuppervisorApproved;
                        product.ProductManagerId = sendTo;
                        product.SentBy = supervisorId;
                        product.Comment = comment;
                    }

                    projectManager.Products = new List<Product>();
                    projectManager.Products.AddRange(products);
                    break;


                case Decision.Reject:

                    var storeKeeper = await _dbContext.StoreKeepers
                        .FirstOrDefaultAsync(s => s.UserId == sendTo);

                    if (storeKeeper == null)
                        return false;

                    foreach (var product in products)
                    {
                        product.ProductStatus = ProductStatus.SuppervisorDisapproved;
                        product.StoreKeeperId = sendTo;
                        product.SentBy = supervisorId;
                        product.Comment = comment;
                    }
                    //Assign back to storeKeeper
                    storeKeeper.Products = new List<Product>();
                    storeKeeper.Products.AddRange(products);

                    break;

            }
            _dbContext.UpdateRange(products);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IList<Product>> GetProductsAssignedToProductManager(
            string supervisorId)
        {
            return await _dbContext.Products.Where(s => s.ProductManagerId == supervisorId).ToListAsync();
        }

        public async Task<bool> ProductManagerProcessProduct(List<Product> products,
            Decision status, string sendTo, string productManagerId, string comment)
        {
            var projectManager = await _dbContext.ProductManagers
                .FirstOrDefaultAsync(p => p.UserId == productManagerId);

            if (projectManager == null)
                return false;

            switch (status)
            {
                case Decision.Accept:
                    foreach (var product in products)
                    {
                        product.ProductStatus = ProductStatus.ProductManagerApproved;
                        product.ProductManagerId = null;
                        product.Comment = comment;

                    }
                    //Assign to ProjectManager

                    projectManager.Products = new List<Product>();
                    projectManager.Products.AddRange(products);

                    break;

                case Decision.Reject:

                    var supervisor = await _dbContext.Supervisors
                      .FirstOrDefaultAsync(s => s.UserId == sendTo);

                    if (supervisor == null)
                        return false;

                    foreach (var product in products)
                    {
                        product.ProductStatus = ProductStatus.ProductManagerDisapproved;
                        product.SentBy = productManagerId;
                        product.SupervisorId = sendTo;
                        product.Comment = comment;
                    }

                    supervisor.Products = new List<Product>();
                    supervisor.Products.AddRange(products);

                    break;
            }
            _dbContext.UpdateRange(products);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Product>> SellApprovedProducts()
            => await _dbContext.Products
            .Where(p => p.ProductStatus == ProductStatus.ProductManagerApproved)
            .ToListAsync();


        private void SetProductStatus(List<Product> products,
            string sentBy, string supervisorId,
            ProductStatus productStatus, string managerId, string storeKeeperId,
            string productManagerId)
        {
            foreach (var product in products)
            {
                product.SentBy = sentBy;
                product.SupervisorId = supervisorId;
                product.ProductStatus = productStatus;
                product.ProductManagerId = managerId;
                product.StoreKeeperId = storeKeeperId;
                product.ProductManagerId = productManagerId;
            }
        }

    }

    
}
