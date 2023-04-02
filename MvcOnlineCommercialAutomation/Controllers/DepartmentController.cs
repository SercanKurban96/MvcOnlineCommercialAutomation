using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineCommercialAutomation.Models.Classes;

namespace MvcOnlineCommercialAutomation.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Department
        Context c = new Context();
        public ActionResult Index()
        {
            var values = c.Departments.Where(x => x.DepartmentStatus == true).ToList();
            return View(values);
        }
        [Authorize(Roles = "A")]
        [HttpGet]
        public ActionResult DepartmentAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DepartmentAdd(Department d)
        {
            if (!ModelState.IsValid)
            {
                return View("DepartmentAdd");
            }
            d.DepartmentStatus = true;
            c.Departments.Add(d);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DepartmentDelete(int id)
        {
            var dep = c.Departments.Find(id);
            dep.DepartmentStatus = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DepartmentBring(int id)
        {
            var dpt = c.Departments.Find(id);
            return View("DepartmentBring", dpt);
        }
        public ActionResult DepartmentEdit(Department d)
        {
            if (!ModelState.IsValid)
            {
                return View("DepartmentBring");
            }
            var dept = c.Departments.Find(d.DepartmentID);
            dept.DepartmentName = d.DepartmentName;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DepartmentDetail(int id)
        {
            var values = c.Employees.Where(x => x.Departmentid == id).ToList();
            var dpt = c.Departments.Where(x => x.DepartmentID == id).Select(y => y.DepartmentName).FirstOrDefault();
            ViewBag.d = dpt;
            return View(values);
        }
        public ActionResult DepartmentEmployeeSale(int id)
        {
            var values = c.SalesTransactions.Where(x => x.Employeeid == id).ToList();
            var per = c.Employees.Where(x => x.EmployeeID == id).Select(y => y.EmployeeName + " " + y.EmployeeSurname).FirstOrDefault();
            ViewBag.dpers = per;
            return View(values);
        }
    }
}