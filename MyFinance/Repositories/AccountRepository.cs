using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyFinance.DbContexts;
using MyFinance.Entities;

namespace MyFinance.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private AppDbContext _context;

        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddAccountForUser(Account account,string userName)
        {
            _context.Users.FirstOrDefault(u => u.UserName == userName)?.Accounts.Add(account);
        }

        public IEnumerable<Account> GetAccountsForUser(string userName)
        {
            return _context.Accounts.Where(a => a.User.UserName == userName);
        }
    }
}
