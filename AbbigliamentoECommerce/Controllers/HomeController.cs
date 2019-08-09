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
        public async Task<ActionResult> Index(AbbigliamentoECommerce.Models.SearchProduct pProd)
        {
            ViewBag.ListProducts = TempData["ListProducts"];
            pProd.Brands = CategoryEntityTOCategoryModel.ConvertoListCategoryEntityTOListCategoryModel(await new CategoryBL().GetCategory("marca"));
            pProd.Colors = CategoryEntityTOCategoryModel.ConvertoListCategoryEntityTOListCategoryModel(await new CategoryBL().GetCategory("Colori"));
            pProd.Categories = CategoryEntityTOCategoryModel.ConvertoListCategoryEntityTOListCategoryModel(await new CategoryBL().GetCategory("Categorie"));
            pProd.Headmoneies = CategoryEntityTOCategoryModel.ConvertoListCategoryEntityTOListCategoryModel(await new CategoryBL().GetCategory("Taglie"));
            pProd.Models = CategoryEntityTOCategoryModel.ConvertoListCategoryEntityTOListCategoryModel(await new CategoryBL().GetCategory("Modelli"));
            return View(pProd);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public async Task<ActionResult> Login()
        {
            
            Session["CurrentUser"] = null;
            return View("Index");
        }

        public async Task<ActionResult> Logout()
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

                    pUser.wDetailUser =ConvertEntityUserTOUserModel.ConvertoUserEntityTOUserModel( await wUserBL.SigIn(pUser.Email, pUser.Password));
                    Session["CurrentUser"] = pUser;
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