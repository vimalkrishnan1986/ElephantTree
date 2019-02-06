using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EasyBilling.Models;

namespace EasyBilling.Controllers
{
    public class VehiclesController : Controller
    {
        private EasyBillingEntities db = new EasyBillingEntities();

        // GET: Vehicles
        public async Task<ActionResult> Index()
        {
            return View(await db.Vehicles.ToListAsync());
        }

      
        // GET: Vehicles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Token_number,Vehicle_type,Vehicle_make")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                vehicle.Token_number = Guid.NewGuid().ToString();
                db.Vehicles.Add(vehicle);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(vehicle);
        }

       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
