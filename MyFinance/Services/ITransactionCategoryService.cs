using MyFinance.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Services
{
    public interface ITransactionCategoryService
    {
        Task<IEnumerable<TransactionCategory>> GetCategories(string userName);
        Task<TransactionCategory> GetCategory(string userName, int id);
        Task<TransactionCategory> AddCategoryAsync(Models.TransactionCategory.IndexViewModel model, string userName); 
    }
}
