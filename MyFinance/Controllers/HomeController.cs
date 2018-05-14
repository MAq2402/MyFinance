using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Models;
using MyFinance.Models.Home;
using MyFinance.Services;

namespace MyFinance.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ITransactionService _transactionService;

        public HomeController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new HomeViewModel
            {
                Transactions = await _transactionService.GetTransactionsAsync(User.Identity.Name)
            };
            return View(model);
        }  
        //[HttpPost]
        //public IActionResult Index(HomeViewModel model)
        //{

        //}
        //[HttpGet]
        //public async Task<IActionResult> Transaction(int id)
        //{
            
        //}
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
