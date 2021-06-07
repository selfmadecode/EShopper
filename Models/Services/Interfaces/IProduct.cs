﻿using E_Shopper.Models.Entities;
using E_Shopper.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shopper.Models.Services.Interfaces
{
    public interface IProduct
    {
        Task AddProduct(Product product);

        Task<IEnumerable<Product>> AllProduct();

        Task StoreKeeperAssignProductToSupervisor(List<Product> products, string userId, string supervisorId);

        Task<IEnumerable<Product>> GetProductsAssignedToSupervisor(string userId,
            string supervisorId);

        Task SupervisorProcessProduct(List<Product> products,
            ProductStatus status, string projectManagerId,
            string storeKeeperId);

        Task<IEnumerable<Product>> GetProductsAssignedToProductManager(string userId,
            string supervisorId);

        Task ProductManagerProcessProduct(List<Product> products, ProductStatus status,
            string projectManagerId, string supervisorId);
        Task<IEnumerable<Product>> SellApprovedProducts();
    }
}