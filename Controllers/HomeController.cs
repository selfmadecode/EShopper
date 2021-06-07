using E_Shopper.Models;
using E_Shopper.Models.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shopper.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProduct _productRepo;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IProduct productRepo , ILogger<HomeController> logger)
        {
            _productRepo = productRepo;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
