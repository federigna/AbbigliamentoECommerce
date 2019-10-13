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
            FilterStatistics wFilter = new FilterStatistics();
            wFilter.Category = "donna";
            wFilter.Datastart = new DateTime(2019,05,10);
            wFilter.DataEnd = new DateTime(2019, 10, 10);
            StatisticBL wBL = new StatisticBL();
            Dictionary<string, string> wChart = await wBL.BestSellingProduct(wFilter.Category, wFilter.Datastart, wFilter.DataEnd, "modello");

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
            wFilter.Category = "donna";
            wFilter.Datastart = new DateTime(2019, 07, 10);
            wFilter.DataEnd = new DateTime(2019, 09, 29);
            StatisticBL wBL = new StatisticBL();
            Dictionary<string, string> wChart = await wBL.BestSellingProduct(wFilter.Category, wFilter.Datastart, wFilter.DataEnd, "modello");

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
            StatisticBL wBL = new StatisticBL();
            Dictionary<string, string> wChart = await wBL.BestSellingProduct(wFilter.Category, wFilter.Datastart, wFilter.DataEnd, "marca");

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
            StatisticBL wBL = new StatisticBL();
            Dictionary<string, string> wChart = await wBL.BestSellingProduct(wFilter.Category, wFilter.Datastart, wFilter.DataEnd, "marca");

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
            StatisticBL wBL = new StatisticBL();
            Dictionary<string, string> wChart = await wBL.UserRegistred(wFilter.Datastart, wFilter.DataEnd);

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
            StatisticBL wBL = new StatisticBL();
            Dictionary<string, string> wChart = await wBL.UserRegistred(wFilter.Datastart, wFilter.DataEnd);

            Assert.IsTrue(wChart.Keys.Count == 0);
        }
    }
}
