using MyFinance.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models.AppAccount
{
    public class DetailViewModel
    {
        public MyFinance.Entities.Account Account { get; set; }
        public IEnumerable<MyFinance.Entities.Transaction> Transactions { get; set; }
        
        [Required]
        public string Name { get; set; }//For update
    }
}
