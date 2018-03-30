using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public TransactionCategory Category { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public decimal Amount { get; set; } 
        public bool IsExpanse { get; set; }
        public int PeriodId { get; set; }
        public Period Period { get; set; }

    }
}
