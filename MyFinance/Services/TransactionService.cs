using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyFinance.Entities;
using MyFinance.Extensions;
using MyFinance.Models.Enums;
using MyFinance.Models.Transaction;
using MyFinance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MyFinance.Services
{
    public class TransactionService:ITransactionService
    {
        private IRepository<Transaction> _transactionRepository;
        private UserManager<User> _userManager;
        private IRepository<Account> _accountRepository;

        public TransactionService(IRepository<Transaction> transactionRepository,IRepository<Account> accountRepository,UserManager<User> userManager)
        {
            _transactionRepository = transactionRepository;
            _userManager = userManager;
            _accountRepository = accountRepository;
        }

        public Transaction AddTransaction(CreateViewModel model)
        {
            bool isExpanse;
            if(model.Type==TransactionType.Expanse)
            {
                isExpanse = true;
            }
            else
            {
                isExpanse = false;
            }

            var dateTime = new DateTime().GenerateFromString(model.DateTime);

            var transaction = new Transaction
            {
                AccountId = model.AccountId,
                Amount = model.Amount,
                CategoryId = model.CategoryId,
                Description = model.Description,
                IsExpanse = isExpanse,
                DateTime = dateTime
            };
            _transactionRepository.Add(transaction);

            if(!_transactionRepository.Save())
            {
                throw new Exception("Could not save");
            }

            return transaction;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsAsync(string userName,int month)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                throw new Exception("Could not find user");
            }

            return _transactionRepository.GetBy(t => t.Account.UserId == user.Id&&t.DateTime.Month==month)
                                         .Include(t => t.Category)
                                         .Include(t => t.Account)
                                         .OrderByDescending(t => t.DateTime);
        }

        public async Task<Transaction> GetTransactionAsync(string userName, int id)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                throw new Exception("Could not find user");
            }

            return _transactionRepository.GetSingleBy(t => t.Account.UserId == user.Id&&t.Id==id);
        }

        public decimal CalculateEarnings(IEnumerable<Transaction> transactions)
        {
            var earnings = 0.0m;
            foreach(var transaction in transactions)
            {
                if(!transaction.IsExpanse)
                {
                    earnings += transaction.Amount;
                }
            }
            return earnings;
        }

        public decimal CalculateExpanses(IEnumerable<Transaction> transactions)
        {
            var expanses = 0.0m;
            foreach (var transaction in transactions)
            {
                if (transaction.IsExpanse)
                {
                    expanses += transaction.Amount;
                }
            }
            return expanses;
        }
    }
}
