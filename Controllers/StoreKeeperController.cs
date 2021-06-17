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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            var product = new ProductAndRolesViewModel
            {
                Products = await _productRepo.AllProduct(userId),
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
        public async Task<IActionResult> Add(Product product)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("Add", product);
            }
           var result = await _productRepo.AddProduct(product);

            return Json(new { success = true, responseText = "Success"});

        }
        public async Task<IActionResult> AssignProductToSupervisor(AssignProductToSupervisor assignedProducts)
        {

            if (!ModelState.IsValid)
            {
                var product = new ProductAndRolesViewModel
                {
                    Products = assignedProducts.ProductsToAssigns,
                    Supervisors = await _userManager.GetUsersInRoleAsync("Supervisor")
                };

                return PartialView("Index", product);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var assignTo = await _productRepo.StoreKeeperAssignProductToSupervisor(
                assignedProducts.ProductsToAssigns, userId, assignedProducts.SendToRole);

            if (!assignTo)
                throw new Exception("Something went wrong");

            return Json(new { response = true, responseText = "Success" });
        }
    }
}
