using E_Shopper.Data;
using E_Shopper.Models.Entities;
using E_Shopper.Models.Enums;
using E_Shopper.Models.Services.Interfaces;
using E_Shopper.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace E_Shopper.Controllers
{
    [Authorize]
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

            // if the user accepts the product and selects a role
            if ((assignedProducts.Decision == Decision.Accept) &&
                (!string.IsNullOrEmpty(assignedProducts.SendBackTo)))
            {
                ViewBag.SelectOneRole = "Accepted Product cannot be assigned back to Supervisors";

                var product = await SetModelState(assignedProducts);

                return View("Index", product);
            }

            // if the user rejects the products and a role 
            // is not selected to send the product back to
            if((assignedProducts.Decision == Decision.Reject) &&
                (string.IsNullOrEmpty(assignedProducts.SendBackTo)))
            {
                ViewBag.SelectOneRole = "Select a role to send to";

                var product = await SetModelState(assignedProducts);

                return View("Index", product);
            }



            var productManager = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _productRepo.ProductManagerProcessProduct(assignedProducts.ProductsToAssigns,
                assignedProducts.Decision, assignedProducts.SendBackTo, productManager, assignedProducts.Comment);

            if (!result)
                throw new Exception("Something went wrong!");
            return RedirectToAction("Index");
        }

        private async Task<ProductAndRolesViewModel> SetModelState(ProductAndRolesViewModel assignedProducts)
        {
            var product = new ProductAndRolesViewModel
            {
                Products = assignedProducts.ProductsToAssigns,
                Supervisors = await _userManager.GetUsersInRoleAsync("Supervisor")
            };

            return product;
        }
    }
}
