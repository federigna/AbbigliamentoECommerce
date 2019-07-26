
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
    }
}