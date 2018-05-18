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
        public decimal Earnings { get; set; }
        public decimal Expanses { get; set; }
    }
}
