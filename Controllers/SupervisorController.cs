using E_Shopper.Models.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shopper.Controllers
{
    public class SupervisorController : Controller
    {
        private readonly IProduct _productRepo;
        public SupervisorController(IProduct productRepo)
        {
            _productRepo = productRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
