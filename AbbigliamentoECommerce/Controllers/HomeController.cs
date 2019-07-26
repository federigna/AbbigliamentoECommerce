using AbbigliamentoECommerce.Converter;
using AbbigliamentoECommerce.Models;
using AbbigliamentoECommerceBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AbbigliamentoECommerce.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            SearchProduct wProd = new SearchProduct();
            wProd.Brands = CategoryEntityTOCategoryModel.ConvertoListCategoryEntityTOListCategoryModel(await new CategoryBL().GetCategory("marca"));
            wProd.Colors = CategoryEntityTOCategoryModel.ConvertoListCategoryEntityTOListCategoryModel(await new CategoryBL().GetCategory("Colori"));
            wProd.Categories = CategoryEntityTOCategoryModel.ConvertoListCategoryEntityTOListCategoryModel(await new CategoryBL().GetCategory("Categorie"));
            wProd.Headmoneies = CategoryEntityTOCategoryModel.ConvertoListCategoryEntityTOListCategoryModel(await new CategoryBL().GetCategory("Taglie"));
            wProd.Models = CategoryEntityTOCategoryModel.ConvertoListCategoryEntityTOListCategoryModel(await new CategoryBL().GetCategory("Modelli"));
            return View(wProd);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public async Task<ActionResult> Login()
        {

            LoggedUser wUser = new LoggedUser();
            return View(wUser);
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoggedUser pUser)
        {

            try
            {

                if (ModelState.IsValid)
                {
                    UserBL wUserBL = new UserBL();
                    User wUser =ConvertEntityUserTOUserModel.ConvertoUserEntityTOUserModel( await wUserBL.SigIn(pUser.Email, pUser.Password));
                    Session["CurrentUser"] = wUser;
                    return View("Index");
                }else
                {
                    return View(pUser);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}