using E_Shopper.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shopper.Models.ViewModel
{
    public class ProductViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Item name cannot be empty")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [Range(typeof(int), "1", "100000", ErrorMessage = "Quantity range is between 1-100000")]
        public int Quantity { get; set; }

        [Required]
        [Range(typeof(decimal), "1", "1000000000000", ErrorMessage = "Amount should be greater than 0 or less than a billion")]
        public decimal Amount { get; set; }

        [Display(Name = "Expiration Date")] // what is shown on the form label
        public DateTime? ExpiringDate { get; set; }

        /*
        [Required(ErrorMessage = "choose file")]
        [Display(Name = "Upload Image")]
        public string ImagePath { get; set; }


        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        */
        // Category Navigation prop

    }
}
