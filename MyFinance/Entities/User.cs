using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Entities
{
    public class User:IdentityUser
    {
        public List<Account> Accounts { get; set; } = new List<Account>();
        public List<TransactionCategory> Categories { get; set; } = new List<TransactionCategory>();
        public List<Period> Periods { get; set; }

    }
}
