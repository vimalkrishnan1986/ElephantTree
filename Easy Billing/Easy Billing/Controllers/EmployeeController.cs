﻿using EasyBilling.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EasyBilling.Controllers
{
    [Route("employees")]
    public class EmployeeController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        [Route("")]
        public async Task<ActionResult> GetAll()
        {
            using (EasyBillingEntities context = new EasyBillingEntities())
            {
                return await Task.FromResult(Ok(context.Employees.ToList()));
            }
        }


        [HttpPut]
        [Route("save")]
        public async Task<ActionResult> Save([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            using (EasyBillingEntities db = new EasyBillingEntities())
            {
                Employee Employeeforupdate = db.Employees.Where(z => z.Token_number == employee.Token_number)
                    .Distinct().FirstOrDefault();
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
                    return StatusCode((int)HttpStatusCode.OK);
                }
                employee.Token_number = Guid.NewGuid().ToString();
                if (ModelState.IsValid)
                {
                    db.Employees.Add(employee);
                    await db.SaveChangesAsync();
                    return StatusCode((int)HttpStatusCode.Created);
                }
            }
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [HttpGet]
        [Route("details/{id}")]
        public async Task<ActionResult> Details([FromRoute] string id)
        {
            using (EasyBillingEntities db = new EasyBillingEntities())
            {
                return await Task.FromResult(Ok(db.Employees.Where(z => z.Token_number == id).Distinct().FirstOrDefault()));
            }
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete([FromRoute]string id)
        {
            using (EasyBillingEntities db = new EasyBillingEntities())
            {
                Employee EmpforDeletion = db.Employees.Where(z => z.Token_number == id).Distinct().FirstOrDefault();
                if (EmpforDeletion != null)
                {
                    db.Employees.Remove(EmpforDeletion);
                    await db.SaveChangesAsync();
                    ModelState.Clear();
                    return StatusCode((int)HttpStatusCode.Accepted);
                }
                return BadRequest($"Invalid token number {id}");
            }
        }
    }
}