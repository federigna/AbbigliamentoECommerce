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
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        // GET: Product/Details/5
        public async Task<ActionResult> Detail(string pId)
        {
            Product wProd = new Product();
            wProd=ProductEntityToProductModel.ConvertoProdyctModelTOProductEntity(await new ProductBL().GetProduct(pId));
            return View(wProd);
        }

        // GET: Product/Create
        public async Task<ActionResult> Create()
        {
            Product wProd = new Product();
            wProd.Brands =CategoryEntityTOCategoryModel.ConvertoListCategoryEntityTOListCategoryModel( await new CategoryBL().GetCategory("marca"));
            wProd.Colors = CategoryEntityTOCategoryModel.ConvertoListCategoryEntityTOListCategoryModel(await new CategoryBL().GetCategory("Colori"));
            wProd.Categories = CategoryEntityTOCategoryModel.ConvertoListCategoryEntityTOListCategoryModel(await new CategoryBL().GetCategory("Categorie"));
            wProd.Headmoneies = CategoryEntityTOCategoryModel.ConvertoListCategoryEntityTOListCategoryModel(await new CategoryBL().GetCategory("Taglie"));
            wProd.Models = CategoryEntityTOCategoryModel.ConvertoListCategoryEntityTOListCategoryModel(await new CategoryBL().GetCategory("Modelli"));
            return View(wProd);
        }

       
        [HttpPost]
        public async Task<ActionResult> Create(Product collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    ProductBL wDB = new ProductBL();
                   
                    wDB.InsertProduct(ProductEntityToProductModel.ConvertoProdyctEntityTOProductModel(collection),"").Wait();
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
                
                List<Product> wListProduct= ProductEntityToProductModel.ConvertoListProdyctEntityTOListProductModel( await wDB.GetListProducts(ProductEntityToProductModel.ConvertoProdyctEntityTOSearchProductModel(collection)));
                TempData["ListProducts"] = wListProduct;
                
                return RedirectToAction("Index","Home", collection);
              
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
