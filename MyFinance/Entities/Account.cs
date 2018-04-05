using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public decimal Amount { get; set; } = 0;
        public bool IsBlocked { get; set; } = false;
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();

    }
}
