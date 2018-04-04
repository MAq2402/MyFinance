using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Podaj nazwę użytkownika"),Display(Name ="Nazwa użytkownika")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Podaj hasło"), Display(Name = "Hasło"),DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
