using System;
using System.Collections.Generic;
using MyFinance.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models.AppAccount
{
    public class IndexViewModel
    {
        public IEnumerable<MyFinance.Entities.Account> Accounts { get; set; }
    }
}
