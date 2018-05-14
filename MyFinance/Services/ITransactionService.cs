using MyFinance.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetTransactionsAsync(string userName);
        
    }
}
