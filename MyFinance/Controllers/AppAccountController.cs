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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyFinance.Controllers
{
    [Authorize]
    public class AppAccountController : Controller
    {
        private IAccountRepository _accountRepository;
        private IAppRepository _appRepository;

        public AppAccountController(IAccountRepository accountRepository,IAppRepository appRepository)
        {
            _accountRepository = accountRepository;
            _appRepository = appRepository;
        }
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
        [HttpPost]
        public IActionResult Index(string userName, AccountForCreation accountFromBody)
        {
            if (User.Identity.Name != userName)
            {
                return RedirectToAction("Index", "Home");
            }

            if(!ModelState.IsValid)
            {
                return View();
            }

            var account = Mapper.Map<Account>(accountFromBody);

            _accountRepository.AddAccountForUser(account, userName);

            if(!_appRepository.Commit())
            {
                return RedirectToAction("Error", "Home");
            }

            //stworz nowy taki ViewModel dla takiego widoku co bedzie wyswietlal konkretne konto.

            return View();

        }
    }
}
