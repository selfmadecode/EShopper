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
    public class StoreKeeperController : Controller
    {
        private readonly IProduct _productRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public StoreKeeperController(IProduct productRepo,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            _productRepo = productRepo;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {

            var product = new ProductAndRolesViewModel
            {
                Products = await _productRepo.AllProduct(),
                Supervisors = await _userManager.GetUsersInRoleAsync("Supervisor")
            };            

            return View(product);
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
        public async Task<IActionResult> AssignProductToSupervisor(ProductAndRolesViewModel assignedProducts)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var assignTo = await _productRepo.StoreKeeperAssignProductToSupervisor(
                assignedProducts.ProductsToAssigns, userId, assignedProducts.SendToRole);

            if (!assignTo)
                return RedirectToAction("Index");

            return RedirectToAction("Index");
        }
    }
}
