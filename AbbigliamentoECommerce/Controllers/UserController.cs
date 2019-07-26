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
    public class UserController : Controller
    {
        // GET: User
        //public ActionResult Index()
        //{
        //    return View();
        //}

        // GET: User/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }

        // GET: User/Create
        public async Task<ActionResult> Create()
        {
            User wUser = new User();
            wUser.DateOfBirth = DateTime.Now;
            return View(wUser);
        }

        // POST: User/Create
        [HttpPost]
        public async Task<ActionResult> Create(User collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    UserBL wDB = new UserBL();
                   
                    wDB.InsertUser(ConvertEntityUserTOUserModel.ConvertoUserEntityTOUserModel(collection)).Wait();
                    return RedirectToAction("Login","Home");
                }
                else
                {
                    return View(collection);
                }
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    UserBL wDB = new UserBL();
                    AbbigliamentoECommerceEntity.User wUser = new AbbigliamentoECommerceEntity.User();
                    wUser.email = collection.Email;
                    wUser.Password = collection.Password;
                    wUser.nome = collection.Name;
                    wUser.Address = collection.Address;
                    wUser.City = collection.City;
                    wUser.cognome = collection.Surname;
                    wUser.DateOfBirth = collection.DateOfBirth;
                    wUser.District = collection.District;
                    wUser.TelefoneNumber = collection.TelefoneNumber;

                    wDB.InsertUser(wUser).Wait();
                    return RedirectToAction("Home");
                }
                else
                {
                    return View(collection);
                }
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


    }
}
