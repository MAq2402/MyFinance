using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace MyFinance.Models.Home
{
    public class HomeViewModel
    {
        public IEnumerable<Entities.Transaction> Transactions { get; set; }
    }
}
