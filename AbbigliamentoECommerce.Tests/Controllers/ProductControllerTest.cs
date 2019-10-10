using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AbbigliamentoECommerce.Controllers;
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
            ViewResult result = await controller.Search(new Models.SearchProduct()) as ViewResult;
            // Act
            //ViewResult result = controller.Index() as ViewResult;
            List<Product> wListProduct = result.Model != null ? result.Model as List<Product> : null;
            // Assert
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
            ViewResult result = await controller.Search(wFilter) as ViewResult;
            // Act
            //ViewResult result = controller.Index() as ViewResult;
            List<Product> wListProduct = result.Model != null ? result.Model as List<Product> : null;
            // Assert
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
            ViewResult result = await controller.Search(wFilter) as ViewResult;
            // Act
            //ViewResult result = controller.Index() as ViewResult;
            List<Product> wListProduct = result.Model != null ? result.Model as List<Product> : null;
            // Assert
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
            ViewResult result = await controller.Search(wFilter) as ViewResult;
            // Act
            //ViewResult result = controller.Index() as ViewResult;
            List<Product> wListProduct = result.Model != null ? result.Model as List<Product> : null;
            // Assert
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
            ViewResult result = await controller.Search(wFilter) as ViewResult;
            // Act
            //ViewResult result = controller.Index() as ViewResult;
            List<Product> wListProduct = result.Model != null ? result.Model as List<Product> : null;
            // Assert
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
            ViewResult result = await controller.Search(wFilter) as ViewResult;
            // Act
            //ViewResult result = controller.Index() as ViewResult;
            List<Product> wListProduct = result.Model != null ? result.Model as List<Product> : null;
            // Assert
            Assert.IsTrue(wListProduct.Count > 0);
        }

        [TestMethod]
        //Filtro per Nome Prdotto e premere CERCA
        public async Task Search_7()
        {
            // Arrange
            UserBL userBL = new UserBL();
            userBL.SetGoogleCedential();
            ProductController controller = new ProductController();
            Models.SearchProduct wFilter = new Models.SearchProduct();
            wFilter.ProductName = "polo";
            ViewResult result = await controller.Search(wFilter) as ViewResult;
            // Act
            //ViewResult result = controller.Index() as ViewResult;
            List<Product> wListProduct = result.Model != null ? result.Model as List<Product> : null;
            // Assert
            Assert.IsTrue(wListProduct.Count > 0);
        }

        [TestMethod]
        //Filtri e premere CERCA
        public async Task Search_8()
        {
            // Arrange
            UserBL userBL = new UserBL();
            userBL.SetGoogleCedential();
            ProductController controller = new ProductController();
            Models.SearchProduct wFilter = new Models.SearchProduct();
            wFilter.Category = "donna";
            wFilter.Brand = "armani";
            wFilter.ProductName = "gonna";

            ViewResult result = await controller.Search(wFilter) as ViewResult;
            // Act
            //ViewResult result = controller.Index() as ViewResult;
            List<Product> wListProduct = result.Model != null ? result.Model as List<Product> : null;
            // Assert
            Assert.IsTrue(wListProduct.Count > 0);
        }
    }
}
