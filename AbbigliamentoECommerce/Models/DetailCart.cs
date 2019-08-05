using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AbbigliamentoECommerce.Models
{
    public class DetailCart:BaseClass
    {
        public int IdCart { get; set; }
        public Product Product { get; set; }
        [Display(Name = "Quantità")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Indicare la quantità")]
        public int Quantity { get; set; }
        [Display(Name = "Quantità")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Seleziona la quantità")]
        public Dictionary<int,int> Quantities { get; set; }

    }
}