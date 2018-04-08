using MyFinance.Entities;
using MyFinance.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Repositories
{
    public interface IAccountRepository:IRepository<Account>
    {
        void AddAccountForUser(Account account,string userName);
    }
}
