using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Repositories;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyFinance.Controllers
{
    public class AppAccountController : Controller
    {
        private IAccountRepository _accountRepository;

        public AppAccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        // GET: /<controller>/
        public IActionResult Index(string userName)
        {
            if(User.Identity.Name!=userName)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new MyFinance.Models.AppAccount.IndexViewModel();

            model.Accounts = _accountRepository.GetAccountsForUser(userName);

            return View(model);
        }
    }
}
