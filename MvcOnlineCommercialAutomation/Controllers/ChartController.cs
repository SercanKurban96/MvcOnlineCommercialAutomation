using MvcOnlineCommercialAutomation.Models.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MvcOnlineCommercialAutomation.Controllers
{
    public class ChartController : Controller
    {
        // GET: Chart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index2()
        {
            var drawchart = new Chart(600, 600);
            drawchart.AddTitle("Kategori - Ürün Stok Sayısı").AddLegend("Stok").AddSeries("Değerler", xValue: new[] { "Mobilya", "Ofis Eşyaları", "Bilgisayar" }, yValues: new[] { 85, 66, 98 }).Write();
            return File(drawchart.ToWebImage().GetBytes(), "image/jpeg");
        }
        Context c = new Context();
        public ActionResult Index3()
        {
            ArrayList xvalue = new ArrayList();
            ArrayList yvalue = new ArrayList();
            var results = c.Products.ToList();
            results.ToList().ForEach(x => xvalue.Add(x.ProductName));
            results.ToList().ForEach(y => yvalue.Add(y.Stock));
            var chart = new Chart(width: 800, height: 800).AddTitle("Stoklar").AddSeries(chartType: "Pie", name: "Stok", xValue: xvalue, yValues: yvalue);
            return File(chart.ToWebImage().GetBytes(), "image/jpeg");
        }
        public ActionResult Index4()
        {
            return View();
        }
        public ActionResult VisualizeProductResult()
        {
            return Json(Productlist(), JsonRequestBehavior.AllowGet);
        }
        public List<chartclass1> Productlist()
        {
            List<chartclass1> cl = new List<chartclass1>();
            cl.Add(new chartclass1()
            {
                productname = "Bilgisayar",
                stock = 120
            });
            cl.Add(new chartclass1()
            {
                productname = "Beyaz Eşya",
                stock = 150
            });
            cl.Add(new chartclass1()
            {
                productname = "Mobilya",
                stock = 70
            });
            cl.Add(new chartclass1()
            {
                productname = "Küçük Ev Aletleri",
                stock = 180
            });
            cl.Add(new chartclass1()
            {
                productname = "Mobil Cihazlar",
                stock = 90
            });
            return cl;
        }
        public ActionResult Index5()
        {
            return View();
        }
        public ActionResult VisualizeProductResult2()
        {
            return Json(Productlist2(), JsonRequestBehavior.AllowGet);
        }
        public List<chartclass2> Productlist2()
        {
            List<chartclass2> cl = new List<chartclass2>();
            using (var c = new Context())
            {
                cl = c.Products.Select(x => new chartclass2
                {
                    prdct = x.ProductName,
                    stck = x.Stock
                }).ToList();
            }
            return cl;
        }
        public ActionResult Index6()
        {
            return View();
        }
        public ActionResult Index7()
        {
            return View();
        }
    }
}