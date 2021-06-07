using E_Shopper.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shopper.Models.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public string Name { get; set; }

        public int Quantity { get; set; }

        public double Amount { get; set; }

        public DateTime? ExpiringDate { get; set; }

        public ProductStatus ProductStatus { get; set; }
        public string StoreKeeperId { get; set; }
        public string SupervisorId { get; set; }
        public string ProductManagerId { get; set; }

    }
}
