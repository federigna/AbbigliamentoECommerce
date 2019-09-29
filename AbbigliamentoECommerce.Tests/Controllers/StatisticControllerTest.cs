using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AbbigliamentoECommerce.Controllers;
using AbbigliamentoECommerce.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AbbigliamentoECommerce.Tests.Controllers
{
    [TestClass]
    public class StatisticControllerTest
    {
        [TestMethod]
        //Recupero Dati statistici avvenuto con successo
        public async void BestSellingProduct()
        {
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
        public async void BestSellingProductFailed()
        {
            StatisticController controller = new StatisticController();
            FilterStatistics wFilter = new FilterStatistics();
            //wFilter.Category = "donna";
            wFilter.Datastart = new DateTime(2019, 07, 10);
            wFilter.DataEnd = new DateTime(2019, 09, 29);
            ViewResult result = await controller.BestSellingProduct(wFilter) as ViewResult;
            Dictionary<string, string> wChart = result.Model as Dictionary<string, string>;
            Assert.IsTrue(wChart.Keys.Count > 0);
        }

        [TestMethod]
        //Recupero Dati statistici avvenuto con successo
        public async void BestSellingBrand()
        {
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
        public async void BestSellingBrandFailed()
        {
            StatisticController controller = new StatisticController();
            FilterStatistics wFilter = new FilterStatistics();
            //wFilter.Category = "donna";
            wFilter.Datastart = new DateTime(2019, 07, 10);
            wFilter.DataEnd = new DateTime(2019, 09, 29);
            ViewResult result = await controller.BestSellingBrand(wFilter) as ViewResult;
            Dictionary<string, string> wChart = result.Model as Dictionary<string, string>;
            Assert.IsTrue(wChart.Keys.Count > 0);
        }

        [TestMethod]
        //Recupero Dati statistici avvenuto con successo
        public async void UserRegistred()
        {
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
        public async void UserRegistredFailed()
        {
            StatisticController controller = new StatisticController();
            FilterStatistics wFilter = new FilterStatistics();
            ViewResult result = await controller.UserRegistred(wFilter) as ViewResult;
            Dictionary<string, string> wChart = result.Model as Dictionary<string, string>;
            Assert.IsTrue(wChart.Keys.Count > 0);
        }
    }
}
