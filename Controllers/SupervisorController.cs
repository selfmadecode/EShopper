using E_Shopper.Data;
using E_Shopper.Models.Enums;
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
        private bool OperationResult;

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
            // if the user selected both the product manager role and storemanager role

            if ((!string.IsNullOrEmpty(assignedProducts.SendToRole))
                && (!string.IsNullOrEmpty(assignedProducts.SendBackTo)))
            {

                ViewBag.SelectOneRole = "Select only one role";

                var product = await SetModelState(assignedProducts);

                return View("Index", product );
            }

            // IF BOTH ROLES ARE EMPTY

            if ((string.IsNullOrEmpty(assignedProducts.SendToRole))
                && (string.IsNullOrEmpty(assignedProducts.SendBackTo)))
            {

                ViewBag.SelectOneRole = "Select a role";

                //var product = new ProductAndRolesViewModel
                //{
                //    Products = assignedProducts.ProductsToAssigns,
                //    ProductManager = await _userManager.GetUsersInRoleAsync("ProductManager"),
                //    StoreKeeper = await _userManager.GetUsersInRoleAsync("StoreKeeper")
                //};

                var product = await SetModelState(assignedProducts);


                return View("Index", product);
            }

            // IF THE USER Accepts the product and the sendToRole (productManager) feild is empty
            if((assignedProducts.Decision == Decision.Accept) && (string.IsNullOrEmpty(assignedProducts.SendToRole)))
            {
                ViewBag.SelectOneRole = "Accepted products should be sent to Product Managers";

                var product = await SetModelState (assignedProducts);

                return View("Index", product);
            }

            // If the User Rejects the product and the sendBackTo(storeManager) field is empty
            if ((assignedProducts.Decision == Decision.Reject) && (string.IsNullOrEmpty(assignedProducts.SendBackTo)))
            {
                ViewBag.SelectOneRole = "Rejected products should be sent to Store Managers";

                var product = await SetModelState (assignedProducts);

                return View("Index", product);
            }

            var supervisorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            
            switch (assignedProducts.Decision)
            {
                //if the decision is accepted, send the product to
                // product manager(SendToRole)
                case Decision.Accept:
                    OperationResult = await _productRepo.SupervisorProcessProduct(
                           assignedProducts.ProductsToAssigns, assignedProducts.Decision,
                           assignedProducts.SendToRole, supervisorId);
                    break;

                //if the decision is accepted, send the product to
                // store manager (SendBankTo)
                case Decision.Reject:
                    OperationResult = await _productRepo.SupervisorProcessProduct(
                            assignedProducts.ProductsToAssigns,
                            assignedProducts.Decision,
                            assignedProducts.SendBackTo, supervisorId);
                    break;
            }

            if (!OperationResult)
                throw new Exception("Something went wrong!");

            return RedirectToAction("Index");
        }
        private async Task<ProductAndRolesViewModel> SetModelState(ProductAndRolesViewModel assignedProducts)
        {
            var product = new ProductAndRolesViewModel
            {
                Products = assignedProducts.ProductsToAssigns,
                ProductManager = await _userManager.GetUsersInRoleAsync("ProductManager"),
                StoreKeeper = await _userManager.GetUsersInRoleAsync("StoreKeeper")
            };

            return product;
        }
    }
}
