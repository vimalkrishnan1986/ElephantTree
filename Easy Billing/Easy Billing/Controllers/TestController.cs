using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EasyBilling.Models;
using PagedList;

namespace EasyBilling.Controllers
{
    public class TestController : MybaseController
    {
        private EasyBillingEntities db = new EasyBillingEntities();
        // GET: Test
        public ActionResult Index()
        {
       
            
            return View();
        }
   



    }
}