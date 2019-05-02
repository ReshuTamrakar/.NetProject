using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]        
        public ActionResult create(Employee employee)
        {
            AttendanceEntities entities = new AttendanceEntities();
            employee.Days = 0;
            entities.Employees.Add(employee);
            entities.SaveChanges();
            return RedirectToAction("list");
        }
        [Route("/employee/list")]
        public ActionResult list()
        {
            AttendanceEntities entities = new AttendanceEntities();
            var model=entities.Employees.ToList();
            return View(model);
        }
      
        [Route("/employee/delete/{id}")]
        public ActionResult Delete(int id)
        {
            AttendanceEntities entities = new AttendanceEntities();

           var e= entities.Employees.FirstOrDefault(emp => emp.ID == id);
            entities.Employees.Remove(e);
            entities.SaveChanges();
            return RedirectToAction("list");
        }

        public ActionResult Edit(int id)
        {
            AttendanceEntities entities = new AttendanceEntities();
            var model= entities.Employees.FirstOrDefault(e => e.ID == id);
            return View(model);
        }
        [Route("/employee/update/{id}")]
        public ActionResult Update(int id, Employee employee)
        {
            AttendanceEntities entities = new AttendanceEntities();
            var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
            entity.FirstName = employee.FirstName;
            entity.LastName = employee.LastName;
            entity.Gender = employee.Gender;
            entity.Salary = employee.Salary;
            entities.Employees.AddOrUpdate(employee);
            entities.SaveChanges();
            return RedirectToAction("list");
        }

        public ActionResult Details (int id)
        {
            AttendanceEntities entities = new AttendanceEntities();
            var details= entities.Employees.FirstOrDefault(e => e.ID == id);
            return View(details);
        }

        public ActionResult Present(int id, Employee e)
        {
            AttendanceEntities entities = new AttendanceEntities();
            var entity = entities.Employees.FirstOrDefault(emp => emp.ID == id);
            entity.IsPresent = 1;
           // int day=(int)entity.Days;
            //int pay = entity.PaySalary;
            if (entity.IsPresent == 1)
            {
                entity.Days=entity.Days + 1;
                entity.PaySalary = (int)entity.Days * (int)entity.Salary/30 ;
            }
            entities.Employees.AddOrUpdate(entity);
            entities.SaveChanges();
            return RedirectToAction("list");
        }
    }
}