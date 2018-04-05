using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models.AppAccount
{
    public class AccountForCreation
    {
        [Required]
        public string Name { get; set; }
    }
}
