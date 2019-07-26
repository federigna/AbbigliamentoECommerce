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
            int i = 1;
            foreach(AbbigliamentoECommerceEntity.Category wSingleCat in pListCat)
            {
                Category wCat = new Category();
                wCat.Id = i;
                wCat.Description = wSingleCat.Description;
                wNewListCat.Add(wCat);
            }
            return wNewListCat;
        }
    }
}