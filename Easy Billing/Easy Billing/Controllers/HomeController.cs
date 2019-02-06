using EasyBilling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EasyBilling.Controllers
{
    public class HomeController : MybaseController
    {
        //[Authorize]
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        public ActionResult test()
        {
            return View();
        }
       // [Authorize]
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();

        }
        public ActionResult SignIn()
        {
            ViewBag.Title = "Sign In";

            return View();
        }

        [HttpPost]
        public ActionResult SignIn(Marchent_Account marchent, string ReturnUrl)
        {
            using ( var client = new HttpClient())
            {
                if (marchent.Email_Id == "souravganguly707@gmail.com" || marchent.Email_Id== "kannantyres@kannantyres.com")
                {
                    client.BaseAddress = new Uri("http://localhost:8087/api/Marchent");

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync("http://localhost:8087/api/Marchent/PostMerchantLogin", marchent);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask1 = result.Content.ReadAsAsync<Marchent_Account>();
                        readTask1.Wait();

                        var mrchnt = readTask1.Result;
                        //============================================== for license blocking for Kannan tyres======================================================================================
                        //NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                        //String sMacAddress = string.Empty;
                        //foreach (NetworkInterface adapter in nics)
                        //{
                        //    if (sMacAddress != "28F10E3842DB")// only return MAC Address from first card  
                        //    {
                        //        IPInterfaceProperties properties = adapter.GetIPProperties();
                        //        sMacAddress = adapter.GetPhysicalAddress().ToString();
                        //    }
                        //}

                        //string HostName = Dns.GetHostName();

                        //var license = "KADAMBARI-LC-" + sMacAddress + "-" + HostName;
                        //if (license != mrchnt.License)
                        //{

                        //    ModelState.AddModelError(string.Empty, "You have no license to use. Please contact to develper. Thank you");
                        //    return View(marchent);
                        //}
                        //else
                        //{
                        //============================================== // for license======================================================================================
                        int timeout = mrchnt.rememberme ? 60 : 5;
                            var ticket = new FormsAuthenticationTicket(mrchnt.Email_Id, mrchnt.rememberme, timeout);
                            string encrypted = FormsAuthentication.Encrypt(ticket);
                            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                            cookie.Expires = DateTime.Now.AddMinutes(timeout);
                            cookie.HttpOnly = true;

                            Response.Cookies.Add(cookie);
                            FormsAuthentication.SetAuthCookie(mrchnt.Email_Id, mrchnt.rememberme);
                            var t = User.Identity.IsAuthenticated;
                            if (Url.IsLocalUrl(ReturnUrl))
                            {
                                return Redirect(ReturnUrl);
                            }
                            else
                            {
                                return RedirectToAction("Index");

                            }

                        //}
                    }
                    else
                    {
                        ViewBag.Title = "Sign In";
                        ModelState.AddModelError(string.Empty, "Please check credentials with your e-mail.");
                        return View(marchent);
                    }
                }else
                {
                    HttpResponseMessage result = null;
                   
                        client.BaseAddress = new Uri("http://localhost:8087/api/Customer");

                        //HTTP POST
                        var postTask = client.PostAsJsonAsync("http://localhost:8087/api/Customer/PostCustomerLogin", marchent);
                        postTask.Wait();

                        result = postTask.Result;
                   
                    if (result.IsSuccessStatusCode)
                    {
                       
                            int timeout = marchent.rememberme ? 60 : 5;
                            var ticket = new FormsAuthenticationTicket(marchent.Email_Id, marchent.rememberme, timeout);
                            string encrypted = FormsAuthentication.Encrypt(ticket);
                            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                            cookie.Expires = DateTime.Now.AddMinutes(timeout);
                            cookie.HttpOnly = true;

                            Response.Cookies.Add(cookie);
                            FormsAuthentication.SetAuthCookie(marchent.Email_Id, marchent.rememberme);
                            var t = User.Identity.IsAuthenticated;
                            if (Url.IsLocalUrl(ReturnUrl))
                            {
                                return Redirect(ReturnUrl);
                            }
                            else
                            {
                                return RedirectToAction("Index");

                            }

                    }
                    else
                    {
                        ViewBag.Title = "Sign In";
                        ModelState.AddModelError(string.Empty, "Please check credentials with your e-mail.");
                        return View(marchent);
                    }
                }
            }
        }

        public ActionResult SignUp(string id)
        {
            if(id=="1")
            {
                ViewBag.succeed = "1";
            }
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
        public ActionResult SignUp(Customer customer)
        {
            using (var client = new HttpClient())
            {
                if (customer.New_customer == true)
                {
                    client.BaseAddress = new Uri("http://localhost:8087/api/Customer");

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync("http://localhost:8087/api/Customer/PostNewCustomer", customer);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {

                        return RedirectToAction("SignUp", new { id = "1" });
                    }
                    else
                    {
                        if (result.StatusCode == HttpStatusCode.Found)
                        {
                            ModelState.AddModelError(string.Empty, "Email id already exists.");
                        }else
                        {
                            ModelState.AddModelError(string.Empty, "something went wrong. Please try again.");
                        }
                    }
                }
                else
                {

                    client.BaseAddress = new Uri("http://localhost:8087/api/Customer");

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync("http://localhost:8087/api/Customer/PostNewUser", customer);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("SignIn");
                    }else
                    {
                        if(result.StatusCode==HttpStatusCode.Found)
                        {
                            ModelState.AddModelError(string.Empty, "Email id already exists.");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "something went wrong. Please try again.");
                        }
                    }
                }
               
            }

           

            return View(customer);
        }

        public ActionResult CustomerSignUp()
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
        public ActionResult CustomerSignUp(Marchent_Account marchent)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:8087/api/Marchent");

                //HTTP POST
                var postTask = client.PostAsJsonAsync("http://localhost:8087/api/Marchent/PostNewMerchant", marchent);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("SignIn");
                }
                else
                {
                    IEnumerable<State> state = null;

                    var responseTask1 = client.GetAsync("http://localhost:8087/api/State/GetAllStates");
                    responseTask1.Wait();

                    var result1 = responseTask1.Result;
                    if (result1.IsSuccessStatusCode)
                    {
                        var readTask1 = result1.Content.ReadAsAsync<IList<State>>();
                        readTask1.Wait();

                        state = readTask1.Result;
                        ViewBag.states = state;
                    }
                    else //web api sent error response 
                    {
                        state = Enumerable.Empty<State>();
                        ViewBag.states = state;
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                return View(marchent);
            }
        }

        [HttpPost]
        public ActionResult Signout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("SignIn");
        }

    }
}

