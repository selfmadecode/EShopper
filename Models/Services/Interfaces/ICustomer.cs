using E_Shopper.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shopper.Models.Services.Interfaces
{
    public interface ICustomer
    {
        bool CreateUser(ApplicationUser user, string role);
    }
}
