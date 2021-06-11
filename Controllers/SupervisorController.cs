using E_Shopper.Data;
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
    public class SupervisorController : Controller
    {
        private readonly IProduct _productRepo;
        private readonly UserManager<ApplicationUser> _userManager;

        public SupervisorController(IProduct productRepo,
            UserManager<ApplicationUser> userManager)
        {
            _productRepo = productRepo;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var product = new ProductAndRolesViewModel
            {
                Products = await _productRepo.GetProductsAssignedToSupervisor(userId),
                ProductManager = await _userManager.GetUsersInRoleAsync("ProductManager"),
                StoreKeeper = await _userManager.GetUsersInRoleAsync("StoreKeeper")                
            };

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> ProcessProduct(ProductAndRolesViewModel assignedProducts)
        {
            var supervisorId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _productRepo.SupervisorProcessProduct(
                assignedProducts.ProductsToAssigns,
                assignedProducts.Decision,
                assignedProducts.SendToRole ?? assignedProducts.SendBackTo,
                // if sendToRole is null, use sendback to 
                supervisorId);

            return RedirectToAction("Index");
        }
    }
}
