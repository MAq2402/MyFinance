using Microsoft.AspNetCore.Identity;
using MyFinance.Entities;
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

        public async Task<IEnumerable<Transaction>> GetTransactionsAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                throw new Exception("Could not find user");
            }

            return _transactionRepository.GetBy(t => t.Account.UserId == user.Id);
        }
    }
}
