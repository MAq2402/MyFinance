﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Repositories;
using MyFinance.Models.AppAccount;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using MyFinance.Entities;
using Microsoft.EntityFrameworkCore;
using MyFinance.Filters;
using MyFinance.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyFinance.Controllers
{
    [Authorize]
    public class AppAccountController : Controller
    {
        private IAppAccountService _accountService;

        public AppAccountController(IAppAccountService accountService)
        {
            _accountService = accountService;
        }
        
        public async Task<IActionResult> Index()
        {

            var model = new MyFinance.Models.AppAccount.IndexViewModel
            {
                Accounts = await _accountService.GetAccountsAsync(User.Identity.Name)
            };

            return View(model);
        }
        [HttpPost]
        //[ValidateModel]
        public async Task<IActionResult> Index(IndexViewModel modelFromBody)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            var account = await _accountService.AddAccountAsync(modelFromBody, User.Identity.Name);

            var modelToReturn = new AccountViewModel
            {
                Account = account
            };
            return RedirectToAction("Account", "AppAccount", new { id = account.Id });

        }
        [HttpGet]
        public async Task<IActionResult> Account(int id)
        {

            var account = await _accountService.GetAccountAsync(User.Identity.Name,id);

            if(account==null)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new AccountViewModel
            {
                Account = account
            };

            return View(model);
        }
    }
}
