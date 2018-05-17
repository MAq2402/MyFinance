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
            var model = new HomeViewModel
            {
                Transactions = await _transactionService.GetTransactionsAsync(User.Identity.Name)
            };
            return View(model);
        }
       
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //private void SetActiveMonth(int monthNumber)
        //{
        //    ViewBag as Dictionary<string,Object>();
        //    for(int i=1;i<13;i++)
        //    {
        //        ViewBag.Active+i = 
        //    }
        //}
    }
}
