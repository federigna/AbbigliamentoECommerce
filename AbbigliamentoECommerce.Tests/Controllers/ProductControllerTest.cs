using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AbbigliamentoECommerce.Controllers;
using AbbigliamentoECommerce.Converter;
using AbbigliamentoECommerce.Models;
using AbbigliamentoECommerceBL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AbbigliamentoECommerce.Tests.Controllers
{
    [TestClass]
    public class ProductControllerTest
    {
        [TestMethod]
        //Non impostare filtri e premere CERCA
        public async Task Search()
        {
            // Arrange
            ProductController controller = new ProductController();
            UserBL userBL = new UserBL();
            userBL.SetGoogleCedential();
            ProductBL wDB = new ProductBL();

            List<Product> wListProduct = ProductEntityToProductModel.ConvertoListProdyctEntityTOListProductModel(await wDB.GetListProducts(ProductEntityToProductModel.ConvertoProdyctEntityTOSearchProductModel(new SearchProduct())));

            Assert.IsTrue(wListProduct.Count > 0);
        }

        [TestMethod]
        //Filtro per Categoria e premere CERCA
        public async Task Search_1()
        {
            // Arrange
            UserBL userBL = new UserBL();
            userBL.SetGoogleCedential();
            ProductController controller = new ProductController();
            Models.SearchProduct wFilter = new Models.SearchProduct();
            wFilter.Category = "Donna";
            ProductBL wDB = new ProductBL();

            List<Product> wListProduct = ProductEntityToProductModel.ConvertoListProdyctEntityTOListProductModel(await wDB.GetListProducts(ProductEntityToProductModel.ConvertoProdyctEntityTOSearchProductModel(wFilter)));

            Assert.IsTrue(wListProduct.Count > 0);
        }

        [TestMethod]
        //Filtro per Marca e premere CERCA
        public async Task Search_2()
        {
            // Arrange
            UserBL userBL = new UserBL();
            userBL.SetGoogleCedential();
            ProductController controller = new ProductController();
            Models.SearchProduct wFilter = new Models.SearchProduct();
            wFilter.Brand = "armani";
            ProductBL wDB = new ProductBL();

            List<Product> wListProduct = ProductEntityToProductModel.ConvertoListProdyctEntityTOListProductModel(await wDB.GetListProducts(ProductEntityToProductModel.ConvertoProdyctEntityTOSearchProductModel(wFilter)));

            Assert.IsTrue(wListProduct.Count > 0);
        }
        [TestMethod]
        //Filtro per Modello e premere CERCA
        public async Task Search_4()
        {
            // Arrange
            UserBL userBL = new UserBL();
            userBL.SetGoogleCedential();
            ProductController controller = new ProductController();
            Models.SearchProduct wFilter = new Models.SearchProduct();
            wFilter.Model = "Gonna";
            ProductBL wDB = new ProductBL();

            List<Product> wListProduct = ProductEntityToProductModel.ConvertoListProdyctEntityTOListProductModel(await wDB.GetListProducts(ProductEntityToProductModel.ConvertoProdyctEntityTOSearchProductModel(wFilter)));

            Assert.IsTrue(wListProduct.Count > 0);
        }

        [TestMethod]
        //Filtro per Taglia e premere CERCA
        public async Task Search_5()
        {
            // Arrange
            UserBL userBL = new UserBL();
            userBL.SetGoogleCedential();
            ProductController controller = new ProductController();
            Models.SearchProduct wFilter = new Models.SearchProduct();
            wFilter.Headmoney = "L";
            ProductBL wDB = new ProductBL();

            List<Product> wListProduct = ProductEntityToProductModel.ConvertoListProdyctEntityTOListProductModel(await wDB.GetListProducts(ProductEntityToProductModel.ConvertoProdyctEntityTOSearchProductModel(wFilter)));
           
            Assert.IsTrue(wListProduct.Count > 0);
        }

        [TestMethod]
        //Filtro per Colore e premere CERCA
        public async Task Search_6()
        {
            // Arrange
            UserBL userBL = new UserBL();
            userBL.SetGoogleCedential();
            ProductController controller = new ProductController();
            Models.SearchProduct wFilter = new Models.SearchProduct();
            wFilter.Color = "Nero";
            ProductBL wDB = new ProductBL();
            List<Product> wListProduct = ProductEntityToProductModel.ConvertoListProdyctEntityTOListProductModel(await wDB.GetListProducts(ProductEntityToProductModel.ConvertoProdyctEntityTOSearchProductModel(wFilter)));

            Assert.IsTrue(wListProduct.Count > 0);
        }

        [TestMethod]
        //Filtro per Nome Prdotto e premere CERCA
        public async Task Search_7()
        {
            // Arrange
            UserBL userBL = new UserBL();
            userBL.SetGoogleCedential();
            Models.SearchProduct wFilter = new Models.SearchProduct();
            wFilter.ProductName = "polo";
            ProductBL wDB = new ProductBL();

            List<Product> wListProduct = ProductEntityToProductModel.ConvertoListProdyctEntityTOListProductModel(await wDB.GetListProducts(ProductEntityToProductModel.ConvertoProdyctEntityTOSearchProductModel(wFilter)));

            Assert.IsTrue(wListProduct.Count > 0);
        }

        [TestMethod]
        //Filtri e premere CERCA
        public async Task Search_8()
        {
            // Arrange
            UserBL userBL = new UserBL();
            userBL.SetGoogleCedential();
            Models.SearchProduct wFilter = new Models.SearchProduct();
            wFilter.Category = "Donna";
            wFilter.Brand = "armani";
            wFilter.ProductName = "gonna";
            ProductBL wDB = new ProductBL();

            List<Product> wListProduct = ProductEntityToProductModel.ConvertoListProdyctEntityTOListProductModel(await wDB.GetListProducts(ProductEntityToProductModel.ConvertoProdyctEntityTOSearchProductModel(wFilter)));

            Assert.IsFalse(wListProduct.Count > 0);
        }

        [TestMethod]
        //Filtri e premere CERCA
        public async Task InsertProduct()
        {
            // Arrange
            UserBL userBL = new UserBL();
            userBL.SetGoogleCedential();
            Models.Product wProdotto = new Models.Product();
            wProdotto.Category = "Uomo";
            wProdotto.Brand = "armani";
            wProdotto.ProductName = "Pantalone";
            wProdotto.Color = "Bianco";
            wProdotto.Description = "pantalone bello";
            wProdotto.Headmoney = "L";
            wProdotto.Price = 60.00;
            wProdotto.Quantity = 30;
            wProdotto.Model = "Pantalone";
            wProdotto.Image = @"C:\Users\feder\Desktop\Ingegneria del Software\pantalone bianco.png";
            ProductBL wDB = new ProductBL();

            await wDB.InsertProduct(ProductEntityToProductModel.ConvertoProdyctEntityTOProductModel(wProdotto),"");

            Models.SearchProduct wFilter = new Models.SearchProduct();
            wFilter.ProductName = "pantalone";

            List<Product> wListProduct = ProductEntityToProductModel.ConvertoListProdyctEntityTOListProductModel(await wDB.GetListProducts(ProductEntityToProductModel.ConvertoProdyctEntityTOSearchProductModel(wFilter)));


            Assert.IsTrue(wListProduct.Count > 0);
        }
    }
}
