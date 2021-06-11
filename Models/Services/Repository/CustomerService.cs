using E_Shopper.Data;
using E_Shopper.Models.Entities;
using E_Shopper.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shopper.Models.Services.Repository
{
    public class CustomerService : ICustomer
    {
        private readonly ApplicationDbContext _dbContext;

        public CustomerService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool CreateUser(ApplicationUser user, string role)
        {
            switch (role)
            {
                case "StoreKeeper":
                    StoreKeeper storeKeeper = new StoreKeeper
                    {
                        UserId = user.Id,                        
                    };
                    _dbContext.StoreKeepers.Add(storeKeeper);
                    break;

                case "Supervisor":
                    Supervisor supervisor = new Supervisor
                    {
                        UserId = user.Id
                    };
                    _dbContext.Supervisors.Add(supervisor);
                    break;

                case "ProductManager":
                    ProductManager productManager = new ProductManager
                    {
                        UserId = user.Id
                    };
                    _dbContext.ProductManagers.Add(productManager);
                    break;
            }
            _dbContext.SaveChanges();
            return true;
        }
    }
}
