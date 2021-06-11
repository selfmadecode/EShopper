using E_Shopper.Data;
using E_Shopper.Models.Entities;
using E_Shopper.Models.Services.Interfaces;
using E_Shopper.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace E_Shopper.Controllers
{
    public class ProductManagerController : Controller
    {
        private readonly IProduct _productRepo;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductManagerController(IProduct productRepo,
            UserManager<ApplicationUser> userManager)
        {
            _productRepo = productRepo;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var product = new ProductAndRolesViewModel
            {
                Products = await _productRepo.GetProductsAssignedToProductManager(userId),
                Supervisors = await _userManager.GetUsersInRoleAsync("Supervisor")
            };

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> ProcessProduct(ProductAndRolesViewModel assignedProducts)
        {
            var productManager = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _productRepo.ProductManagerProcessProduct(assignedProducts.ProductsToAssigns,
                assignedProducts.Decision, assignedProducts.SendToRole, productManager);

            return RedirectToAction("Index");
        }
    }
}
