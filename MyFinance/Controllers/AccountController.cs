using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Entities;
using MyFinance.Models.Account;
using MyFinance.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<User> _signIngManager;
        private UserManager<User> _userManager;
        private IAppAccountService _appAccountService;
        private ITransactionCategoryService _categoryService;

        public AccountController(SignInManager<User> signInManager, 
                                 UserManager<User> userManager,
                                 IAppAccountService appAccountService,
                                 ITransactionCategoryService categoryService)
        {
            _signIngManager = signInManager;
            _userManager = userManager;
            _appAccountService = appAccountService;
            _categoryService = categoryService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Home", "Index");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            var result = await _signIngManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

            if(result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var user = new User
            {
                UserName = model.UserName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _appAccountService.AddDefaultAccountAsync(user.UserName);
                await _categoryService.AddDefaultCategoryAsync(user.UserName);

                await _signIngManager.SignInAsync(user, false);



                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {

            await _signIngManager.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }
    }
}
