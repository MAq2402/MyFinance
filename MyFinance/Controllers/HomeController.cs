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
        public async Task<IActionResult> Index(int year, int month)
        {
            var date = new DateTime();
            if(year==0||month==0)
            {
                date = DateTime.Today;
            }
            else
            {
                date = new DateTime(year, month, 1);
            }
  
            var transactions = await _transactionService.GetTransactionsAsync(User.Identity.Name, date);
            var earnings = _transactionService.CalculateEarnings(transactions);
            var expanses = _transactionService.CalculateExpanses(transactions);


            var model = new HomeViewModel
            {
                Transactions = transactions,
                Earnings = earnings,
                Expanses = expanses,
                Date = date.ToString("MM-yyyy")

            };
            return View(model);
        }

        [HttpPost]

        public IActionResult Index(HomeViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }
            var date = DateTime.Parse(model.Date);
            return RedirectToAction("Index", "Home", new { year = date.Year, month = date.Month });
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
