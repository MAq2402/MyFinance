using MyFinance.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models.Transaction
{
    public class TransactionInputModel
    {
        [Required(ErrorMessage ="Uzupełnij sumę"), Display(Name = "Suma")]
        public decimal Amount { get; set; }
        [Required, Display(Name = "Typ")]
        public TransactionType Type { get; set; }
        [Required, Display(Name = "Kategoria")]
        public int CategoryId { get; set; }
        [Required, Display(Name = "Konto")]
        public int AccountId { get; set; }
        [Required(ErrorMessage = "Uzupełnij datę"), Display(Name = "Data")]
        public string DateTime { get; set; }
    }
}
