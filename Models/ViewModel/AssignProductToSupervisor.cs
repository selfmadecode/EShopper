using E_Shopper.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shopper.Models.ViewModel
{
    public class AssignProductToSupervisor
    {
        [Required(ErrorMessage ="Select a supervisor")]
        public string SendToRole { get; set; }

        [Required]
        public List<Product> ProductsToAssigns { get; set; }
    }
}
