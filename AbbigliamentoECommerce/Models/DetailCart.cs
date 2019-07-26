using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbbigliamentoECommerce.Models
{
    public class DetailCart:BaseClass
    {
        public int IdCart { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}