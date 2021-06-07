using E_Shopper.Models.Entities;
using E_Shopper.Models.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shopper.Controllers
{
    public class StoreKeeperController : Controller
    {
        private readonly IProduct _productRepo;
        public StoreKeeperController(IProduct productRepo)
        {
            _productRepo = productRepo;
        }
        public async Task<IActionResult> Index()
        {           
            return View(await _productRepo.AllProduct());
        }

        [HttpGet]
        public ViewResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<ViewResult> Add(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            await _productRepo.AddProduct(product);

            return View();
        }
    }
}
