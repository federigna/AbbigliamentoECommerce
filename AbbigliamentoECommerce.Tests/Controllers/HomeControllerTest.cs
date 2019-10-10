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
using AbbigliamentoECommerceBL;

namespace AbbigliamentoECommerce.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        //Autenticazione avvenuta con successo
        public async Task Login()
        {
            // Arrange
            UserBL userBL = new UserBL();
            userBL.SetGoogleCedential();
            HomeController controller = new HomeController();
            LoggedUser wLogUser = new LoggedUser();
            wLogUser.Email = "test@test.it";
            wLogUser.Password = "asdasd";
            ViewResult result= await controller.Login(wLogUser) as ViewResult;
            wLogUser = result.Model as LoggedUser;
            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(wLogUser.wDetailUser.Id));
        }

        [TestMethod]
        //Autentizacione Fallita
        public async Task LoginFailed()
        {
            // Arrange
            UserBL userBL = new UserBL();
            userBL.SetGoogleCedential();
            HomeController controller = new HomeController();
            LoggedUser wLogUser = new LoggedUser();
            wLogUser.Email = "test@test.com";
            wLogUser.Password = "asd";
            ViewResult result = await controller.Login(wLogUser) as ViewResult;
            wLogUser = result.Model as LoggedUser;
            // Assert
            Assert.IsTrue(wLogUser.wDetailUser==null);
        }
    }
}
