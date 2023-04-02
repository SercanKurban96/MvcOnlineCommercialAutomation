using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineCommercialAutomation.Models.Classes;

namespace MvcOnlineCommercialAutomation.Controllers
{
    public class SalesController : Controller
    {
        // GET: Sales
        Context c = new Context();
        public ActionResult Index()
        {
            var values = c.SalesTransactions.ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult NewSale()
        {
            List<SelectListItem> value1 = (from x in c.Products.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.ProductName,
                                               Value = x.ProductID.ToString()
                                           }).ToList();
            
            List<SelectListItem> value2 = (from x in c.Currents.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CurrentName + " " + x.CurrentSurname,
                                               Value = x.CurrentID.ToString()
                                           }).ToList();

            List<SelectListItem> value3 = (from x in c.Employees.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.EmployeeName + " " + x.EmployeeSurname,
                                               Value = x.EmployeeID.ToString()
                                           }).ToList();

            ViewBag.val1 = value1;
            ViewBag.val2 = value2;
            ViewBag.val3 = value3;

            return View();
        }
        [HttpPost]
        public ActionResult NewSale(SalesTransaction sales)
        {
            sales.SalesDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.SalesTransactions.Add(sales);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SaleBring(int id)
        {
            List<SelectListItem> value1 = (from x in c.Products.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.ProductName,
                                               Value = x.ProductID.ToString()
                                           }).ToList();

            List<SelectListItem> value2 = (from x in c.Currents.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CurrentName + " " + x.CurrentSurname,
                                               Value = x.CurrentID.ToString()
                                           }).ToList();

            List<SelectListItem> value3 = (from x in c.Employees.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.EmployeeName + " " + x.EmployeeSurname,
                                               Value = x.EmployeeID.ToString()
                                           }).ToList();

            ViewBag.val1 = value1;
            ViewBag.val2 = value2;
            ViewBag.val3 = value3;

            var value = c.SalesTransactions.Find(id);
            return View("SaleBring", value);
        }
        public ActionResult SalesEdit(SalesTransaction sales)
        {
            var sls = c.SalesTransactions.Find(sales.SalesID);
            sls.Currentid = sales.SalesID;
            sls.Piece = sales.Piece;
            sls.Price = sales.Price;
            sls.Employeeid = sales.Employeeid;
            sls.SalesDate = sales.SalesDate;
            sls.TotalAmount = sales.TotalAmount;
            sls.Productid = sales.Productid;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SalesDetail(int id)
        {
            var values = c.SalesTransactions.Where(x => x.SalesID == id).ToList();
            return View(values);
        }
    }
}