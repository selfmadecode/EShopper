using E_Shopper.Data;
using E_Shopper.Models.Entities;
using E_Shopper.Models.Enums;
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
        public string SendToRole { get; set; }

        public Decision Decision { get; set; }

        public IList<Product> Products { get; set; }
        public IEnumerable<ApplicationUser> Supervisors { get; set; }
        public IEnumerable<ApplicationUser> StoreKeeper { get; set; }
        public IEnumerable<ApplicationUser> ProductManager { get; set; }

        public List<Product> ProductsToAssigns { get; set; }
    }
}
