using EasyBilling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace EasyBilling.Controllers
{
    public class ProductController : MybaseController
    {
        // GET: Product
        public ActionResult Index()
        {
            IEnumerable<Product> product = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8087/api/");
                //HTTP GET
                var responseTask = client.GetAsync("http://localhost:8087/api/Product/GetAllProducts");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Product>>();
                    readTask.Wait();

                    product = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    product = Enumerable.Empty<Product>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(product);
        }
        public ActionResult Create()
        {
            IEnumerable<Tax_Group> txgrp = null;
            using (var client = new HttpClient())
            {
                var responseTask = client.GetAsync("http://localhost:8087/api/TaxGroup/GetAllUserTaxGroups");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Tax_Group>>();
                    readTask.Wait();

                    txgrp = readTask.Result;
                    ViewBag.states = txgrp;
                }
                else //web api sent error response 
                {


                    txgrp = Enumerable.Empty<Tax_Group>();
                    ViewBag.states = txgrp;
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult create(Product product)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:8087/api/Product");

                //HTTP POST
                var postTask = client.PostAsJsonAsync("http://localhost:8087/api/Product/PostNewProduct", product);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(product);
        }

        public ActionResult Edit(string id)
        {
            Product product = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8087/api/GetMerchant");
                //HTTP GET
                var responseTask = client.GetAsync("http://localhost:8087/api/Product/GetAllProducts?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Product>();
                    readTask.Wait();

                    product = readTask.Result;
                }
            }
            //IList<Tax_Group> taxexist = null;

            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("http://localhost:8087/api/GetMerchant");
                //HTTP GET
                //var responseTask = client.GetAsync("http://localhost:8087/api/TaxGroup/GetAllTaxGroupforedit?id=" + (product.GL_CODE).ToString());
                //responseTask.Wait();

                //var result = responseTask.Result;
                //if (result.IsSuccessStatusCode)
                //{
                //    var readTask = result.Content.ReadAsAsync<IList<Tax_Group>>();
                //    readTask.Wait();

                //    taxexist = readTask.Result;


                //    ViewBag.states = taxexist;

                //}
            //}
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8087/api/product");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Product>("http://localhost:8087/api/Product/PutProducts", product);
                putTask.Wait();


                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(product);
        }
        public ActionResult Delete(string id)
        {
            Product product = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8087/api/GetMerchant");
                //HTTP GET
                var responseTask = client.GetAsync("http://localhost:8087/api/Product/GetAllProducts?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Product>();
                    readTask.Wait();

                    product = readTask.Result;
                }
            }

            return View(product);
        }
        [HttpPost]
        public ActionResult Delete(string id, Product product)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8087/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("http://localhost:8087/api/Product/Delete?id=" + id.ToString());
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
            Product product = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8087/api/GetMerchant");
                //HTTP GET
                var responseTask = client.GetAsync("http://localhost:8087/api/Product/GetAllProducts?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Product>();
                    readTask.Wait();

                    product = readTask.Result;
                }
            }

            return View(product);
        }
        [Authorize]
        public ActionResult ProductList()
        {
            using (EasyBillingEntities db = new EasyBillingEntities())
            {
                return View(db.Other_Products.Distinct().ToList());
            }
        }
        [Authorize]
        public ActionResult CreateProduct()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateProduct(Other_Product product)
        {
            using (EasyBillingEntities db = new EasyBillingEntities())
            {
                product.Token_number = Guid.NewGuid().ToString();
                db.Other_Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("ProductList");
            }
        }

    }

}