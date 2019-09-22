using AbbigliamentoECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbbigliamentoECommerce.Converter
{
    public static class ConvertEntityUserTOUserModel
    {
        public static User ConvertoUserEntityTOUserModel(AbbigliamentoECommerceEntity.User pUser)
        {
            User wUser = new User();
            wUser.Address = pUser.Address;
            wUser.City = pUser.City;
            wUser.DateOfBirth = pUser.DateOfBirth;
            wUser.District = pUser.District;
            wUser.Email = pUser.email;
            wUser.Id = pUser.Id;
            wUser.Name = pUser.nome;
            wUser.Surname = pUser.cognome;
            wUser.Ruolo = pUser.Ruolo;
            return wUser;
        }

        public static Cart ConvertoCartEntityTOCartModel(AbbigliamentoECommerceEntity.Cart pCart)
        {
            Cart wCart = new Cart();
            wCart.ShippingCost = 5.00;
            wCart.TotalPrice = pCart.listProduct.Sum(p => p.quantita * p.singleProduct.prezzo);
            wCart.Vat = 1.06;
            wCart.DetailsCart = new List<DetailCart>();
            foreach (AbbigliamentoECommerceEntity.CartDetail wCartDetail in pCart.listProduct)
            {
                DetailCart wDetailCart = new DetailCart();
                wDetailCart.Quantity = wCartDetail.quantita;
                wDetailCart.Quantities = new Dictionary<int, int>();
                for (int i = 0; i < wCartDetail.singleProduct.Quantity; i++)
                {
                    wDetailCart.Quantities.Add(i, i + 1);
                }
                wDetailCart.Product = ProductEntityToProductModel.ConvertoProdyctEntityTOProductModel(wCartDetail.singleProduct);
                wCart.DetailsCart.Add(wDetailCart);
            }
            return wCart;
        }

        public static AbbigliamentoECommerceEntity.Cart ConvertCartModelTOCartEntity(Cart pCart)
        {
            AbbigliamentoECommerceEntity.Cart wCart = new AbbigliamentoECommerceEntity.Cart();
            
            wCart.listProduct = new List<AbbigliamentoECommerceEntity.CartDetail>();
            foreach (DetailCart wCartDetail in pCart.DetailsCart)
            {
                AbbigliamentoECommerceEntity.CartDetail wDetailCart = new AbbigliamentoECommerceEntity.CartDetail();
                wDetailCart.quantita = wCartDetail.Quantity;
                wDetailCart.singleProduct = ProductEntityToProductModel.ConvertoProdyctEntityTOProductModel(wCartDetail.Product);
                wCart.listProduct.Add(wDetailCart);
            }
            return wCart;
        }

        public static AbbigliamentoECommerceEntity.User ConvertoUserEntityTOUserModel(User collection)
        {
            AbbigliamentoECommerceEntity.User wUser = new AbbigliamentoECommerceEntity.User();
            wUser.email = collection.Email;
            wUser.Password = collection.Password;
            wUser.nome = collection.Name;
            wUser.Address = collection.Address;
            wUser.City = collection.City;
            wUser.cognome = collection.Surname;
            wUser.DateOfBirth = collection.DateOfBirth;
            wUser.District = collection.District;
            wUser.TelefoneNumber = collection.TelefoneNumber;
            wUser.Ruolo = collection.Ruolo;
            return wUser;
        }
    }
}