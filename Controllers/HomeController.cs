using E_Shopper.Data;
using E_Shopper.Models;
using E_Shopper.Models.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace E_Shopper.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProduct _productRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IProduct productRepo,
            ILogger<HomeController> logger, UserManager<ApplicationUser> userManager)
        {
            _productRepo = productRepo;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!string.IsNullOrEmpty(userId))
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (await _userManager.IsInRoleAsync(user, "StoreKeeper"))
                    return RedirectToAction("Index", "StoreKeeper");

                if (await _userManager.IsInRoleAsync(user, "ProductManager"))
                    return RedirectToAction("Index", "ProductManager");

                if (await _userManager.IsInRoleAsync(user, "Supervisor"))
                    return RedirectToAction("Index", "Supervisor");
            }

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
