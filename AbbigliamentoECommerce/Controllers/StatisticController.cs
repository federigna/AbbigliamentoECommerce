using AbbigliamentoECommerce.Converter;
using AbbigliamentoECommerce.Models;
using AbbigliamentoECommerce.Utility;
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
            try
            {
                StatisticBL wBL = new StatisticBL();
                Dictionary<string, string> wChart = await wBL.BestSellingProduct(pFilter.Category, pFilter.Datastart, pFilter.DataEnd);

                var chart = new Chart(width: 300, height: 200)
                .AddSeries(chartType: "column",
                                xValue: wChart.Keys,
                                yValues: wChart.Values)
                                .GetBytes("png");
                ViewBag.FilterStastic = Init("BestSellingProduct");
                ViewBag.FilterStastic.Chart = base.File(chart, "image/bytes");
               
            }
            catch (Exception ex)
            {
                Log.Error("Errore in BestSellingProduct", ex);
            }
            
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

        public async Task<ActionResult> ViewStatistic(string pNameStatistic)
        {
            ViewBag.FilterStastic = await Init( pNameStatistic);
            return View();
        }

        private async Task<FilterStatistics> Init(string pNameStatistic)
        {
            try
            {
                FilterStatistics wFilter = new FilterStatistics();
                wFilter.NameStatistic = pNameStatistic;
                wFilter.Categories = CategoryEntityTOCategoryModel.ConvertoListCategoryEntityTOListCategoryModel(await new CategoryBL().GetCategory("Categorie"));
                return wFilter;
            }
            catch (Exception ex)
            {
                Log.Error("Inizializazione Filtri Statistici", ex);
            }
            return null;
        }
    }
}