﻿using MyFinance.Entities;
using MyFinance.Models.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetTransactionsAsync(string userName,DateTime date);
        Task<Transaction> GetTransactionAsync(string userName,int id);
        Transaction AddTransaction(CreateViewModel model);
        decimal CalculateEarnings(IEnumerable<Transaction> transactions);
        decimal CalculateExpanses(IEnumerable<Transaction> transactions);
        Transaction UpdateTransaction(EditViewModel model,int id);
        void DeleteTransaction(int id);
        IEnumerable<Transaction> GetTransactionsByCategory(int categoryId);

        IEnumerable<Transaction> GetTransactionsByAccount(int accountId);
    }
}
