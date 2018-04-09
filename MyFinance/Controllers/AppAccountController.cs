using System;
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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyFinance.Controllers
{
    [Authorize]
    [ValidateCurrentUser]
    public class AppAccountController : Controller
    {
        private IAccountRepository _accountRepository;

        public AppAccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        
        public IActionResult Index(string userName)
        {

            var model = new MyFinance.Models.AppAccount.IndexViewModel
            {
                Accounts = _accountRepository.GetBy(a => a.User.UserName == userName)
            };

            return View(model);
        }
        [HttpPost]
        //[ValidateModel]
        public IActionResult Index(string userName, IndexViewModel modelFromBody)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            var account = new Account
            {
                Name = modelFromBody.Name
            };

            _accountRepository.AddAccountForUser(account, userName);

            if(!_accountRepository.Save())
            {
                return RedirectToAction("Error", "Home");
            }

            var model = new TransactionCategorytViewModel
            {
                Account = account
            };

            return RedirectToAction("Account", "AppAccount", new { userName = userName, id = account.Id });

        }
        [HttpGet]
        public IActionResult Account(string userName,int id)
        {

            var account = _accountRepository.GetById(id);

            if(account==null)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new TransactionCategorytViewModel
            {
                Account = account
            };

            return View(model);
        }
    }
}
