using AbbigliamentoECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbbigliamentoECommerce.Converter
{
    public static class CategoryEntityTOCategoryModel
    {
        public static List<Category> ConvertoListCategoryEntityTOListCategoryModel(List<AbbigliamentoECommerceEntity.Category> pListCat)
        {
            List<Category> wNewListCat = new List<Category>();
           
            foreach(AbbigliamentoECommerceEntity.Category wSingleCat in pListCat)
            {
                Category wCat = new Category();
                wCat.Id = wSingleCat.Description;
                wCat.Description = wSingleCat.Description;
                wNewListCat.Add(wCat);
            }
            return wNewListCat;
        }
    }
}