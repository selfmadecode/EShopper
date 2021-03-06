using E_Shopper.Data;
using E_Shopper.Models.Services.Interfaces;
using E_Shopper.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shopper.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ICustomer _customer;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ICustomer customer
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _customer = customer;
        }

        [HttpGet]
        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string returnUrl = null)
        {

            if (!ModelState.IsValid)
                return View(loginViewModel);


            var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email,
                loginViewModel.Password, loginViewModel.RememberMe, lockoutOnFailure: false);


            if (result.Succeeded)
            {
                return Redirect(returnUrl ?? "/");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View();
            }
        }

        [HttpGet]
        public ViewResult Register()
        {
            var roles = new RegisterViewModel {
                RolesInDb = _roleManager.Roles
            };
            
            return View(roles);
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel, string returnUrl = null)
        {


            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = registerViewModel.Email,
                    Email = registerViewModel.Email,
                    FirstName = registerViewModel.FirstName,
                    LastName = registerViewModel.LastName
                };

                var result = await _userManager.CreateAsync(user, registerViewModel.Password);


                if (result.Succeeded)
                {

                    var createUser = _customer.CreateUser(user, registerViewModel.RoleName);

                    if (createUser)
                    {
                        await _userManager.AddToRoleAsync(user, registerViewModel.RoleName);
                        await _signInManager.SignInAsync(user, isPersistent: false);
                    }                    
                    return LocalRedirect(returnUrl ?? "/");

                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }


            return View(registerViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
