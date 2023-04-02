using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineCommercialAutomation.Models.Classes;

namespace MvcOnlineCommercialAutomation.Controllers
{
    public class CurrentController : Controller
    {
        // GET: Current
        Context c = new Context();
        public ActionResult Index()
        {
            var values = c.Currents.Where(x => x.CurrentStatus == true).ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult CurrentAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CurrentAdd(Current cur)
        {
            if (!ModelState.IsValid)
            {
                return View("CurrentAdd");
            }
            cur.CurrentStatus = true;
            c.Currents.Add(cur);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CurrentDelete(int id)
        {
            var cr = c.Currents.Find(id);
            cr.CurrentStatus = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CurrentBring(int id)
        {
            var curr = c.Currents.Find(id);
            return View("CurrentBring", curr);
        }
        public ActionResult CurrentEdit(Current p)
        {
            if (!ModelState.IsValid)
            {
                return View("CurrentBring");
            }
            var crr = c.Currents.Find(p.CurrentID);
            crr.CurrentName = p.CurrentName;
            crr.CurrentSurname = p.CurrentSurname;
            crr.CurrentCity = p.CurrentCity;
            crr.CurrentMail = p.CurrentMail;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CustomerSales(int id)
        {
            var values = c.SalesTransactions.Where(x=>x.Currentid == id).ToList();
            var cr = c.Currents.Where(x => x.CurrentID == id).Select(y => y.CurrentName + " " + y.CurrentSurname).FirstOrDefault();
            ViewBag.current = cr;
            return View(values);
        }
    }
}