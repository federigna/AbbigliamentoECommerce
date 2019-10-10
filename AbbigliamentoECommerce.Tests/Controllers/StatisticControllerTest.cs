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
    public class StatisticControllerTest
    {
        [TestMethod]
        //Recupero Dati statistici avvenuto con successo
        public async Task BestSellingProduct()
        {
            UserBL userBL = new UserBL();
            userBL.SetGoogleCedential();
            StatisticController controller = new StatisticController();
            FilterStatistics wFilter = new FilterStatistics();
            wFilter.Category = "donna";
            wFilter.Datastart = new DateTime(2019,07,10);
            wFilter.DataEnd = new DateTime(2019, 09, 29);
            ViewResult result=await controller.BestSellingProduct(wFilter) as ViewResult;
            Dictionary<string, string> wChart =result.Model as Dictionary<string, string>;
            Assert.IsTrue(wChart.Keys.Count > 0);
        }

        [TestMethod]
        //Recupero Dati statistici fallito
        public async Task BestSellingProductFailed()
        {
            UserBL userBL = new UserBL();
            userBL.SetGoogleCedential();
            StatisticController controller = new StatisticController();
            FilterStatistics wFilter = new FilterStatistics();
            //wFilter.Category = "donna";
            wFilter.Datastart = new DateTime(2019, 07, 10);
            wFilter.DataEnd = new DateTime(2019, 09, 29);
            ViewResult result = await controller.BestSellingProduct(wFilter) as ViewResult;
            Dictionary<string, string> wChart = result.Model as Dictionary<string, string>;
            Assert.IsTrue(wChart.Keys.Count == 0);
        }

        [TestMethod]
        //Recupero Dati statistici avvenuto con successo
        public async Task BestSellingBrand()
        {
            UserBL userBL = new UserBL();
            userBL.SetGoogleCedential();
            StatisticController controller = new StatisticController();
            FilterStatistics wFilter = new FilterStatistics();
            wFilter.Category = "donna";
            wFilter.Datastart = new DateTime(2019, 07, 10);
            wFilter.DataEnd = new DateTime(2019, 09, 29);
            ViewResult result = await controller.BestSellingBrand(wFilter) as ViewResult;
            Dictionary<string, string> wChart = result.Model as Dictionary<string, string>;
            Assert.IsTrue(wChart.Keys.Count > 0);
        }

        [TestMethod]
        //Recupero Dati statistici fallito
        public async Task BestSellingBrandFailed()
        {
            UserBL userBL = new UserBL();
            userBL.SetGoogleCedential();
            StatisticController controller = new StatisticController();
            FilterStatistics wFilter = new FilterStatistics();
            //wFilter.Category = "donna";
            wFilter.Datastart = new DateTime(2019, 07, 10);
            wFilter.DataEnd = new DateTime(2019, 09, 29);
            ViewResult result = await controller.BestSellingBrand(wFilter) as ViewResult;
            Dictionary<string, string> wChart = result.Model as Dictionary<string, string>;
            Assert.IsTrue(wChart.Keys.Count == 0);
        }

        [TestMethod]
        //Recupero Dati statistici avvenuto con successo
        public async Task UserRegistred()
        {
            UserBL userBL = new UserBL();
            userBL.SetGoogleCedential();
            StatisticController controller = new StatisticController();
            FilterStatistics wFilter = new FilterStatistics();
            wFilter.Datastart = new DateTime(2019, 07, 10);
            wFilter.DataEnd = new DateTime(2019, 09, 29);
            ViewResult result = await controller.UserRegistred(wFilter) as ViewResult;
            Dictionary<string, string> wChart = result.Model as Dictionary<string, string>;
            Assert.IsTrue(wChart.Keys.Count > 0);
        }

        [TestMethod]
        //Recupero Dati statistici fallito
        public async Task UserRegistredFailed()
        {
            UserBL userBL = new UserBL();
            userBL.SetGoogleCedential();
            StatisticController controller = new StatisticController();
            FilterStatistics wFilter = new FilterStatistics();
            ViewResult result = await controller.UserRegistred(wFilter) as ViewResult;
            Dictionary<string, string> wChart = result.Model as Dictionary<string, string>;
            Assert.IsTrue(wChart.Keys.Count == 0);
        }
    }
}
