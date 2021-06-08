using E_Shopper.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shopper.Models.ViewModel
{
    public class ProductAndRoleViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
