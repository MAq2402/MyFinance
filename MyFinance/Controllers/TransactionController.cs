using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Entities;
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
        public IActionResult Create(CreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
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

            var categories = await _transactionCategoryService.GetCategories(userName);

            var accounts = await _accountService.GetAccountsAsync(userName);

            ViewBag.Categories = categories;
            ViewBag.Accounts = accounts;

            return View();
        }
    }
}
