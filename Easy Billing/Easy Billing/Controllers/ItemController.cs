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
using System.IO;
using Newtonsoft.Json;

namespace EasyBilling.Controllers
{
    public class ItemController : Controller
    {
        private EasyBillingEntities db = new EasyBillingEntities();

        // GET: Item
        public async Task<ActionResult> Index()
        {
            //return View(await db.Item_Tyres.ToListAsync());
           
           var _data = await db.Item_Tyres.ToListAsync();
            string json = JsonConvert.SerializeObject(_data.ToArray());
   
            json =  "{\"data\":" + json + "}";
            //write string to file
            var par=Server.MapPath("~/Json/sampledata.json");
             System.IO.File.WriteAllText(par, json);
            return View();
        }

   
        // GET: Item/Create
        public ActionResult Create()
        {
            Random rd = new Random();
           int id= rd.Next(1,99999999);
            ViewBag.itemId = id.ToString();
            return View();
        }

        // POST: Item/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
      
        public async Task<JsonResult> Create([Bind(Include = "Token_number,Item_Id,Tyre_make,Tyre_type,Tyre_feel,Company_name,Tyre_size,Vehicle_type,Description")] Item_Tyre item_Tyre)
        {
            if (ModelState.IsValid)
            {
                item_Tyre.Token_number = Guid.NewGuid().ToString();
                var lastId = (from a in db.Item_Tyres
                              orderby a.Item_Id descending
                              select a.Item_Id).Distinct().FirstOrDefault();
                if (!string.IsNullOrEmpty(lastId))
                {
                    var text = lastId;
                    var fstfr = text.Substring(0, 9);
                    var lstfr = text.Substring(text.Length - 8);
                    string newlstversn = (int.Parse(lstfr) + 100000001).ToString();
                    string fstfr1 = (newlstversn.Substring(newlstversn.Length - 8)).ToString();
                    item_Tyre.Item_Id = "TY" + item_Tyre.Company_name.Remove(3) + "TUB-" + fstfr1;
                }
                else
                    item_Tyre.Item_Id = "TY" + item_Tyre.Company_name.Remove(3) + "TUB-00000001";
               

                
                if (!string.IsNullOrEmpty(item_Tyre.Item_Id))
                {
                    db.Item_Tyres.Add(item_Tyre);
                    await db.SaveChangesAsync();
                    var _data = await db.Item_Tyres.ToListAsync();
                    string json = JsonConvert.SerializeObject(_data.ToArray());

                    json = "{\"data\":" + json + "}";
                    //write string to file
                    var par = Server.MapPath("~/Json/sampledata.json");
                    System.IO.File.WriteAllText(par, json);
                    return Json(item_Tyre);
                }
                else
                {
                    return Json(item_Tyre);
                }
                
            }

            return Json(item_Tyre);
        }

        public async Task<ActionResult> ItemTubeList()
        {
            return View(await db.Item_Tubes.ToListAsync());
        }


        // GET: Item/Create
        public ActionResult CreateItemTube()
        {
            Random rd = new Random();
            int id = rd.Next(1, 99999999);
            ViewBag.itemId = id.ToString();
            return View();
        }

        // POST: Item/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateItemTube([Bind(Include = "Token_number,Item_Id,Tube_size,Company_name,Vehicle_type,Description")] Item_Tube item_Tube)
        {
            if (ModelState.IsValid)
            {
                item_Tube.Token_number = Guid.NewGuid().ToString();
                if (!string.IsNullOrEmpty(item_Tube.Item_Id))
                {
                    db.Item_Tubes.Add(item_Tube);
                    await db.SaveChangesAsync();
                    return RedirectToAction("ItemTubeList");
                }
                else
                {
                    return View(item_Tube);
                }

            }

            return View(item_Tube);
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
