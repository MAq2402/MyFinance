using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyFinance.Entities;
using MyFinance.Models.TransactionCategory;
using MyFinance.Repositories;

namespace MyFinance.Services
{
    public class TransactionCategoryService : ITransactionCategoryService
    {
        private UserManager<User> _userManager;
        private IRepository<TransactionCategory> _categoryRepository;

        public TransactionCategoryService(UserManager<User> userManager,IRepository<TransactionCategory> categoryRepository)
        {
            _userManager = userManager;
            _categoryRepository = categoryRepository;
        }

        public async Task<TransactionCategory> AddCategoryAsync(IndexViewModel model, string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if(user ==null)
            {
                throw new Exception("Could not find user");
            }

            var category = new TransactionCategory
            {
                Name = model.Name,
                User = user
            };

            _categoryRepository.Add(category);


            if(!_categoryRepository.Save())
            {
                throw new Exception("Could not save category");
            }

            return category;
        }

        public async Task AddDefaultCategoryAsync(string userName)
        {
            var model = new MyFinance.Models.TransactionCategory.IndexViewModel
            {
                Name = "Domyślna"
            };

            await AddCategoryAsync(model, userName);
        }

        public async Task<IEnumerable<TransactionCategory>> GetCategories(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if(user==null)
            {
                throw new Exception("Could not find user");
            }

            return _categoryRepository.GetBy(c => c.UserId == user.Id)
                                      .Include(c=>c.Transactions);
        }

        public async Task<TransactionCategory> GetCategory(string userName, int id)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                throw new Exception("Could not find user");
            }

            return _categoryRepository.GetSingleBy(c => c.UserId == user.Id&&c.Id==id);
        }
    }
}
