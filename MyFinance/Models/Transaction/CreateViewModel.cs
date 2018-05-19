﻿using MyFinance.Models.Enums;
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
        [Display(Name = "Suma")]
        public decimal Amount { get; set; }
        [Display(Name = "Typ")]
        public TransactionType Type { get; set; }
        [Display(Name = "Kategoria")]
        public int CategoryId { get; set; }
        [Display(Name = "Konto")]
        public int AccountId { get; set; }
        [Display(Name ="Data")]
        public string DateTime { get; set; }
    }
}