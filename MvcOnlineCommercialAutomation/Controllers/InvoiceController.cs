using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineCommercialAutomation.Models.Classes;

namespace MvcOnlineCommercialAutomation.Controllers
{
    public class InvoiceController : Controller
    {
        // GET: Invoice
        Context c = new Context();
        public ActionResult Index()
        {
            var list = c.Invoices.ToList();
            return View(list);
        }
        [HttpGet]
        public ActionResult InvoiceAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InvoiceAdd(Invoice p)
        {
            c.Invoices.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult InvoiceBring(int id)
        {
            var invoice = c.Invoices.Find(id);
            return View("InvoiceBring", invoice);
        }
        public ActionResult InvoiceEdit(Invoice p)
        {
            var inv = c.Invoices.Find(p.InvoiceID);
            inv.InvoiceSerialNumber = p.InvoiceSerialNumber;
            inv.InvoiceItemNumber = p.InvoiceItemNumber;
            inv.TaxDepartment = p.TaxDepartment;
            inv.InvoiceDate = p.InvoiceDate;
            inv.InvoiceTime = p.InvoiceTime;
            inv.Deliverer = p.Deliverer;
            inv.Receiver = p.Receiver;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult InvoiceDetail(int id)
        {
            var values = c.InvoiceItems.Where(x => x.Invoiceid == id).ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult InvoiceItemAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InvoiceItemAdd(InvoiceItem p)
        {
            c.InvoiceItems.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Dynamic()
        {
            Class3 cs = new Class3();
            cs.value1 = c.Invoices.ToList();
            cs.value2 = c.InvoiceItems.ToList();
            return View(cs);
        }
        public ActionResult InvoiceSave(string InvoiceSerialNumber, string InvoiceItemNumber, DateTime InvoiceDate, string TaxDepartment, string InvoiceTime, string Deliverer, string Receiver, string Total, InvoiceItem[] invoiceitems)
        {
            Invoice inv = new Invoice();
            inv.InvoiceSerialNumber = InvoiceSerialNumber;
            inv.InvoiceItemNumber = InvoiceItemNumber;
            inv.InvoiceDate = InvoiceDate;
            inv.TaxDepartment = TaxDepartment;
            inv.InvoiceTime = InvoiceTime;
            inv.Deliverer = Deliverer;
            inv.Receiver = Receiver;
            inv.Total = decimal.Parse(Total);
            c.Invoices.Add(inv);

            foreach (var x in invoiceitems)
            {
                InvoiceItem item = new InvoiceItem();
                item.Description = x.Description;
                item.UnitPrice = x.UnitPrice;
                item.Invoiceid = x.InvoiceItemID;
                item.Amount = x.Amount;
                item.Sum = x.Sum;
                c.InvoiceItems.Add(item);
            }

            c.SaveChanges();
            return Json("İşlem Başarılı", JsonRequestBehavior.AllowGet);
        }
    }
}