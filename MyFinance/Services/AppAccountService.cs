using Microsoft.AspNetCore.Identity;
using MyFinance.Entities;
using MyFinance.Models.AppAccount;
using MyFinance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Services
{
    public class AppAccountService:IAppAccountService
    {
        private IRepository<Account> _accountRepository;
        private UserManager<User> _userManager;

        public AppAccountService(IRepository<Account> accountRepository,UserManager<User> userManager)
        {
            _accountRepository = accountRepository;
            _userManager = userManager;
        }

        public async Task<Account> AddAccountAsync(IndexViewModel model,string userName)
        {
            var user =  await _userManager.FindByNameAsync(userName);

            if(user==null)
            {
                throw new Exception("Could not find user");
            }

            var account = new Account
            {
                User = user,
                Name = model.Name
            };

            _accountRepository.Add(account);

            if(!_accountRepository.Save())
            {
                throw new Exception("Could not save new account");
            }

            return account;
        }

        public async Task<Account> GetAccountAsync(string userName,int id)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if(user==null)
            {
                throw new Exception("Could not find user");
            }

            return _accountRepository.GetSingleBy(a => a.Id == id&&a.UserId==user.Id);
        }

        public async Task<IEnumerable<Account>> GetAccountsAsync(string userName)
        {

            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                throw new Exception("Could not find user");
            }

            return _accountRepository.GetBy(a=>a.UserId==user.Id);
        }
    }
}
