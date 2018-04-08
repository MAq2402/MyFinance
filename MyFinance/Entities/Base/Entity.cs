using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Entities.Base
{
    public abstract class Entity
    {
        public int Id { get; set; }
    }
}
