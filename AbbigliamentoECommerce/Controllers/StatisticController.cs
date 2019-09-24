using AbbigliamentoECommerce.Models;
using AbbigliamentoECommerceBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;


namespace AbbigliamentoECommerce.Controllers
{
    public class StatisticController : Controller
    {
     
        // GET: Statistic
        public async Task<ActionResult> BestSellingProduct(FilterStatistics pFilter)
        {
            StatisticBL wBL = new StatisticBL();
            Dictionary<string,string> wChart= await wBL.BestSellingProduct(pFilter.Category, pFilter.Datastart, pFilter.DataEnd);

            var chart = new Chart(width: 300, height: 200)
            .AddSeries(chartType: "column",
                            xValue:wChart.Keys,
                            yValues: wChart.Values)
                            .GetBytes("png");
            pFilter.Chart= base.File(chart, "image/bytes");
            ViewBag.FilterStastic = pFilter;
            return View("ViewStatistic");
        }

        public ActionResult BestSellingBrand()
        {
            var chart = new Chart(width: 300, height: 200)
            .AddSeries(chartType: "pie",
                            xValue: new[] { "gonna", "maglia", "pantalone" },
                            yValues: new[] { "30", "40", "20" })
                            .GetBytes("png");
            return base.File(chart, "image/bytes");
        }

        public ActionResult UserRegistred()
        {
            var chart = new Chart(width: 300, height: 200)
            .AddSeries(chartType: "line",
                            xValue: new[] { "gonna", "maglia", "pantalone" },
                            yValues: new[] { "30", "40", "20" })
                            .GetBytes("png");
            return base.File(chart, "image/bytes");
        }

        public ActionResult GainByCategory()
        {
            var chart = new Chart(width: 300, height: 200)
            .AddSeries(chartType: "column",
                            xValue: new[] { "gonna", "maglia", "pantalone" },
                            yValues: new[] { "30", "40", "20" })
                            .GetBytes("png");
            return base.File(chart, "image/bytes");
        }

        public ActionResult ViewStatistic(string pNameStatistic)
        {
            //switch (pNameStatistic)
            //{
            //    case "BestSellingProduct":
            //        return BestSellingProduct();
            //    case "BestSellingBrand":
            //        return BestSellingBrand();
            //    case "UserRegistred":
            //        return UserRegistred();
            //    case "GainByCategory":
            //        return GainByCategory();

            //}
            FilterStatistics wFilter = new FilterStatistics();
            wFilter.NameStatistic = pNameStatistic;
            ViewBag.FilterStastic = wFilter;
            return View();
        }
    }
}