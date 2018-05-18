using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyFinance.Entities;
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
        public async Task<IActionResult> Index(int monthNumber)
        {
            if(monthNumber==0)
            {
                monthNumber = DateTime.Now.Month;
                return RedirectToAction("Index", "Home", new { monthNumber = monthNumber });
            }

            var transactions = await _transactionService.GetTransactionsAsync(User.Identity.Name, monthNumber);
            var earnings = _transactionService.CalculateEarnings(transactions);
            var expanses = _transactionService.CalculateExpanses(transactions);

            var model = new HomeViewModel
            {
                Transactions = transactions,
                Earnings = earnings,
                Expanses = expanses
            };
            return View(model);
        }
       
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
