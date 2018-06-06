using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Entities;
using MyFinance.Models.Enums;
using MyFinance.Models.Transaction;
using MyFinance.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyFinance.Controllers
{
    public class TransactionController : Controller
    {
        private ITransactionService _transactionService;
        private ITransactionCategoryService _transactionCategoryService;
        private IAppAccountService _accountService;
        private UserManager<User> _userManager;

        public TransactionController(ITransactionService transactionService, ITransactionCategoryService transactionCategoryService, IAppAccountService accountService, UserManager<User> userManager)
        {
            _transactionService = transactionService;
            _transactionCategoryService = transactionCategoryService;
            _accountService = accountService;
            _userManager = userManager;
        }
        // GET: /<controller>/
        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var userName = User.Identity.Name;

                model.Accounts = await _accountService.GetAccountsAsync(userName,false);
                model.Categories = await _transactionCategoryService.GetCategoriesAsync(userName);

                return View(model);
            }
            var outDateTime = new DateTime();

            if(!DateTime.TryParse(model.DateTime, out outDateTime))
            {
                ModelState.AddModelError("", "Wprowadź poprawną datę");
                var userName = User.Identity.Name;

                model.Accounts = await _accountService.GetAccountsAsync(userName, false);
                model.Categories = await _transactionCategoryService.GetCategoriesAsync(userName);

                return View(model);
            }

            if (!_accountService.UpdateAmount(model))
            {
                ModelState.AddModelError("", "Na koncie brakuje srodków, aby wykonać transakcje");
                var userName = User.Identity.Name;

                model.Accounts = await _accountService.GetAccountsAsync(userName, false);
                model.Categories = await _transactionCategoryService.GetCategoriesAsync(userName);
                return View(model);
            }

            var transaction = _transactionService.AddTransaction(model);

            if (transaction == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> Transaction(int id)
        {
            var transaction = await _transactionService.GetTransactionAsync(User.Identity.Name, id);

            if (transaction == null)
            {
                return RedirectToAction("Home", "Index");
            }

            var model = new TransactionViewModel
            {
                Transaction = transaction
            };

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var userName = User.Identity.Name;

            var categories = await _transactionCategoryService.GetCategoriesAsync(userName);

            var accounts = await _accountService.GetAccountsAsync(userName,false);

            var model = new CreateViewModel
            {
                Accounts = accounts,
                Categories = categories
            };

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userName = User.Identity.Name;

            var transaction = await _transactionService.GetTransactionAsync(userName, id);

            if(transaction==null)
            {
                return RedirectToAction("Index", "Home");
            }

            var categories = await _transactionCategoryService.GetCategoriesAsync(userName);

            var accounts = await _accountService.GetAccountsAsync(userName,false);

            

            TransactionType transactionType;
            if (transaction.IsExpanse)
            {
                transactionType = TransactionType.Expanse;
            }
            else
            {
                transactionType = TransactionType.Earning;
            }

            var model = new MyFinance.Models.Transaction.EditViewModel
            {
                Categories = categories,
                Accounts = accounts,
                Amount = transaction.Amount,
                DateTime = transaction.DateTime.ToString("dd.MM.yyyy"),
                Type = transactionType,
                AccountId = transaction.AccountId,
                CategoryId = transaction.CategoryId,
                Id=id
            };

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model,int id)
        {
            if (!ModelState.IsValid)
            {
                var userName = User.Identity.Name;

                model.Accounts = await _accountService.GetAccountsAsync(userName,false);
                model.Categories = await _transactionCategoryService.GetCategoriesAsync(userName);

                return View(model);
            }

            if (!_accountService.UpdateAmount(model))
            {
                ModelState.AddModelError("", "Na koncie brakuje srodków, aby wykonać transakcje");
                var userName = User.Identity.Name;

                model.Accounts = await _accountService.GetAccountsAsync(userName, false);
                model.Categories = await _transactionCategoryService.GetCategoriesAsync(userName);

                return View(model);
            }

            var transaction = _transactionService.UpdateTransaction(model,id);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var transaction = await _transactionService.GetTransactionAsync(User.Identity.Name, id);

            if(transaction==null)
            {
                return RedirectToAction("Index", "Home");
            }

            _accountService.DeleteTransaction(transaction);

            return RedirectToAction("Index", "Home");
        }
    }
}
