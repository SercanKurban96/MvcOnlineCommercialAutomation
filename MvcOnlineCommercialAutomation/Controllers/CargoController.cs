using MvcOnlineCommercialAutomation.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineCommercialAutomation.Controllers
{
    public class CargoController : Controller
    {
        // GET: Cargo
        Context c = new Context();
        public ActionResult Index(string p)
        {
            //var cargos = c.CargoDetails.ToList();
            //return View(cargos);

            var cargos = from x in c.CargoDetails select x;
            if (!string.IsNullOrEmpty(p))
            {
                cargos = cargos.Where(y => y.TrackingCode.Contains(p));
            }
            return View(cargos.ToList());
        }
        [HttpGet]
        public ActionResult NewCargo()
        {
            Random rnd = new Random();
            string[] characters = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            int c1, c2, c3;
            c1 = rnd.Next(0, characters.Length);
            c2 = rnd.Next(0, characters.Length);
            c3 = rnd.Next(0, characters.Length);

            int n1, n2, n3;
            n1 = rnd.Next(100, 1000);
            n2 = rnd.Next(10, 99);
            n3 = rnd.Next(10, 99);

            string code = n1.ToString() + characters[c1] + n2 + characters[c2] + n3 + characters[c3];
            ViewBag.trackingCode = code;
            return View();
        }
        [HttpPost]
        public ActionResult NewCargo(CargoDetail cargo)
        {
            c.CargoDetails.Add(cargo);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CargoTracking(string id)
        {
            var values = c.CargoTrackings.Where(x => x.TrackingCode == id).ToList();
            return View(values);
        }
    }
}