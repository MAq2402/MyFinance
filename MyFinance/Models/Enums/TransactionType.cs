using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models.Enums
{
    public enum TransactionType
    {

        [Display(Name ="Wydatek")]
        Expanse,
        [Display(Name ="Przychód")]
        Earning
    }
}
