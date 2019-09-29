using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AbbigliamentoECommerce;
using AbbigliamentoECommerce.Controllers;
using System.Threading.Tasks;
using AbbigliamentoECommerce.Models;

namespace AbbigliamentoECommerce.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        //Autenticazione avvenuta con successo
        public async void Login()
        {
            // Arrange
            HomeController controller = new HomeController();
            LoggedUser wLogUser = new LoggedUser();
            wLogUser.Email = "test@test.com";
            wLogUser.Password = "asdasd";
            ViewResult result= await controller.Login() as ViewResult;
            wLogUser = result.Model as LoggedUser;
            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(wLogUser.wDetailUser.Id));
        }

        [TestMethod]
        //Autentizacione Fallita
        public async void LoginFailed()
        {
            // Arrange
            HomeController controller = new HomeController();
            LoggedUser wLogUser = new LoggedUser();
            wLogUser.Email = "test@test.com";
            wLogUser.Password = "asd";
            ViewResult result = await controller.Login() as ViewResult;
            wLogUser = result.Model as LoggedUser;
            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(wLogUser.wDetailUser.Id));
        }
    }
}
