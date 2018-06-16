using MyFinance.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Entities
{
    public class Account:Entity
    {
        public string Name { get; set; }
        public string UserId { get; set; }
        public  User User { get; set; }
        public decimal Amount { get; set; } = 0;
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();

    }
}
