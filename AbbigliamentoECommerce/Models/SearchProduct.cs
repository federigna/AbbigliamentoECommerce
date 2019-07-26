using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AbbigliamentoECommerce.Models
{
    public class SearchProduct
    {
        [Display(Name = "Nome Prodotto")]
        public string ProductName { get; set; }
        //quantità disponibile
        [Display(Name = "Colore")]
        public List<Category> Colors { get; set; }
        [Display(Name = "Colore")]
        public string Color { get; set; }
        [Display(Name = "Marca")]
        public string Brand { get; set; }
        [Display(Name = "Marca")]
        public List<Category> Brands { get; set; }
        [Display(Name = "Taglia")]
        public List<Category> Headmoneies { get; set; }
        [Display(Name = "Taglia")]
        public string Headmoney { get; set; }
        [Display(Name = "Modello")]
        public string Model { get; set; }
        [Display(Name = "Modello")]
        public List<Category> Models { get; set; }
        [Display(Name = "Sesso")]
        public List<Category> Categories { get; set; }
        [Display(Name = "Sesso")]
        public string Category { get; set; }
       
    }
}