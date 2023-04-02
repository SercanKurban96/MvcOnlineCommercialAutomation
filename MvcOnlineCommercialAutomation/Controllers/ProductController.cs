using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using MvcOnlineCommercialAutomation.Models.Classes;
using Newtonsoft.Json.Linq;

namespace MvcOnlineCommercialAutomation.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        Context c = new Context();
        public ActionResult Index(string p)
        {
            //var products = c.Products.ToList();
            //var products = c.Products.Where(x => x.Status == true).ToList();

            var products = from x in c.Products select x;
            if (!string.IsNullOrEmpty(p))
            {
                products = products.Where(y => y.ProductName.Contains(p));
            }
            return View(products.ToList());
        }
        [HttpGet]
        public ActionResult ProductAdd()
        {
            List<SelectListItem> value1 = (from x in c.Categories.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CategoryName,
                                               Value = x.CategoryID.ToString()
                                           }).ToList();
            ViewBag.vl1 = value1;
            return View();
        }
        [HttpPost]
        public ActionResult ProductAdd(Product p)
        {
            c.Products.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ProductDelete(int id)
        {
            var value = c.Products.Find(id);
            value.Status = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ProductBring(int id)
        {
            List<SelectListItem> value1 = (from x in c.Categories.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CategoryName,
                                               Value = x.CategoryID.ToString()
                                           }).ToList();
            ViewBag.vl1 = value1;

            var productvalue = c.Products.Find(id);
            return View("ProductBring", productvalue);
        }
        public ActionResult ProductEdit(Product p)
        {
            var prod = c.Products.Find(p.ProductID);
            prod.PurchasePrice = p.PurchasePrice;
            prod.Status = p.Status;
            prod.Categoryid = p.Categoryid;
            prod.Brand = p.Brand;
            prod.SalePrice = p.SalePrice;
            prod.Stock = p.Stock;
            prod.ProductName = p.ProductName;
            prod.ProductImage = p.ProductImage;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ProductList()
        {
            var values = c.Products.ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult MakeSale(int id)
        {
            List<SelectListItem> value3 = (from x in c.Employees.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.EmployeeName + " " + x.EmployeeSurname,
                                               Value = x.EmployeeID.ToString()
                                           }).ToList();

            ViewBag.val3 = value3;
            var value1 = c.Products.Find(id);
            ViewBag.val1 = value1.ProductID;
            ViewBag.val2 = value1.SalePrice;
            return View();
        }
        [HttpPost]
        public ActionResult MakeSale(SalesTransaction p)
        {
            p.SalesDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.SalesTransactions.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index","Sales");
        }
    }
}