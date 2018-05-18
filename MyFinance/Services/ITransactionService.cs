using MyFinance.Entities;
using MyFinance.Models.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetTransactionsAsync(string userName,int month);
        Task<Transaction> GetTransactionAsync(string userName,int id);
        Transaction AddTransaction(CreateViewModel model);
        decimal CalculateEarnings(IEnumerable<Transaction> transactions);
        decimal CalculateExpanses(IEnumerable<Transaction> transactions);

        
    }
}
