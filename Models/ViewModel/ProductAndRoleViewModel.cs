using E_Shopper.Data;
using E_Shopper.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shopper.Models.ViewModel
{
    public class ProductAndRolesViewModel
    {

        //public IEnumerable<Product> AssignedProduct { get; set; }
        public string SendToRole { get; set; }

        public IList<Product> Products { get; set; }
        public IEnumerable<ApplicationUser> Supervisors { get; set; }

        public List<Product> ProductsToAssigns { get; set; }
    }

    public class ProductsToAssign
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public double Amount { get; set; }

        public DateTime? ExpiringDate { get; set; }
    }
}
