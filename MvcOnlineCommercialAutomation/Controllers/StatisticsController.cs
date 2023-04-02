using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using MvcOnlineCommercialAutomation.Models.Classes;

namespace MvcOnlineCommercialAutomation.Controllers
{
    public class StatisticsController : Controller
    {
        // GET: Statistics
        Context c = new Context();
        public ActionResult Index()
        {
            var value1 = c.Currents.Count().ToString();
            ViewBag.v1 = value1;

            var value2 = c.Products.Count().ToString();
            ViewBag.v2 = value2;

            var value3 = c.Employees.Count().ToString();
            ViewBag.v3 = value3;

            var value4 = c.Categories.Count().ToString();
            ViewBag.v4 = value4;

            var value5 = c.Products.Sum(x => x.Stock).ToString();
            ViewBag.v5 = value5;

            var value6 = (from x in c.Products select x.Brand).Distinct().Count().ToString();
            ViewBag.v6 = value6;

            var value7 = c.Products.Count(x => x.Stock < 20).ToString();
            ViewBag.v7 = value7;

            var value8 = (from x in c.Products orderby x.SalePrice descending select x.ProductName).FirstOrDefault();
            ViewBag.v8 = value8;

            var value9 = (from x in c.Products orderby x.SalePrice ascending select x.ProductName).FirstOrDefault();
            ViewBag.v9 = value9;

            var value10 = c.Products.Count(x => x.ProductName == "Buzdolabı").ToString();
            ViewBag.v10 = value10;

            var value11 = c.Products.Count(x => x.ProductName == "Laptop").ToString();
            ViewBag.v11 = value11;

            var value12 = c.Products.GroupBy(x => x.Brand).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault();
            ViewBag.v12 = value12;

            var value13 = c.Products.Where(u => u.ProductID == (c.SalesTransactions.GroupBy(x => x.Productid).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault())).Select(k=>k.ProductName).FirstOrDefault();
            ViewBag.v13 = value13;

            var value14 = c.SalesTransactions.Sum(x => x.TotalAmount).ToString();
            ViewBag.v14 = value14;

            DateTime today = DateTime.Today;
            var value15 = c.SalesTransactions.Count(x => x.SalesDate == today).ToString();
            ViewBag.v15 = value15;

            var value16 = c.SalesTransactions.Where(x => x.SalesDate == today).Sum(y => (decimal?)y.TotalAmount).ToString();
            ViewBag.v16 = value16;

            return View();
        }
        public ActionResult SimpleTables()
        {
            var query = from x in c.Currents
                        group x by x.CurrentCity into g
                        select new ClassGroup
                        {
                            City = g.Key,
                            Count = g.Count()
                        };
            return View(query.ToList());
        }
        public PartialViewResult Partial1()
        {
            var query2 = from x in c.Employees
                         group x by x.Department.DepartmentName into g
                         select new ClassGroup2
                         {
                             Department = g.Key,
                             Count = g.Count()
                         };
            return PartialView(query2.ToList());
        }
        public PartialViewResult Partial2()
        {
            var query3 = c.Currents.ToList();
            return PartialView(query3);
        }
        public PartialViewResult Partial3()
        {
            var query4 = c.Products.ToList();
            return PartialView(query4);
        }
        public PartialViewResult Partial4()
        {
            var query5 = from x in c.Products
                         group x by x.Brand into g
                         select new ClassGroup3
                         {
                             Brand = g.Key,
                             Count = g.Count()
                         };
            return PartialView(query5.ToList());
        }
    }
}