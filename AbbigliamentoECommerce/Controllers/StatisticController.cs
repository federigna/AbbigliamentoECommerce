using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;


namespace AbbigliamentoECommerce.Controllers
{
    public class StatisticController : Controller
    {
        // GET: Statistic
        public ActionResult BestSellingProduct()
        {
            var chart = new Chart(width: 300, height: 200)
            .AddSeries(chartType: "column",
                            xValue: new[] { "gonna", "maglia", "pantalone" },
                            yValues: new[] { "30", "40", "20" })
                            .GetBytes("png");
            return base.File(chart, "image/bytes");
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

        public ActionResult ViewStatistic()
        {
            return View();
        }
    }
}