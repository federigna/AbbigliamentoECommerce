using System;
using System.Collections.Generic;
using System.Text;

namespace AbbigliamentoECommerceEntity
{
    public class Cart
    {
       public int NumOrdine { get; set; }
        public List<CartDetail> listProduct{ get; set; }
    }
}
