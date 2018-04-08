using System;
using System.Collections.Generic;
using MyFinance.Entities;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyFinance.Models.AppAccount
{
    public class IndexViewModel
    {
        public IEnumerable<MyFinance.Entities.Account> Accounts { get; set; }

        [Required]
        public string Name { get; set; }//FOR post
    }
}
