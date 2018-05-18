using MyFinance.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Entities
{
    public class Transaction:Entity
    {
        public int CategoryId { get; set; }
        public virtual TransactionCategory Category { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Amount { get; set; } 
        public bool IsExpanse { get; set; }

    }
}
