using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using MvcOnlineCommercialAutomation.Models.Classes;

namespace MvcOnlineCommercialAutomation.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        Context c = new Context();
        public ActionResult Index(int page = 1)
        {
            //var values = c.Categories.ToList();
            var values = c.Categories.ToList().ToPagedList(page, 4);
            return View(values);
        }
        [HttpGet]
        public ActionResult CategoryAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CategoryAdd(Category cat)
        {
            if (!ModelState.IsValid)
            {
                return View("CategoryAdd");
            }
            c.Categories.Add(cat);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CategoryDelete(int id)
        {
            var ctg = c.Categories.Find(id);
            c.Categories.Remove(ctg);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CategoryBring(int id)
        {
            var category = c.Categories.Find(id);
            return View("CategoryBring", category);
        }
        public ActionResult CategoryEdit(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View("CategoryBring");
            }
            var ctgr = c.Categories.Find(category.CategoryID);
            ctgr.CategoryName = category.CategoryName;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Trying()
        {
            Class2 cs = new Class2();
            cs.Categories = new SelectList(c.Categories, "CategoryID", "CategoryName");
            cs.Products = new SelectList(c.Products, "ProductID", "ProductName");
            return View(cs);
        }
        public JsonResult ProductBring(int p)
        {
            var productList = (from x in c.Products
                               join y in c.Categories
                               on x.Category.CategoryID equals y.CategoryID
                               where x.Category.CategoryID == p
                               select new
                               {
                                   Text = x.ProductName,
                                   Value = x.ProductID.ToString()
                               }).ToList();
            return Json(productList, JsonRequestBehavior.AllowGet);
        }
    }
}