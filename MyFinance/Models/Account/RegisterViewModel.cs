using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Podaj nazwę użytkownika"),Display(Name ="Nazwa Użytkownia")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Wprowadź hasło"), Display(Name ="Hasło"),DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Potwierź hasło"), Display(Name ="Potwierdź hasło"), DataType(DataType.Password),Compare(nameof(Password),ErrorMessage ="Hasła nie zgadzają się")]
        public string ConfirmPassword { get; set; }
    }
}
