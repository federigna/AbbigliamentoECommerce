using AbbigliamentoECommerce.Converter;
using AbbigliamentoECommerce.Models;
using AbbigliamentoECommerce.Utility;
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
            return View("Index",pProd);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public async Task<ActionResult> Login()
        {
            
            Session["CurrentUser"] = null;
            LoggedUser wLoggedUser = new LoggedUser();
            return View("Login",wLoggedUser);
        }

      

        public async Task<ActionResult> Logout()
        {
            Session["CurrentUser"] = null;
            Session["Ruolo"] = null;
            return await Index(new SearchProduct());
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
                    Session["Ruolo"] = pUser.wDetailUser.Ruolo;
                    return await Index(new SearchProduct ());
                }else
                {
                    return View(pUser);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Errore in fase di Login", ex);
                ViewBag.ErrorMessage = "Username o Password errati";
                return View(pUser);
            }
        }
    }
}