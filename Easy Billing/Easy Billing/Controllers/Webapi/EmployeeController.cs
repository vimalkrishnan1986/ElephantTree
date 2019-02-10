using EasyBilling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace EasyBilling.Controllers.Webapi
{
    [RoutePrefix("api/employees")]
    public class EmployeeController : ApiController
    {
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAll()
        {
            using (EasyBillingEntities context = new EasyBillingEntities())
            {
                return await Task.FromResult(Ok(context.Employees.ToList()));
            }
        }

        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> Save([FromBody] Employee employee)
        {
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
                    return Ok();
                }
                employee.Token_number = Guid.NewGuid().ToString();
                
                    db.Employees.Add(employee);
                    await db.SaveChangesAsync();
                    return Created("", employee);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetById([FromUri] string id)
        {
            using (EasyBillingEntities db = new EasyBillingEntities())
            {
                return await Task.FromResult(Ok(db.Employees.Where(z => z.Token_number == id).Distinct().FirstOrDefault()));
            }
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete([FromUri]string id)
        {
            using (EasyBillingEntities db = new EasyBillingEntities())
            {
                Employee EmpforDeletion = db.Employees.Where(z => z.Token_number == id).Distinct().FirstOrDefault();
                if (EmpforDeletion != null)
                {
                    db.Employees.Remove(EmpforDeletion);
                    await db.SaveChangesAsync();
                    ModelState.Clear();
                    return Ok();
                }
                return BadRequest($"Invalid token number {id}");
            }
        }
    }
}