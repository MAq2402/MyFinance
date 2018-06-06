using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyFinance.Entities;
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

            var dateTime = new DateTime();

            dateTime = DateTime.Parse(model.DateTime);

            var transaction = new Transaction
            {
                AccountId = model.AccountId,
                Amount = model.Amount,
                CategoryId = model.CategoryId,
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

        public Transaction UpdateTransaction(EditViewModel model, int id)
        {
            var transaction = _transactionRepository.GetSingleBy(t => t.Id == id);

            if(transaction==null)
            {
                throw new Exception("Transaction does not exist");
            }

            transaction.AccountId = model.AccountId;
            transaction.Amount = model.Amount;
            transaction.CategoryId = model.CategoryId;
            transaction.DateTime = DateTime.Parse(model.DateTime);

            if(model.Type==TransactionType.Earning)
            {
                transaction.IsExpanse = false;
            }
            else
            {
                transaction.IsExpanse = true;
            }

            _transactionRepository.Save();

            return transaction;
        }

        public void DeleteTransaction(int id)
        {
            var transaction = _transactionRepository.GetSingleBy(t => t.Id == id);

            if(transaction==null)
            {
                throw new Exception("Could not find transaction");
            }

            _transactionRepository.Delete(transaction);

            if(!_transactionRepository.Save())
            {
                throw new Exception("Could not delete transaction");
            }
        }

        public IEnumerable<Transaction> GetTransactionsByAccount(int accountId)
        {
            return _transactionRepository.GetBy(t => t.AccountId == accountId)
                                         .Include(t=>t.Category)
                                         .OrderByDescending(t => t.DateTime);
        }

        public IEnumerable<Transaction> GetTransactionsByCategory(int categoryId)
        {
            return _transactionRepository.GetBy(t => t.CategoryId == categoryId)
                                         .Include(t => t.Account)
                                         .OrderByDescending(t => t.DateTime);
        }
    }
}
