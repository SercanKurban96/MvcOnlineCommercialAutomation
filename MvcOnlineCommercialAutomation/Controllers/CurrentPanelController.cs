using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcOnlineCommercialAutomation.Models.Classes;

namespace MvcOnlineCommercialAutomation.Controllers
{
    public class CurrentPanelController : Controller
    {
        // GET: CurrentPanel
        Context c = new Context();
        [Authorize]
        public ActionResult Index()
        {
            var mail = (string)Session["CurrentMail"];
            var values = c.Messages.Where(x => x.MessageReceiver == mail).ToList();
            ViewBag.m = mail;
            var mailid = c.Currents.Where(x => x.CurrentMail == mail).Select(y => y.CurrentID).FirstOrDefault();
            ViewBag.mid = mailid;
            var totalsale = c.SalesTransactions.Where(x => x.Currentid == mailid).Count();
            ViewBag.ts = totalsale;
            var totalamount = c.SalesTransactions.Where(x => x.Currentid == mailid).Sum(y => y.TotalAmount);
            ViewBag.ta = totalamount;
            var totalproduct = c.SalesTransactions.Where(x => x.Currentid == mailid).Sum(y => y.Piece);
            ViewBag.tp = totalproduct;
            var namesurname = c.Currents.Where(x => x.CurrentMail == mail).Select(y => y.CurrentName + " " + y.CurrentSurname).FirstOrDefault();
            ViewBag.ns = namesurname;
            return View(values);
        }
        public ActionResult MyOrders()
        {
            var mail = (string)Session["CurrentMail"];
            var id = c.Currents.Where(x => x.CurrentMail == mail.ToString()).Select(y => y.CurrentID).FirstOrDefault();
            var values = c.SalesTransactions.Where(x => x.Currentid == id).ToList();
            return View(values);
        }
        public ActionResult IncomingMessages()
        {
            var mail = (string)Session["CurrentMail"];
            var messages = c.Messages.Where(x => x.MessageReceiver == mail).OrderByDescending(x => x.MessageID).ToList();
            var incomingCount = c.Messages.Count(x => x.MessageReceiver == mail).ToString();
            ViewBag.v1 = incomingCount;
            var outgoingCount = c.Messages.Count(x => x.MessageSender == mail).ToString();
            ViewBag.v2 = outgoingCount;
            return View(messages);
        }
        public ActionResult OutgoingMessages()
        {
            var mail = (string)Session["CurrentMail"];
            var messages = c.Messages.Where(x => x.MessageSender == mail).OrderByDescending(x => x.MessageID).ToList();
            var incomingCount = c.Messages.Count(x => x.MessageReceiver == mail).ToString();
            ViewBag.v1 = incomingCount;
            var outgoingCount = c.Messages.Count(x => x.MessageSender == mail).ToString();
            ViewBag.v2 = outgoingCount;
            return View(messages);
        }
        public ActionResult MessageDetail(int id)
        {
            var values = c.Messages.Where(x => x.MessageID == id).ToList();
            var mail = (string)Session["CurrentMail"];
            var incomingCount = c.Messages.Count(x => x.MessageReceiver == mail).ToString();
            ViewBag.v1 = incomingCount;
            var outgoingCount = c.Messages.Count(x => x.MessageSender == mail).ToString();
            ViewBag.v2 = outgoingCount;
            return View(values);
        }
        [HttpGet]
        public ActionResult NewMessage()
        {
            var mail = (string)Session["CurrentMail"];
            var incomingCount = c.Messages.Count(x => x.MessageReceiver == mail).ToString();
            ViewBag.v1 = incomingCount;
            var outgoingCount = c.Messages.Count(x => x.MessageSender == mail).ToString();
            ViewBag.v2 = outgoingCount;
            return View();
        }
        [HttpPost]
        public ActionResult NewMessage(Message m)
        {
            var mail = (string)Session["CurrentMail"];
            m.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            m.MessageSender = mail;
            c.Messages.Add(m);
            c.SaveChanges();
            return View();
        }
        public ActionResult CargoTracking(string p)
        {
            var cargos = from x in c.CargoDetails select x;
            cargos = cargos.Where(y => y.TrackingCode.Contains(p));
            return View(cargos.ToList());
        }
        public ActionResult CurrentCargoTracking(string id)
        {
            var values = c.CargoTrackings.Where(x => x.TrackingCode == id).ToList();
            return View(values);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
        public PartialViewResult Partial1()
        {
            var mail = (string)Session["CurrentMail"];
            var id = c.Currents.Where(x => x.CurrentMail == mail).Select(y => y.CurrentID).FirstOrDefault();
            var findCurrent = c.Currents.Find(id);
            return PartialView("Partial1", findCurrent);
        }
        public PartialViewResult Partial2()
        {
            var datas = c.Messages.Where(x => x.MessageSender == "admin").ToList();
            return PartialView(datas);
        }
        public ActionResult CurrentInfoEdit(Current cr)
        {
            var cur = c.Currents.Find(cr.CurrentID);
            cur.CurrentName = cr.CurrentName;
            cur.CurrentSurname = cr.CurrentSurname;
            cur.CurrentPassword = cr.CurrentPassword;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}