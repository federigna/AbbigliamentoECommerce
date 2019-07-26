using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbbigliamentoECommerce.Models
{
    public class Cart:BaseClass
    {
        public User UserOwner { get; set; }
        public double TotalPrice { get; set; }
        public double Vat { get; set; }
        public double ShippingCost { get; set; }
        public List<DetailCart> DetailsCart { get; set; }
    }
}