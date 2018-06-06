using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Models.TransactionCategory;
using MyFinance.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyFinance.Controllers
{
    [Authorize]
    public class TransactionCategoryController : Controller
    {
        private ITransactionCategoryService _categoryService;
        private ITransactionService _transactionService;
        private IAppAccountService _appAccountService;

        public TransactionCategoryController(ITransactionCategoryService categoryService, 
                                             ITransactionService transactionService,
                                             IAppAccountService appAccountService)
        {
            _categoryService = categoryService;
            _transactionService = transactionService;
            _appAccountService = appAccountService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetCategoriesAsync(User.Identity.Name);

            var model = new MyFinance.Models.TransactionCategory.IndexViewModel
            {
                Categories = categories
            };

            return View(model);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var category = await _categoryService.GetCategoryAsync(User.Identity.Name, id);

            if (category == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var transactions = _transactionService.GetTransactionsByCategory(category.Id);

            var model = new MyFinance.Models.TransactionCategory.DetailViewModel
            {
                Category = category,
                Transactions = transactions,
                Earnings = _transactionService.CalculateEarnings(transactions),
                Expanses = _transactionService.CalculateExpanses(transactions)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(Models.TransactionCategory.IndexViewModel modelFromBody)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            var category = await _categoryService.AddCategoryAsync(modelFromBody, User.Identity.Name);

            var modelToReturn = new DetailViewModel
            {
                Category = category
            };
            return RedirectToAction("Detail", "TransactionCategory", new { id = category.Id });

        }
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetCategoryAsync(User.Identity.Name, id);

            if(category==null)
            {
                return RedirectToAction("Index", "TransactionCategory");
            }
            var transactions = _transactionService.GetTransactionsByCategory(category.Id);

            _appAccountService.UpdateAmountsOfAccounts(transactions);

            _categoryService.DeleteCategory(category);

            return RedirectToAction("Index", "TransactionCategory");
        }
    }
}
