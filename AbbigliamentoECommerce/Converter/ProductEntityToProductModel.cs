
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbbigliamentoECommerce.Converter
{
    public class ProductEntityToProductModel
    {
        public static AbbigliamentoECommerceEntity.Product ConvertoProdyctEntityTOProductModel(AbbigliamentoECommerce.Models.Product pProduct)
        {
            AbbigliamentoECommerceEntity.Product wProduct = new AbbigliamentoECommerceEntity.Product();
            wProduct.UId = pProduct.Id;
            wProduct.categoria = pProduct.Category;
            wProduct.colore = pProduct.Color;
            wProduct.marca = pProduct.Brand;
            wProduct.nome = pProduct.ProductName;
            wProduct.prezzo = pProduct.Price;
            wProduct.Quantity = pProduct.Quantity;
            wProduct.taglia = pProduct.Headmoney;
            wProduct.UrlDownloadWeb = pProduct.Image;
            return wProduct;
        }

        public static AbbigliamentoECommerceEntity.Product ConvertoProdyctEntityTOSearchProductModel(AbbigliamentoECommerce.Models.SearchProduct pProduct)
        {
            AbbigliamentoECommerceEntity.Product wProduct = new AbbigliamentoECommerceEntity.Product();
            wProduct.categoria = pProduct.Category;
            wProduct.colore = pProduct.Color;
            wProduct.marca = pProduct.Brand;
            wProduct.nome = pProduct.ProductName;
            wProduct.taglia = pProduct.Headmoney;
            return wProduct;
        }

        public static List<AbbigliamentoECommerce.Models.Product> ConvertoListProdyctEntityTOListProductModel(List<AbbigliamentoECommerceEntity.Product> pListpProduct)
        {
            List<AbbigliamentoECommerce.Models.Product> wListProduct = new List<AbbigliamentoECommerce.Models.Product>();
            foreach (AbbigliamentoECommerceEntity.Product pProduct in pListpProduct)
            {
                AbbigliamentoECommerce.Models.Product wProduct = new AbbigliamentoECommerce.Models.Product();
                wProduct.Id = pProduct.UId;
                wProduct.Category = pProduct.categoria;
                wProduct.Color = pProduct.colore;
                wProduct.Brand = pProduct.marca;
                wProduct.ProductName = pProduct.nome;
                wProduct.Price = pProduct.prezzo;
                wProduct.Quantity = pProduct.Quantity;
                wProduct.Headmoney = pProduct.taglia;
                wProduct.Image = pProduct.UrlDownloadWeb;
                wListProduct.Add(wProduct);
            }
            

            return wListProduct;
        }

    }
}