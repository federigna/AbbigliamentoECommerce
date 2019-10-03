using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AbbigliamentoECommerce.Models
{
    public class FilterStatistics2 : BaseClass
    {

        [Display(Name = "Data Inizio")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Indicare la data di inizio")]
        public DateTime Datastart { get; set; }
        [Display(Name = "Data Fine")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Indicare la data di inizio")]
        public DateTime DataEnd { get; set; }
        public string NameStatistic { get; set; }
        public FileContentResult Chart { get; set; }

    }
}