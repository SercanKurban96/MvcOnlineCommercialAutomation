using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineCommercialAutomation.Models.Classes;

namespace MvcOnlineCommercialAutomation.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        Context c = new Context();
        public ActionResult Index()
        {
            var values = c.Employees.ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult EmployeeAdd()
        {
            List<SelectListItem> value1 = (from x in c.Departments.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmentName,
                                               Value = x.DepartmentID.ToString()
                                           }).ToList();
            ViewBag.vl1 = value1;
            return View();
        }
        [HttpPost]
        public ActionResult EmployeeAdd(Employee employee)
        {
            if (Request.Files.Count > 0)
            {
                string fileName = Path.GetFileName(Request.Files[0].FileName);
                string extension = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Image/" + fileName + extension;
                Request.Files[0].SaveAs(Server.MapPath(path));
                employee.EmployeeImage = "/Image/" + fileName + extension;
            }
            c.Employees.Add(employee);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult EmployeeBring(int id)
        {
            List<SelectListItem> value1 = (from x in c.Departments.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmentName,
                                               Value = x.DepartmentID.ToString()
                                           }).ToList();
            ViewBag.vl1 = value1;

            var emp = c.Employees.Find(id);
            return View("EmployeeBring", emp);
        }
        public ActionResult EmployeeEdit(Employee employee)
        {
            if (Request.Files.Count > 0)
            {
                string fileName = Path.GetFileName(Request.Files[0].FileName);
                string extension = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Image/" + fileName + extension;
                Request.Files[0].SaveAs(Server.MapPath(path));
                employee.EmployeeImage = "/Image/" + fileName + extension;
            }
            var emp = c.Employees.Find(employee.EmployeeID);
            emp.EmployeeName = employee.EmployeeName;
            emp.EmployeeSurname = employee.EmployeeSurname;
            emp.EmployeeImage = employee.EmployeeImage;
            emp.EmployeeDetail = employee.EmployeeDetail;
            emp.EmployeeAddress = employee.EmployeeAddress;
            emp.EmployeePhone = employee.EmployeePhone;
            emp.Departmentid = employee.Departmentid;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult EmployeeList()
        {
            var query = c.Employees.ToList();
            return View(query);
        }
    }
}