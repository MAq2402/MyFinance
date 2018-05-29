using MyFinance.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models.Transaction
{
    public class CreateViewModel:TransactionInputModel
    {
        public IEnumerable<MyFinance.Entities.Account> Accounts { get; set; }
        public IEnumerable<MyFinance.Entities.TransactionCategory> Categories { get; set; }
    }
}
