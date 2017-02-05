using CSCGroupProject.Infrastructure;
using CSCGroupProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CSCGroupProject.Controllers
{
    [RoutePrefix("Admin")]
    [Authorize(Roles="Admin")]
    public class AdminController : Controller
    {
        ShoppingCartDBContext db = new ShoppingCartDBContext();
        //
        // GET: /Admin/
        [Route("List")]
        public ViewResult Index()
        {
            return View(db.Products);
        }

        [Route("Edit/{productId}")]
        [Route("Edit")]       
        public ViewResult Edit(int productId)
        {
            Product product = db.Products.FirstOrDefault(p => p.ProductID == productId);
            return View(product);
        }

        public ViewResult Create()
        {
            return View("Edit", new Product());
        }

        
        [HttpPost]
        [Route("Edit")]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ProductID == 0)
                {
                    db.Products.Add(product);
                }
                else
                {
                    Product dbEntry = db.Products.Find(product.ProductID);
                    if (dbEntry != null)
                    {
                        dbEntry.Name = product.Name;
                        dbEntry.Description = product.Description;
                        dbEntry.Price = product.Price;
                        dbEntry.Category = product.Category;
                    }
                }

                db.SaveChanges();
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        [HttpPost]
        public ActionResult Delete(int productId)
        {
            Product dbEntry = db.Products.Find(productId);
            if (dbEntry != null)
            {
                db.Products.Remove(dbEntry);
                db.SaveChanges();
            }

            if (dbEntry != null)
            {
                TempData["message"] = string.Format("{0} was deleted",
                dbEntry.Name);
            }
            return RedirectToAction("Index");
        }
	}
}