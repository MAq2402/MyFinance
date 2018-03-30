using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Controllers
{
    public class AccountController:Controller
    {
        private SignInManager<User> _signIngManager;
        private UserManager<User> _userManager;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signIngManager = signInManager;
            _userManager = userManager;
        }
        //[HttpGet]
        //public IActionResult Login()
        //{

        //}
        //[HttpPost]
        //public IActionResult Login()
        //{

        //}

        //[HttpGet]
        //public IActionResult Register()
        //{

        //}
        //[HttpPost]
        //public IActionResult Register()
        //{

        //}
        //[HttpPost]
        //public IActionResult Logout()
        //{

        //}
    }
}
