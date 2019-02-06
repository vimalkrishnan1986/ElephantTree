using EasyBilling.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace EasyBilling.Controllers
{
    public class DealerController : MybaseController
    {
        // GET: Dealer
        public ActionResult Index(int? page, string keyword)
        {
            IEnumerable<Dealer> dealer = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8087/api/Dealer/GetAllDealers");
                //HTTP GET
                var responseTask = client.GetAsync("http://localhost:8087/api/Dealer/GetAllDealers");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Dealer>>();
                    readTask.Wait();
                    dealer = readTask.Result;
                }
                else //web api sent error response 
                {
                    dealer = Enumerable.Empty<Dealer>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
                if (!string.IsNullOrEmpty(keyword))
                {
                    return View(dealer.Where(f => f.Dealer_code.Contains(keyword)).Distinct().ToList().ToPagedList(page ?? 1, 4));
                }
            }
            return View(dealer.ToPagedList(page ?? 1, 4));
        }
        public JsonResult GetDealers(string term)
        {
            using (EasyBillingEntities db = new EasyBillingEntities())
            {
                var dcmlempty = decimal.Parse("0.000");
                List<string> _Products_For_Sales = (from pdfs in db.Dealers
                                               
                                                    where pdfs.Dealer_code.Contains(term)
                                                    select pdfs.Dealer_code).Distinct().ToList();
                if (_Products_For_Sales.Count == 0)
                {
                    _Products_For_Sales.Add("No Supplier is matched with this Code...");
                }
                return Json(_Products_For_Sales, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Create()
        {
            IEnumerable<State> state = null;
            using (var client = new HttpClient())
            {
                var responseTask = client.GetAsync("http://localhost:8087/api/State/GetAllStates");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<State>>();
                    readTask.Wait();

                    state = readTask.Result;
                    ViewBag.states = state;
                }
                else //web api sent error response 
                {


                    state = Enumerable.Empty<State>();
                    ViewBag.states = state;
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult create(Dealer dealer)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8087/api/Dealer/PostNewDealer");

                //HTTP POST
                var postTask = client.PostAsJsonAsync("http://localhost:8087/api/Dealer/PostNewDealer", dealer);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(dealer);
        }

        public ActionResult Edit(string id)
        {
            Dealer dealer = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8087/api/GetMerchant");
                //HTTP GET
                var responseTask = client.GetAsync("http://localhost:8087/api/Dealer/GetAllDealers?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Dealer>();
                    readTask.Wait();

                    dealer = readTask.Result;
                }
            }
            IList<State> stateexist = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8087/api/GetMerchant");
                //HTTP GET
                var responseTask = client.GetAsync("http://localhost:8087/api/State/GetAllStatesforedit?id=" + (dealer.State).ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<State>>();
                    readTask.Wait();

                    stateexist = readTask.Result;


                    ViewBag.states = stateexist;

                }
            }
            return View(dealer);
        }
        [HttpPost]
        public ActionResult Edit(Dealer dealer)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8087/api/Dealer");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Dealer>("http://localhost:8087/api/Dealer/PutDealers", dealer);
                putTask.Wait();


                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(dealer);
        }
        public ActionResult Delete(string id)
        {
            Dealer dealer = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8087/api/GetMerchant");
                //HTTP GET
                var responseTask = client.GetAsync("http://localhost:8087/api/Dealer/GetAllDealers?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Dealer>();
                    readTask.Wait();

                    dealer = readTask.Result;
                }
            }

            return View(dealer);
        }
        [HttpPost]
        public ActionResult Delete(string id, Dealer Dealer)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8087/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("http://localhost:8087/api/Dealer/Delete?id=" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }
        public ActionResult Details(string id)
        {
            Dealer dealer = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8087/api/GetMerchant");
                //HTTP GET
                var responseTask = client.GetAsync("http://localhost:8087/api/Dealer/GetAllDealers?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Dealer>();
                    readTask.Wait();

                    dealer = readTask.Result;
                }
            }

            return View(dealer);
        }
    }
}