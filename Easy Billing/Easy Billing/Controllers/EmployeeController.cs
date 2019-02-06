using EasyBilling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EasyBilling.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            using (EasyBillingEntities db = new EasyBillingEntities())
            {
                return View(db.Employees.Distinct().ToList());
            }

        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Employee employee)
        {
            using (EasyBillingEntities db = new EasyBillingEntities())
            {
                employee.Token_number = Guid.NewGuid().ToString();
                if (ModelState.IsValid)
                {
                    db.Employees.Add(employee);
                    await db.SaveChangesAsync();
                    ModelState.Clear();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(employee);
                }

            }
        }
        public ActionResult Details(string id)
        {
            using (EasyBillingEntities db = new EasyBillingEntities())
            {
                return View(db.Employees.Where(z => z.Token_number == id).Distinct().FirstOrDefault());
            }
        }
        public ActionResult Update(string id)
        {
            using (EasyBillingEntities db = new EasyBillingEntities())
            {
                return View(db.Employees.Where(z => z.Token_number == id).Distinct().FirstOrDefault());
            }
        }
        [HttpPost]
        public async Task<ActionResult> Update(Employee employee)
        {
            using (EasyBillingEntities db = new EasyBillingEntities())
            {
                Employee Employeeforupdate = db.Employees.Where(z => z.Token_number == employee.Token_number).Distinct().FirstOrDefault();
                if (Employeeforupdate != null)
                {
                    Employeeforupdate.Contact_number = employee.Contact_number;
                    Employeeforupdate.Designation = employee.Designation;
                    Employeeforupdate.Email_id = employee.Email_id;
                    Employeeforupdate.Employee_Id = employee.Employee_Id;
                    Employeeforupdate.Employee_name = employee.Employee_name;
                    Employeeforupdate.Joining_date = employee.Joining_date;
                    Employeeforupdate.Leaving_date = employee.Leaving_date;
                    Employeeforupdate.login_required = employee.login_required;
                    Employeeforupdate.Salary = employee.Salary;
                  

                    await db.SaveChangesAsync();
                    ModelState.Clear();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(employee);
                }

            }
        }
        public ActionResult Delete(string id)
        {
            using (EasyBillingEntities db = new EasyBillingEntities())
            {
                return View(db.Employees.Where(z => z.Token_number == id).Distinct().FirstOrDefault());
            }
        }
        [HttpPost]
        public async Task<ActionResult> Delete(Employee employee)
        {
            using (EasyBillingEntities db = new EasyBillingEntities())
            {
                Employee EmpforDeletion = db.Employees.Where(z => z.Token_number == employee.Token_number).Distinct().FirstOrDefault();
                if (EmpforDeletion != null)
                {
                    db.Employees.Remove(EmpforDeletion);
                    await db.SaveChangesAsync();
                    ModelState.Clear();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(employee);
                }
            }
        }
    }
}