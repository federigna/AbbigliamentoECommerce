﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AbbigliamentoECommerce.Models
{
    public class Product : BaseClass
    {
        [Display(Name = "Nome Prodotto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Inserire il nomedel prodotto")]
        public string ProductName { get; set; }
        //quantità disponibile
        [Display(Name = "Quantità Disponibile")]
        [RegularExpression(@"[1-9]$", ErrorMessage = "Quantità non valido.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Inserire la quantità")]
        public int Quantity { get; set; }
        [Display(Name = "Colore")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Seleziona il colore")]
        public List<Category> Colors { get; set; }
        [Display(Name = "Colore")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Seleziona il colore")]
        public string Color { get; set; }
        [Display(Name = "Marca")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Seleziona la marca")]
        public string Brand { get; set; }
        [Display(Name = "Marca")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Seleziona la marca")]
        public List<Category> Brands { get; set; }
        [Display(Name = "Taglia")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Seleziona la taglia")]
        public List<Category> Headmoneies { get; set; }
        [Display(Name = "Taglia")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Seleziona la taglia")]
        public string Headmoney { get; set; }
        [Display(Name = "Modello")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Seleziona il modello")]
        public string Model { get; set; }
        [Display(Name = "Modello")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Seleziona il modello")]
        public List<Category> Models { get; set; }
        [Display(Name = "Sesso")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Seleziona la categoria")]
        public List<Category> Categories { get; set; }
        [Display(Name = "Sesso")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Seleziona la categoria")]
        public string Category { get; set; }
        [Display(Name = "Prezzo")]
        [DataType(DataType.Currency)]
        [RegularExpression(@"[0-9]+,[0-9]{2}$", ErrorMessage = "Prezzo non valido.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Inserire il prezzo")]
        public double Price { get; set; }
        [Display(Name = "Immagine")]
        [DataType(DataType.Upload)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Inserire un immagine del prodotto")]
        public string Image { get; set; }
        [Display(Name = "Quantità ")]
        public int QuantityBuy { get; set; }
        public Dictionary<int, int> ListQuantity
        {
            get
            {
                for (int i = 0; i < this.Quantity; i++)
                {
                    this.ListQuantity.Add(i, i);
                }
                return this.ListQuantity;
            }
        }
    }
}