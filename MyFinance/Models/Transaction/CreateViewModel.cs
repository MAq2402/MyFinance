using MyFinance.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models.Transaction
{
    public class CreateViewModel
    {
        [Display(Name ="Opis")]
        public string Description { get; set; }
        [Required,Display(Name = "Suma")]
        public decimal Amount { get; set; }
        [Required,Display(Name = "Typ")]
        public TransactionType Type { get; set; }
        [Required,Display(Name = "Kategoria")]
        public int CategoryId { get; set; }
        [Required,Display(Name = "Konto")]
        public int AccountId { get; set; }
        [Required,Display(Name ="Data")]
        public string DateTime { get; set; }

        public IEnumerable<MyFinance.Entities.Account> Accounts { get; set; }
        public IEnumerable<MyFinance.Entities.TransactionCategory> Categories { get; set; }
    }
}
