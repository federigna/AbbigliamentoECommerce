﻿using AbbigliamentoECommerce.Converter;
using AbbigliamentoECommerce.Models;
using AbbigliamentoECommerceBL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AbbigliamentoECommerce.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        // GET: Product/Details/5
        public async Task<ActionResult> Detail(string pId, string pScript = "")
        {
            Product wProd = new Product();
            wProd = ProductEntityToProductModel.ConvertoProdyctModelTOProductEntity(await new ProductBL().GetProduct(pId));
            ViewBag.ScriptDetail = pScript; 
            return View("Detail", wProd);
        }

        public async Task<ActionResult> AddCart(string pId, int pQuantityBy)
        {
           LoggedUser wLogUser = (LoggedUser)Session["CurrentUser"];
            await new CartBL().AddProductToCart(wLogUser.wDetailUser.Id, pId, pQuantityBy);
            return await Detail(pId, "Prodotto aggiunto al carrello.");
        }
        // GET: Product/Create
        public async Task<ActionResult> Create()
        {
            Product wProd = new Product();
            wProd.Brands = CategoryEntityTOCategoryModel.ConvertoListCategoryEntityTOListCategoryModel(await new CategoryBL().GetCategory("marca"));
            wProd.Colors = CategoryEntityTOCategoryModel.ConvertoListCategoryEntityTOListCategoryModel(await new CategoryBL().GetCategory("Colori"));
            wProd.Categories = CategoryEntityTOCategoryModel.ConvertoListCategoryEntityTOListCategoryModel(await new CategoryBL().GetCategory("Categorie"));
            wProd.Headmoneies = CategoryEntityTOCategoryModel.ConvertoListCategoryEntityTOListCategoryModel(await new CategoryBL().GetCategory("Taglie"));
            wProd.Models = CategoryEntityTOCategoryModel.ConvertoListCategoryEntityTOListCategoryModel(await new CategoryBL().GetCategory("Modelli"));
            return View(wProd);
        }


        [HttpPost]
        public async Task<ActionResult> Create(Product collection,HttpPostedFileBase pFile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    ProductBL wDB = new ProductBL();
                    var appSetting = ConfigurationManager.AppSettings;
                    string pathImage = appSetting["PathImage"];
                    collection.Image = pathImage + collection.ImageFile.FileName;
                    collection.ImageFile.SaveAs(collection.Image);
                    await wDB.InsertProduct(ProductEntityToProductModel.ConvertoProdyctEntityTOProductModel(collection), "");
                    return RedirectToAction("Home");
                }
                else
                {
                    collection.Brands = CategoryEntityTOCategoryModel.ConvertoListCategoryEntityTOListCategoryModel(await new CategoryBL().GetCategory("marca"));
                    collection.Colors = CategoryEntityTOCategoryModel.ConvertoListCategoryEntityTOListCategoryModel(await new CategoryBL().GetCategory("Colori"));
                    collection.Categories = CategoryEntityTOCategoryModel.ConvertoListCategoryEntityTOListCategoryModel(await new CategoryBL().GetCategory("Categorie"));
                    collection.Headmoneies = CategoryEntityTOCategoryModel.ConvertoListCategoryEntityTOListCategoryModel(await new CategoryBL().GetCategory("Taglie"));
                    collection.Models = CategoryEntityTOCategoryModel.ConvertoListCategoryEntityTOListCategoryModel(await new CategoryBL().GetCategory("Modelli"));

                    return View(collection);
                }
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        // GET: Product/Edit/5
        public ActionResult Edit(string id)
        {
            return View();
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
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

        [HttpPost]
        public async Task<ActionResult> Search(SearchProduct collection)
        {
            try
            {

                // TODO: Add insert logic here
                ProductBL wDB = new ProductBL();

                List<Product> wListProduct = ProductEntityToProductModel.ConvertoListProdyctEntityTOListProductModel(await wDB.GetListProducts(ProductEntityToProductModel.ConvertoProdyctEntityTOSearchProductModel(collection)));
                TempData["ListProducts"] = wListProduct;

                return RedirectToAction("Index", "Home", collection);

            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
