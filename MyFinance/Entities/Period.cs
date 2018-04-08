using MyFinance.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Entities
{
    public class Period:Entity
    { 
        public DateTime Start { get; set; } = DateTime.Now;
        public DateTime End { get; set; }
        public bool IsPresent { get; set; } = true;
        public List<Transaction> Transactions { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public decimal Expanses { get; set; }
        public decimal  Earnings { get; set; }

    }
}
