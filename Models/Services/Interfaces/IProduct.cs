using E_Shopper.Data;
using E_Shopper.Models.Entities;
using E_Shopper.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shopper.Models.Services.Interfaces
{
    public interface IProduct
    {
        Task<bool> AddProduct(Product product);

        Task<IList<Product>> AllProduct(string userId);

        Task<bool> StoreKeeperAssignProductToSupervisor(List<Product> products, string userId, string supervisorId);

        Task<IList<Product>> GetProductsAssignedToSupervisor(
            string supervisorId);

        Task<bool> SupervisorProcessProduct(List<Product> products,
            Decision status, string sendTo, string supervisorId, string comment);

        Task<IList<Product>> GetProductsAssignedToProductManager(
            string supervisorId);

        Task<bool> ProductManagerProcessProduct(List<Product> products, Decision status,
            string projectManagerId, string supervisorId, string comment);
        Task<IEnumerable<Product>> SellApprovedProducts();
    }
}
