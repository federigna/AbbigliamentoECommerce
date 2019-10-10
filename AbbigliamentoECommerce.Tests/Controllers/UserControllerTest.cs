using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using AbbigliamentoECommerce.Controllers;
using AbbigliamentoECommerce.Models;
using AbbigliamentoECommerceBL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AbbigliamentoECommerce.Tests.Controllers
{
    [TestClass]
    public class UserControllerTest
    {
        [TestMethod]
        //Iscrizione Corretta
        public async Task Create()
        {
            // Arrange
            UserBL userBL = new UserBL();
            userBL.SetGoogleCedential();
            UserController controller = new UserController();
            User wUser = new User();
            wUser.Address = "Via Test";
            wUser.City = "Napoli";
            wUser.DateOfBirth = new DateTime(1990,06,02);
            wUser.District = "NA";
            wUser.Email = "pippo@pippo.com";
            wUser.Name = "Pippo";
            wUser.Password = "pippo14";
            wUser.Ruolo = "Cliente";
            wUser.Surname = "Pippo";
            wUser.TelefoneNumber = "+397866215";
            ViewResult result = await controller.Create(wUser) as ViewResult;
            // Act
            // Assert
            Assert.IsNull(result.View);
        }

        [TestMethod]
        //Iscrizione fallita
        public async Task CreateFail()
        {
            // Arrange
            UserBL userBL = new UserBL();
            userBL.SetGoogleCedential();
            UserController controller = new UserController();
            User wUser = new User();
            wUser.Address = "Via Test";
            wUser.City = "Napoli";
            wUser.DateOfBirth = new DateTime(1990, 06, 02);
            wUser.District = "NA";
            wUser.Email = "pippo@pippo.com";
            wUser.Name = "Pippo";
            wUser.Password = "pippo14";
            wUser.Ruolo = "Cliente";
            wUser.Surname = "Pippo";
            wUser.TelefoneNumber = "7866215";
            ViewResult result = await controller.Create(wUser) as ViewResult;
            // Act
            // Assert
            Assert.IsNull(result.View);
        }
    }
}
