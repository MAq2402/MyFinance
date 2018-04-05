using MyFinance.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Repositories
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAccountsForUser(string userName);

        void AddAccountForUser(Account account,string userName);
    }
}
