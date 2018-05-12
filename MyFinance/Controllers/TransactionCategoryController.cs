using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Filters;
using MyFinance.Models.TransactionCategory;
using MyFinance.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyFinance.Controllers
{
    [Authorize]
    [ValidateCurrentUser]
    public class TransactionCategoryController : Controller
    {
        private ITransactionCategoryService _categoryService;

        public TransactionCategoryController(ITransactionCategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]       
        public async Task<IActionResult> Index(string userName)
        {
            var categories = await _categoryService.GetCategories(userName);

            var model = new MyFinance.Models.TransactionCategory.IndexViewModel
            {
                Categories = categories
            };

            return View(model);
        }
        
        public async Task<IActionResult> Category(string userName,int id)
        {
            var category = await _categoryService.GetCategory(userName,id);

            if (category == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new MyFinance.Models.TransactionCategory.TransactionCategorytViewModel
            {

                Category = category
            };

            return View(model);
        }

        [HttpPost]
        //[ValidateModel]
        public async Task<IActionResult> Index(string userName, Models.TransactionCategory.IndexViewModel modelFromBody)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            var category = await _categoryService.AddCategoryAsync(modelFromBody, userName);

            var modelToReturn = new TransactionCategorytViewModel
            {
                Category = category
            };
            return RedirectToAction("Category", "TransactionCategory", new { userName = userName, id = category.Id });

        }
    }
}
