using MyFinance.Entities;
using MyFinance.Models.AppAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Services
{
    public interface IAppAccountService
    {
        Task<IEnumerable<Account>> GetAccountsAsync(string userName,bool includeTransactions);
        Task<Account> GetAccountAsync(string userName,int id);
        Task<Account> AddAccountAsync(IndexViewModel model,string userName);
        Task AddDefaultAccountAsync(string userName);
        void DeleteAccount(int id);
        void UpdateAccount(int id, DetailViewModel model);
    }
}
