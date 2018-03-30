using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Entities
{
    public class TransactionCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
