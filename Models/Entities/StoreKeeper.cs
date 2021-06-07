using E_Shopper.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shopper.Models.Entities
{
    public class StoreKeeper
    {
        public int Id { get; set; }

        public List<Product> Products { get; set; }
        
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }

    }
}
