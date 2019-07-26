using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AbbigliamentoECommerce.Models
{
    public class LoggedUser : BaseClass
    {
       
        [Display(Name = "Indirizzo Email")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "L'Email non è valida.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Inserire l'email")]
        public string Email { get; set; }
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Inserire la password")]
        public string Password { get; set; }
        
        public User wDetailUser { get; set; }
    }
}