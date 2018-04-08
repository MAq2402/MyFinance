using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyFinance.DbContexts;
using MyFinance.Entities;
using MyFinance.Repositories.Base;

namespace MyFinance.Repositories
{
    public class AccountRepository :Repository<Account> ,IAccountRepository//IAccountRepositroy and Repository<T> both implement IRepository
    {
        public AccountRepository(AppDbContext context):base(context)
        {

        }

        public void AddAccountForUser(Account account,string userName)
        {
            _context.Users.FirstOrDefault(u => u.UserName == userName)?.Accounts.Add(account);
        }

    }
}
