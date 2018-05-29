using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyFinance.Entities;
using MyFinance.Models.AppAccount;
using MyFinance.Models.Enums;
using MyFinance.Models.Transaction;
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
        private IRepository<Transaction> _transactionRepository;

        public AppAccountService(IRepository<Account> accountRepository,
                                UserManager<User> userManager,
                                IRepository<Transaction> transactionRepository)
        {
            _accountRepository = accountRepository;
            _userManager = userManager;
            _transactionRepository = transactionRepository;
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

        public async Task AddDefaultAccountAsync(string userName)
        {
            var model = new MyFinance.Models.AppAccount.IndexViewModel
            {
                Name = "Domyślne"
            };
            await AddAccountAsync(model, userName);
        }

        public void DeleteAccount(int id)
        {
            var account = _accountRepository.GetSingleBy(a => a.Id == id);

            if(account==null)
            {
                throw new Exception("Could not find account");
            }

            _accountRepository.Delete(account);

            if(!_accountRepository.Save())
            {
                throw new Exception("Could not remove account");
            }
        }

        public void DeleteTransaction(Transaction transaction)
        {
            var account = _accountRepository.GetSingleBy(a => a.Id == transaction.AccountId);

            if(account==null)
            {
                throw new Exception("Account does not exist");
            }
            if(transaction.IsExpanse)
            {
                account.Amount += transaction.Amount;
            }
            else
            {
                account.Amount -= transaction.Amount;
            }
            

            account.Transactions.Remove(transaction);

            if(!_accountRepository.Save())
            {
                throw new Exception("Could not delete transaction in account");
            }




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

        public async Task<IEnumerable<Account>> GetAccountsAsync(string userName,bool includeTransactions)
        {

            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                throw new Exception("Could not find user");
            }

            if(includeTransactions)
            {
                return _accountRepository.GetBy(a => a.UserId == user.Id).Include(a => a.Transactions);
            }
            else
            {
                return _accountRepository.GetBy(a => a.UserId == user.Id);
            }
        }

        public void UpdateAccount(int id, DetailViewModel model)
        {
            var account = _accountRepository.GetSingleBy(a => a.Id == id);

            if (account == null)
            {
                throw new Exception("Could not find account");
            }

            account.Name = model.Name;

            _accountRepository.Save();
        }

        public bool UpdateAmount(TransactionInputModel model)
        {
            var account = _accountRepository.GetSingleBy(a => a.Id == model.AccountId);

            if (account == null)
            {
                throw new Exception("Account does not exist");
            }

            if (model.Type==TransactionType.Expanse)
            {
                if (model.Amount > account.Amount)
                {
                    return false;
                }
                account.Amount -= model.Amount;
            }
            else
            {
                account.Amount += model.Amount;
            }

            return true;
        }
    }
}
