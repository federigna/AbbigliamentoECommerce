using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AbbigliamentoECommerce.Models
{
    public class User : BaseClass
    {
        [Display(Name = "Nome")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Inserire il nome")]
        public string Name { get; set; }
        [Display(Name = "Cognome")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Inserire il cognome")]
        public string Surname { get; set; }
        [Display(Name = "Data Di Nascita")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Inserire la data di nascita")]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Recapito Telefonico")]
        [DataType(DataType.PhoneNumber)]
        public string TelefoneNumber { get; set; }
        [Display(Name = "Indirizzo di spedizione")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Inserire l'indirizzo di spedizione")]
        public string Address { get; set; }
        [Display(Name = "Città")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Inserire la città")]
        public string City { get; set; }
        [Display(Name = "Provincia")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Inserire la provincia")]
        public string District { get; set; }
        [Display(Name = "Indirizzo Email")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "L'Email non è valida.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Inserire l'email")]
        public string Email { get; set; }
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Inserire la password")]
        public string Password { get; set; }
        [Display(Name = "Conferma Password")]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Inserire la conferma password")]
        public string ConfirmePassword { get; set; }
    }
}