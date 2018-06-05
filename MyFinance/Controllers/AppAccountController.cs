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
using MyFinance.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyFinance.Controllers
{
    [Authorize]
    public class AppAccountController : Controller
    {
        private IAppAccountService _accountService;
        private ITransactionService _transactionService;

        public AppAccountController(IAppAccountService accountService,ITransactionService transactionService)
        {
            _accountService = accountService;
            _transactionService = transactionService;
        }
        
        public async Task<IActionResult> Index()
        {
            var accounts = await _accountService.GetAccountsAsync(User.Identity.Name,true);
     
            var model = new MyFinance.Models.AppAccount.IndexViewModel
            {
                Accounts = accounts
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

            var modelToReturn = new DetailViewModel
            {
                Account = account
            };
            return RedirectToAction("Detail", "AppAccount", new { id = account.Id });

        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {

            var account = await _accountService.GetAccountAsync(User.Identity.Name,id);

            if(account==null)
            {
                return RedirectToAction("Index", "Home");
            }
            var transactions = _transactionService.GetTransactionsByAccount(account.Id);

            var model = new DetailViewModel
            {
                Account = account,
                Transactions = transactions,
                 Earnings = _transactionService.CalculateEarnings(transactions),
                 Expanses = _transactionService.CalculateExpanses(transactions)              
            };

            return View(model);
        }
        public IActionResult Delete(int id)
        {
            _accountService.DeleteAccount(id);

            return RedirectToAction("Index", "AppAccount");
        }
        [HttpPost]
        public async Task<IActionResult> Detail(int id,MyFinance.Models.AppAccount.DetailViewModel model)
        {
            model.Account = await _accountService.GetAccountAsync(User.Identity.Name, id);
            model.Transactions = _transactionService.GetTransactionsByCategory(id);

            if (!ModelState.IsValid)
            {               
                return View(model);
            }

            _accountService.UpdateAccount(id, model);
            
            return View(model);
        }
    }
}
