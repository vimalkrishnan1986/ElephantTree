using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using EasyBilling.Models;

namespace EasyBilling.Controllers
{

    public class EmployeeController : Controller
    {
        //for  thorwing the view
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

    }
}