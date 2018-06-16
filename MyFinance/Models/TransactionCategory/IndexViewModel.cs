using System;
using System.Collections.Generic;
using MyFinance.Entities;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyFinance.Models.TransactionCategory
{
    public class IndexViewModel
    {
        public IEnumerable<MyFinance.Entities.TransactionCategory> Categories { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
