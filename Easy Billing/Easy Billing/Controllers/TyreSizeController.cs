using EasyBilling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EasyBilling.Controllers
{
    public class TyreSizeController : Controller
    {
        // GET: TyreSize
        public ActionResult Index()
        {
            using (EasyBillingEntities db = new EasyBillingEntities())
            {
                return View(db.Tyre_sizes.Distinct().ToList());
            }

        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Tyre_size tyre_Size)
        {
            using (EasyBillingEntities db = new EasyBillingEntities())
            {
                tyre_Size.Token_number = Guid.NewGuid().ToString();
                if (ModelState.IsValid)
                {
                    db.Tyre_sizes.Add(tyre_Size);
                    await db.SaveChangesAsync();
                    ModelState.Clear();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(tyre_Size);
                }

            }
        }
        public ActionResult Details(string id)
        {
            using (EasyBillingEntities db = new EasyBillingEntities())
            {
                return View(db.Tyre_sizes.Where(z => z.Token_number == id).Distinct().FirstOrDefault());
            }
        }
        public ActionResult Update(string id)
        {
            using (EasyBillingEntities db = new EasyBillingEntities())
            {
                return View(db.Tyre_sizes.Where(z => z.Token_number == id).Distinct().FirstOrDefault());
            }
        }
        [HttpPost]
        public async Task<ActionResult> Update(Tyre_size tyre_Size)
        {
            using (EasyBillingEntities db = new EasyBillingEntities())
            {
                Tyre_size tyresizesforupdate = db.Tyre_sizes.Where(z => z.Token_number == tyre_Size.Token_number).Distinct().FirstOrDefault();
                if (tyresizesforupdate != null)
                {
                    tyresizesforupdate.Tyre_size1 = tyre_Size.Tyre_size1;
                    tyresizesforupdate.With_tube = tyre_Size.With_tube;

                     await db.SaveChangesAsync();
                    ModelState.Clear();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(tyre_Size);
                }

            }
        }
        public ActionResult Delete(string id)
        {
            using (EasyBillingEntities db = new EasyBillingEntities())
            {
                return View(db.Tyre_sizes.Where(z => z.Token_number == id).Distinct().FirstOrDefault());
            }
        }
        [HttpPost]
        public async Task<ActionResult> Delete(Tyre_size tyre_Size)
        {
            using (EasyBillingEntities db = new EasyBillingEntities())
            {
                Tyre_size tyresizeforDeletion = db.Tyre_sizes.Where(z => z.Token_number == tyre_Size.Token_number).Distinct().FirstOrDefault();
                if (tyresizeforDeletion == null )
                {
                    return View(tyre_Size);
                   
                }
                else
                {
                    db.Tyre_sizes.Remove(tyresizeforDeletion);
                    await db.SaveChangesAsync();
                    ModelState.Clear();
                    return RedirectToAction("Index");
                }
            }
        }
    }
}