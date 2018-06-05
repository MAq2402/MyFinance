using MyFinance.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models.TransactionCategory
{
    public class DetailViewModel
    {
        public MyFinance.Entities.TransactionCategory Category { get; set; }
        public IEnumerable<MyFinance.Entities.Transaction> Transactions { get; set; }

        public decimal Earnings { get; set; }
        public decimal Expanses { get; set; }
    }
}
