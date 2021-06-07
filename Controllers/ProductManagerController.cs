using E_Shopper.Models.Entities;
using E_Shopper.Models.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shopper.Controllers
{
    public class ProductManagerController : Controller
    {
        private readonly IProduct _productRepo;

        public ProductManagerController(IProduct productRepo)
        {
            _productRepo = productRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ViewResult Add(Product product)
        {
            return View();
        }
    }
}
