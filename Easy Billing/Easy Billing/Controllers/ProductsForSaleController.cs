using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using EasyBilling.Models;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Http;
using PagedList;
using System.Web;
using System.Data.Entity.Validation;

namespace EasyBilling.Controllers
{
    public class ProductsForSaleController : MybaseController
    {
       
        private EasyBillingEntities db = new EasyBillingEntities();

        // GET: ProductsForSale
        public async Task<ActionResult> Index(int? page, string keyword)
        {
            var products_For_Sales = db.Products_For_Sales.Include(p => p.Product).Distinct();
            //return View(await products_For_Sales.ToListAsync());
            var dcmlempty = decimal.Parse("0.000");
            //var products_For_Sales = (from pdfs in db.Products_For_Sales
            //                          join stk in db.Stocks on pdfs.Token_Number equals stk.Product_Token
            //                          where (stk.Pieces != 0 ) && pdfs.Approve == true

            //                          select pdfs).Distinct();

            ViewBag.productstks = await (from pdfs in db.Products_For_Sales
                                         join stk in db.Stocks on pdfs.Token_Number equals stk.Product_Token
                                         //where pdfs.Approve == true

                                         select stk).Distinct().ToListAsync();
            ViewBag.billno = null;
            var results = (from p in db.Products_For_Sales
                       
                          group p.Tyre_Size by p.Tyre_Size into g
                         
                          select new CategoryClass { Product_name = g.Key, Subcategory = g.Distinct().ToList() }).ToList();
            ViewBag.ctgry = results;
            if (!string.IsNullOrEmpty(keyword))
            {
                return View(products_For_Sales.Where(f => f.Product_name.Contains(keyword)).OrderByDescending(z=>z.Date).Distinct().ToList().ToPagedList(page ?? 1, 9));
            }
            return View(products_For_Sales.OrderByDescending(z => z.Date).Distinct().ToList().ToPagedList(page ?? 1, 9));
        }
        public JsonResult GetStudents(string term)
        {
         //   var dcmlempty = decimal.Parse("0.000");
            List<string> _Products_For_Sales = (from pdfs in db.Products_For_Sales
                                     join stk in db.Stocks on pdfs.Token_Number equals stk.Product_Token
                                      where (stk.Pieces != 0) /*&& pdfs.Approve == true*/
                                      && pdfs.Product_name.Contains(term)
                                      select pdfs.Product_name).Distinct().ToList();
            if (_Products_For_Sales.Count == 0)
            {
                _Products_For_Sales.Add("No tyre is matched with this name...");
            }
            return Json(_Products_For_Sales, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Getsubcat(string term)
        {
            List<string> _Products_For_Sales = (from p in db.Products_For_Sales
                          where p.Tyre_Size.Contains(term)
                                                select p.Tyre_Size).Distinct().ToList();
           
            if (_Products_For_Sales.Count == 0)
            {
                _Products_For_Sales.Add("No subcategory found. You can can create new one...");
            }
            return Json(_Products_For_Sales, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult Search(string keyword)
        {
            var dcmlempty = decimal.Parse("0.000");
            if (!string.IsNullOrEmpty(keyword))
            {
                
                var products = (from pdfs in db.Products_For_Sales
                              join stk in db.Stocks on pdfs.Token_Number equals stk.Product_Token
                              orderby pdfs.Date descending
                              where (stk.Pieces != 0) && pdfs.Approve == true
                              && pdfs.Product_name.Contains(keyword) 
                              select pdfs).Distinct().ToList();
                
                return PartialView("_Products", products);
            }else
            {
                
                var prdall = (from pdfs in db.Products_For_Sales
                                                    join stk in db.Stocks on pdfs.Token_Number equals stk.Product_Token
                              orderby pdfs.Date descending
                              where (stk.Pieces != 0 ) && pdfs.Approve == true
                                                   
                                                    select pdfs).Distinct().ToList();

                return PartialView("_Products", prdall);
            }
           
        }

        public PartialViewResult Filter(string key, int? page,string keysub)
        {
            var dcmlempty = decimal.Parse("0.000");
            if (!string.IsNullOrEmpty(key))
            {
                if (!string.IsNullOrEmpty(keysub))
                {
                    var products = (from pdfs in db.Products_For_Sales
                                    join stk in db.Stocks on pdfs.Token_Number equals stk.Product_Token
                                    orderby pdfs.Date descending
                                    where pdfs.Approve == true
                                    && pdfs.Product_name == key && pdfs.Tyre_Size==keysub
                                    select pdfs).Distinct().ToList().ToPagedList(page ?? 1, 9);
                    return PartialView("_CategoryProducts", products);
                }
                else
                {

                    var products = (from pdfs in db.Products_For_Sales
                                    join stk in db.Stocks on pdfs.Token_Number equals stk.Product_Token
                                    orderby pdfs.Date descending
                                    where pdfs.Approve == true
                                    && pdfs.Product_name == key
                                    select pdfs).Distinct().ToList().ToPagedList(page ?? 1, 9);
                    return PartialView("_CategoryProducts", products);
                }
                
            }
            else
            {

                var prdall = (from pdfs in db.Products_For_Sales
                              join stk in db.Stocks on pdfs.Token_Number equals stk.Product_Token
                              orderby pdfs.Date descending
                              where pdfs.Approve == true

                              select pdfs).Distinct().ToList().ToPagedList(page ?? 1, 9);

                return PartialView("_CategoryProducts", prdall);
            }

        }
        public PartialViewResult FilterForBilling(string key, int? page, string keysub)
        {
            var dcmlempty = decimal.Parse("0.000");
            if (!string.IsNullOrEmpty(key))
            {
                if (!string.IsNullOrEmpty(keysub))
                {
                    var products = (from pdfs in db.Products_For_Sales
                                    join stk in db.Stocks on pdfs.Token_Number equals stk.Product_Token
                                    orderby pdfs.Date descending
                                    where pdfs.Approve == true
                                    && pdfs.Product_name == key && pdfs.Tyre_Size == keysub
                                    select pdfs).Distinct().ToList().ToPagedList(page ?? 1, 9);
                    return PartialView("_BillingCategoryProducts", products);
                }
                else
                {

                    var products = (from pdfs in db.Products_For_Sales
                                    join stk in db.Stocks on pdfs.Token_Number equals stk.Product_Token
                                    orderby pdfs.Date descending
                                    where pdfs.Approve == true
                                    && pdfs.Product_name == key
                                    select pdfs).Distinct().ToList().ToPagedList(page ?? 1, 9);
                    return PartialView("_BillingCategoryProducts", products);
                }

            }
            else
            {

                var prdall = (from pdfs in db.Products_For_Sales
                              join stk in db.Stocks on pdfs.Token_Number equals stk.Product_Token
                              orderby pdfs.Date descending
                              where pdfs.Approve == true

                              select pdfs).Distinct().ToList().ToPagedList(page ?? 1, 9);

                return PartialView("_BillingCategoryProducts", prdall);
            }

        }
        //[Authorize]

        public async Task<ActionResult> Billing(int? page, string keyword)
        {
            var dcmlempty = decimal.Parse("0.000");
            var products_For_Sales = (from pdfs in db.Products_For_Sales
                           join stk in db.Stocks on pdfs.Token_Number equals stk.Product_Token
                                     
                                      where (stk.Pieces != 0 ) && pdfs.Approve == true

                            select pdfs).Distinct();
            ViewBag.productstks = await (from pdfs in db.Products_For_Sales
                                         join stk in db.Stocks on pdfs.Token_Number equals stk.Product_Token
                                         orderby pdfs.Date descending
                                         where (stk.Pieces != 0 ) && pdfs.Approve == true

                                         select stk).Distinct().ToListAsync();
            ViewBag.billno = null;
            var results = (from p in db.Products_For_Sales

                           group p.Tyre_Size by p.Product_name into g

                           select new CategoryClass { Product_name = g.Key, Subcategory = g.Distinct().ToList() }).ToList();
            ViewBag.ctgry = results;
            if (!string.IsNullOrEmpty(keyword))
            {
                return View(products_For_Sales.OrderByDescending(z=>z.Date).Where(f=>f.Product_name.Contains(keyword)).Distinct().ToList().ToPagedList(page ?? 1, 9));
            }
            return View( products_For_Sales.OrderByDescending(z => z.Date).Distinct().ToList().ToPagedList(page??1,9));
        }

        // GET: ProductsForSale/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products_For_Sale products_For_Sale = await db.Products_For_Sales.FindAsync(id);
            if (products_For_Sale == null)
            {
                return HttpNotFound();
            }
            var chk = await db.Stocks.Where(z => z.Product_Token == id).Select(x => new { x.Pieces }).Distinct().FirstOrDefaultAsync();
            if ( chk.Pieces == int.Parse("0"))
            {
                ViewBag.stkin = "0";

            }
            else
            {
                ViewBag.stkin = chk.Pieces.ToString();
               
            }
            return View(products_For_Sale);
        }
    
    public JsonResult GetProducts(string id)
        {

            var products = db.Products.Select(x => new
            {
                Tax_rate = x.Tax_rate,
                HSN_SAC_Code = x.HSN_SAC_Code,
                x.Token_Number

            }).Where(z => z.Token_Number == id).FirstOrDefault();
            return Json(products, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProductDetails()
        {
            return View();
        }
        public ActionResult RateUpdate()
        {
            using (EasyBillingEntities db = new EasyBillingEntities())
            {
                ViewBag.tyresize = db.Tyre_sizes.ToList();
                return View(db.Products_For_Sales.Where(z => z.Approve == true).ToList());
            }
        }
        [HttpPost]
        public ActionResult RateUpdate(Products_For_Sale products_For_Sale)
        {
            using (EasyBillingEntities db = new EasyBillingEntities())
            {
                ViewBag.tyresize = db.Tyre_sizes.ToList();
                return View(db.Products_For_Sales.Where(z => z.Approve == true).ToList());
            }
        }
        public ActionResult awatingapproval()
        {
            using (EasyBillingEntities db = new EasyBillingEntities())
            {
                ViewBag.tyresize = db.Tyre_sizes.ToList();
                return View(db.Products_For_Sales.Where(z=>z.Approve==false).ToList());
            }
        }
        public async Task<ActionResult> Approve(string id)
        {
            using (EasyBillingEntities db = new EasyBillingEntities())
            {
                Products_For_Sale products_For_Sale = db.Products_For_Sales.Where(z => z.Token_Number == id).FirstOrDefault();
                products_For_Sale.Approve = true;
                bool chk = db.Stocks.Where(a => a.Product_Token == id).Any();
                if (chk == true)
                {
                    Stock stk = db.Stocks.Where(a => a.Product_Token == products_For_Sale.Token_Number).FirstOrDefault();
                    //stk.Date = DateTime.Now.Date;
                    stk.Remodify_Date = DateTime.Now.Date;
                    products_For_Sale.Approve_date = DateTime.Now.Date;
                    stk.Pieces = stk.Pieces + (int)products_For_Sale.StockIn;



                    stk.Product_Token = products_For_Sale.Token_Number;


                }
                else
                {
                    
                    Stock stk = new Stock();
                    stk.Date = DateTime.Now.Date;
                    stk.Remodify_Date = DateTime.Now.Date;
                    products_For_Sale.Approve_date = DateTime.Now.Date;
                    stk.Pieces = (int)products_For_Sale.Pieces;
                    
                    stk.Product_Token = products_For_Sale.Token_Number;
                    stk.Remodify_Date = DateTime.Now.Date;
                    stk.Remodify_pcs = 99;
                    stk.CGST = products_For_Sale.CGST;
                    stk.SGST = products_For_Sale.SGST;
                    db.Stocks.Add(stk);
                }

               await db.SaveChangesAsync();


                return RedirectToAction("awatingapproval");
            }
        }
        public ActionResult Reject(string id)
        {
            using (EasyBillingEntities db = new EasyBillingEntities())
            {
                Products_For_Sale products_For_Sale = db.Products_For_Sales.Where(z => z.Token_Number == id).FirstOrDefault();
                products_For_Sale.Approve = false;
               
                db.SaveChangesAsync();


                return RedirectToAction("awatingapproval");
            }
        }
        // GET: ProductsForSale/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Product_Token = new SelectList(db.Products, "Token_Number", "Product_Code");
            ViewBag.prdct = db.Products.ToList();
            ViewBag.tyresize = db.Tyre_sizes.ToList();
            ViewBag.dlr = db.Dealers.Distinct().ToList();
            ViewBag.itmtyres = db.Item_Tyres.Distinct().ToList();
            using (var client = new HttpClient())
            {
               
                var responseTask1 = client.GetAsync("http://localhost:8087/api/Dealer/GetAllDealers");
                responseTask1.Wait();
                
                var purchlastid = client.GetAsync("http://localhost:8087/api/Purchase/GetPurchaselastId");
                purchlastid.Wait();

             
                var result = responseTask1.Result;
               
                var lstid = purchlastid.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask1 = result.Content.ReadAsAsync<IList<Dealer>>();
                    readTask1.Wait();
                    ViewBag.dlr = readTask1.Result;
                    
                }
                else //web api sent error response 
                {
                    ViewBag.dlr = Enumerable.Empty<Dealer>();
                   
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
                if (lstid.IsSuccessStatusCode)
                {
                    var readTask3 = lstid.Content.ReadAsAsync<Purchase_Invoice>();
                    readTask3.Wait();
                    var lst = readTask3.Result;
                    var text = lst.Purchase_invoice_number;
                    var fstfr = text.Substring(0, 4);
                    var lstfr = text.Substring(text.Length - 7);
                    string newlstversn = (int.Parse(lstfr) + 10000001).ToString();
                    string fstfr1 = (newlstversn.Substring(newlstversn.Length - 7)).ToString();
                    String totalvrsn = fstfr + fstfr1;
                    ViewBag.lstid = totalvrsn;
                }
                else //web api sent error response 
                {
                    ViewBag.lstid = "PRCH0000001";
                }
            }
                return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create(Products_For_Sale products_For_Sale)
        {
            try { 
            if (ModelState.ContainsKey("products_For_Sale.Date"))
                ModelState["products_For_Sale.Date"].Errors.Clear();


            if (ModelState.IsValid)
            {
                products_For_Sale.Product_Token = products_For_Sale.Product_name;
                products_For_Sale.Date = DateTime.Now.Date;
                products_For_Sale.Token_Number = Guid.NewGuid().ToString();

                products_For_Sale.Administrator_Token_number = (from mrchnt in db.Marchent_Accounts
                                                                where mrchnt.Email_Id == User.Identity.Name
                                                                select mrchnt.Token_number).Distinct().FirstOrDefault();
                products_For_Sale.Approve = false;
                    var supplierdata = (from sup in db.Dealers
                                        where sup.Token_number == products_For_Sale.Supplier_token
                                        select sup.Name).FirstOrDefault();
                    if(!string.IsNullOrEmpty(supplierdata))
                    {
                        products_For_Sale.Supplier_name = supplierdata;
                    }
                var productdata = (from prdct in db.Products
                                   where prdct.Token_Number == products_For_Sale.Product_name
                                   select new { prdct.Product_name, prdct.Description, prdct.GL_CODE, prdct.Product_Code }).Distinct().FirstOrDefault();
                if (productdata != null)
                {
                    products_For_Sale.Product_name = productdata.Product_name;

                    db.Products_For_Sales.Add(products_For_Sale);

                        //bool chk = db.Stocks.Where(a => a.Product_Token == products_For_Sale.Token_Number).Any();
                        //if (chk == true)
                        //{
                        //    Stock stk = db.Stocks.Where(a => a.Product_Token == products_For_Sale.Token_Number).FirstOrDefault();
                        //    stk.Date = DateTime.Now.Date;

                        //    stk.Pieces = stk.Pieces + (int)products_For_Sale.StockIn;



                        //    stk.Product_Token = products_For_Sale.Token_Number;


                        //}
                        //else
                        //{

                        //    Stock stk = new Stock();
                        //    stk.Date = DateTime.Now.Date;

                        //    stk.Pieces = (int)products_For_Sale.Pieces;


                        //    stk.Product_Token = products_For_Sale.Token_Number;
                        //    stk.Remodify_Date = DateTime.Now.Date;
                        //    stk.Remodify_pcs = 99;
                        //    stk.CGST = products_For_Sale.CGST;
                        //    stk.SGST = products_For_Sale.SGST;
                        //    db.Stocks.Add(stk);
                        //}
                       
                        await db.SaveChangesAsync();

                }
                else
                {
                    ViewBag.prdct = db.Products.ToList();
                    ViewBag.Product_Token = new SelectList(db.Products, "Token_Number", "Product_Code", products_For_Sale.Product_Token);
                    ModelState.AddModelError(string.Empty, "Product doesn't exists. Please check your product");

                    return View(products_For_Sale);
                }
            }
            else
            {
                return View(products_For_Sale);
            }


            return RedirectToAction("Index");
        }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return View(products_For_Sale);
            }

        }
        //public async Task<ActionResult> Create( IList<Products_For_Sale> products_For_SaleAsList, Products_For_Sale products_For_SaleAsList1, Purchase_Master purchase, HttpPostedFileBase Image)
        //{
        //    Image = Request.Files["Image"];
        //    if (purchase!=null)
        //        {
        //        using (var client = new HttpClient())
        //        {

        //            client.BaseAddress = new Uri("http://localhost:8087/api/Purchase");

        //            //HTTP POST
        //            var postTask = client.PostAsJsonAsync("http://localhost:8087/api/Purchase/PostNewMerchant?BussinessId="+User.Identity.Name, purchase);
        //            postTask.Wait();

        //            var result = postTask.Result;

        //            if (!result.IsSuccessStatusCode)
        //            {
        //                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
        //                return new JsonResult { Data = new { status = false } };
        //            }

        //        }
        //    }
        //    int count = 0;
        //    foreach (var products_For_Sale in products_For_SaleAsList)
        //    {

        //        if (ModelState.ContainsKey("products_For_SaleAsList[" + count + "].Date"))
        //            ModelState["products_For_SaleAsList[" + count + "].Date"].Errors.Clear();
        //        count++;
        //    }
        //        foreach (var products_For_Sale in products_For_SaleAsList)
        //    {

        //        if (ModelState.IsValid)
        //        {
        //            products_For_Sale.Product_Token = products_For_Sale.Product_name;
        //            products_For_Sale.Date = DateTime.Now.Date;
        //            products_For_Sale.Token_Number = Guid.NewGuid().ToString();

        //            products_For_Sale.Administrator_Token_number = (from mrchnt in db.Marchent_Accounts
        //                                                       where mrchnt.Email_Id == User.Identity.Name
        //                                                       select mrchnt.Token_number).Distinct().FirstOrDefault();
        //            products_For_Sale.Approve = false;

        //            var productdata = (from prdct in db.Products
        //                               where prdct.Token_Number == products_For_Sale.Product_name
        //                               select new { prdct.Product_name, prdct.Description, prdct.GL_CODE, prdct.Product_Code }).Distinct().FirstOrDefault();
        //            if (productdata != null)
        //            {
        //                products_For_Sale.Product_name = productdata.Product_name;

        //                db.Products_For_Sales.Add(products_For_Sale);

        //                bool chk = db.Stocks.Where(a => a.Product_Token == products_For_Sale.Token_Number).Any();
        //                if (chk == true)
        //                {
        //                    Stock stk = db.Stocks.Where(a => a.Product_Token == products_For_Sale.Token_Number).FirstOrDefault();
        //                    stk.Date = DateTime.Now.Date;

        //                        stk.Pieces = stk.Pieces + (int)products_For_Sale.StockIn;



        //                    stk.Product_Token = products_For_Sale.Token_Number;


        //                }
        //                else
        //                {

        //                    Stock stk = new Stock();
        //                    stk.Date = DateTime.Now.Date;

        //                        stk.Pieces = (int)products_For_Sale.StockIn;


        //                    stk.Product_Token = products_For_Sale.Token_Number;
        //                    db.Stocks.Add(stk);


        //                }
        //                await db.SaveChangesAsync();

        //            }
        //            else
        //            {
        //                ViewBag.prdct = db.Products.ToList();
        //                ViewBag.Product_Token = new SelectList(db.Products, "Token_Number", "Product_Code", products_For_Sale.Product_Token);
        //                ModelState.AddModelError(string.Empty, "Product doesn't exists. Please check your product");
        //                return new JsonResult { Data = new { status = false, error = "Product doesn't exists. Please check your product" } };
        //            }
        //        }
        //        else
        //        {
        //            return new JsonResult { Data = new { status = false } };
        //        }

        //    }
        //    return new JsonResult { Data = new { status = true } };
        //}
        //public async Task<ActionResult> Create([Bind(Include = "Token_Number,Name,Product_Token,Product_Code,Product_name,Description,HSN_SAC_Code,Tax_rate,GL_CODE,Sell_On,Pieces,Weight,Unit,Amount,Amout_after_tax,Date,Discount,Total,Single_product_not_combo_multi_,Approve,Marchent_Token_number,StockIn")] Products_For_Sale products_For_Sale)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        products_For_Sale.Product_Token = products_For_Sale.Product_name;

        //        products_For_Sale.Token_Number = Guid.NewGuid().ToString();
        //        products_For_Sale.Marchent_Token_number = (from mrchnt in db.Marchent_Accounts
        //                                                   where mrchnt.Email_Id == User.Identity.Name
        //                                                   select mrchnt.Token_number).Distinct().FirstOrDefault();
        //        products_For_Sale.Approve = true;
        //        products_For_Sale.Single_product_not_combo_multi_ = true;
        //        var productdata = (from prdct in db.Products
        //                           where prdct.Token_Number == products_For_Sale.Product_name
        //                           select new { prdct.Product_name, prdct.Description, prdct.GL_CODE, prdct.Product_Code }).Distinct().FirstOrDefault();
        //        if (productdata != null)
        //        {
        //            products_For_Sale.Product_name = productdata.Product_name;
        //            products_For_Sale.Product_Code = productdata.Product_Code;
        //            if (products_For_Sale.Description == null)
        //            {
        //                products_For_Sale.Description = productdata.Description;
        //            }
        //            products_For_Sale.GL_CODE = productdata.GL_CODE;
        //            db.Products_For_Sales.Add(products_For_Sale);

        //            bool chk = db.Stocks.Where(a => a.Product_Token == products_For_Sale.Token_Number).Any();
        //            if (chk == true)
        //            {
        //                Stock stk = db.Stocks.Where(a => a.Product_Token == products_For_Sale.Token_Number).FirstOrDefault();
        //                stk.Date = DateTime.Now.Date;
        //                if(products_For_Sale.Sell_On.ToLower()=="pieces" || products_For_Sale.Sell_On.ToLower() == "plate")
        //                {
        //                    stk.Pieces = stk.Pieces + (int)products_For_Sale.StockIn;

        //                }
        //                else
        //                {
        //                    stk.Quantity = stk.Quantity + products_For_Sale.StockIn;
        //                }

        //                stk.Product_Token = products_For_Sale.Token_Number;


        //            }
        //            else
        //            {

        //                Stock stk = new Stock();
        //                stk.Date = DateTime.Now.Date;
        //                if (products_For_Sale.Sell_On.ToLower() == "pieces" || products_For_Sale.Sell_On.ToLower() == "plate")
        //                {
        //                    stk.Pieces = (int)products_For_Sale.StockIn;
        //                    stk.Quantity = decimal.Parse("0.000");
        //                }
        //                else
        //                {
        //                    stk.Quantity = products_For_Sale.StockIn;
        //                    stk.Pieces = 0;
        //                }

        //                stk.Product_Token = products_For_Sale.Token_Number;
        //                db.Stocks.Add(stk);


        //            }
        //            await db.SaveChangesAsync();
        //            return RedirectToAction("Create");
        //        }
        //        else
        //        {
        //            ViewBag.prdct = db.Products.ToList();
        //            ViewBag.Product_Token = new SelectList(db.Products, "Token_Number", "Product_Code", products_For_Sale.Product_Token);
        //            ModelState.AddModelError(string.Empty, "Product doesn't exists. Please check your product");
        //            return View(products_For_Sale);
        //        }
        //    }
        //    ViewBag.prdct = db.Products.ToList();
        //    ViewBag.Product_Token = new SelectList(db.Products, "Token_Number", "Product_Code", products_For_Sale.Product_Token);
        //    return View(products_For_Sale);
        //}

        // GET: ProductsForSale/Edit/5
        public async Task<ActionResult> Edit(string id)

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products_For_Sale products_For_Sale = await db.Products_For_Sales.FindAsync(id);
            if (products_For_Sale == null)
            {
                return HttpNotFound();
            }
           
                products_For_Sale.StockIn = db.Stocks.Where(a => a.Product_Token == products_For_Sale.Token_Number).Select(x => x.Pieces).Distinct().FirstOrDefault();
                products_For_Sale.StockIn = (int)products_For_Sale.StockIn;
           
            ViewBag.Product_Token = new SelectList(db.Products, "Token_Number", "Product_Code", products_For_Sale.Product_Token);
            ViewBag.prdct = db.Products.OrderByDescending(x => x.Token_Number == products_For_Sale.Product_Token).ToList();
            ViewBag.prdctselected = await (from a in db.Products
                                           where a.Token_Number == products_For_Sale.Product_Token

                                           select new { a.Token_Number, a.Product_name }).Distinct().FirstOrDefaultAsync();
            return View(products_For_Sale);
        }

        // POST: ProductsForSale/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Token_Number,Name,Product_Token,Product_Code,Product_name,Description,HSN_SAC_Code,Tax_rate,GL_CODE,Sell_On,Pieces,Weight,Unit,Amount,Amout_after_tax,Date,Discount,Total,Single_product_not_combo_multi_,Approve,Marchent_Token_number,StockIn,Subcategory,Image_path,Author_name,Publisher_name,Image")]  Products_For_Sale products_For_Sale, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                products_For_Sale.Product_Token = products_For_Sale.Product_name;
                bool chk = db.Stocks.Where(a => a.Product_Token == products_For_Sale.Token_Number).Any();
                if (chk == true)
                {
                    Stock stk = db.Stocks.Where(a => a.Product_Token == products_For_Sale.Token_Number).FirstOrDefault();

                    stk.Date = DateTime.Now.Date;
                  
                        stk.Pieces =(int) products_For_Sale.StockIn;
                   
                    stk.Product_Token = products_For_Sale.Token_Number;
                }
                else
                {
                    ViewBag.Product_Token = new SelectList(db.Products, "Token_Number", "Product_Code", products_For_Sale.Product_Token);
                    return View(products_For_Sale);
                }
         


                products_For_Sale.Administrator_Token_number = (from mrchnt in db.Marchent_Accounts
                                                           where mrchnt.Email_Id == User.Identity.Name
                                                           select mrchnt.Token_number).Distinct().FirstOrDefault();
                products_For_Sale.Approve = true;
          
                var productdata = (from prdct in db.Products
                                   where prdct.Token_Number == products_For_Sale.Product_name
                                   select new { prdct.Product_name, prdct.Description, prdct.GL_CODE, prdct.Product_Code }).Distinct().FirstOrDefault();
                if (productdata != null)
                {
                    products_For_Sale.Product_name = productdata.Product_name;
                 
                }
                db.Entry(products_For_Sale).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Product_Token = new SelectList(db.Products, "Token_Number", "Product_Code", products_For_Sale.Product_Token);
            return View(products_For_Sale);
        }

        // GET: ProductsForSale/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products_For_Sale products_For_Sale = await db.Products_For_Sales.FindAsync(id);
            if (products_For_Sale == null)
            {
                return HttpNotFound();
            }
            if (products_For_Sale.Approve == true)
            {
                products_For_Sale.Approve = false;
            }else
            {
                products_For_Sale.Approve = true;
            }
            //db.Products_For_Sales.Remove(products_For_Sale);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
           
        }

        // POST: ProductsForSale/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Products_For_Sale products_For_Sale = await db.Products_For_Sales.FindAsync(id);
            db.Products_For_Sales.Remove(products_For_Sale);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<ActionResult> Billing(FormCollection frm, int? page)
        {
            try
            {
                var results = (from p in db.Products_For_Sales

                               group p.Tyre_Size by p.Product_name into g

                               select new CategoryClass { Product_name = g.Key, Subcategory = g.Distinct().ToList() }).ToList();
                ViewBag.ctgry = results;
                Billing_Master billing = new Billing_Master();
                List<Billing_Detail> detail = new List<Billing_Detail>();

                Billing_Detail blldtl = new Billing_Detail();
                Stockout stkout = new Stockout();
                List<Stockout> stkoutlst = new List<Stockout>();

                Barcode_Master brcdmstr = new Barcode_Master();

                using (EasyBillingEntities dc = new EasyBillingEntities())
                {
                    bool ischk = dc.Billing_Masters.Any();
                    if (ischk == false)
                    {
                        billing.Billing_Number = "INV00000001";
                    }
                    else
                    {
                        var text = (from a in dc.Billing_Masters
                                    orderby a.Billing_Number descending
                                    select a.Billing_Number).FirstOrDefault();

                        var fstfr = text.Substring(0, 3);
                        var lstfr = text.Substring(text.Length - 8);
                        string newlstversn = (int.Parse(lstfr) + 100000001).ToString();
                        string fstfr1 = (newlstversn.Substring(newlstversn.Length - 8)).ToString();
                        String totalvrsn = fstfr + fstfr1;
                        billing.Billing_Number = totalvrsn;
                    }
                    var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

                    Random rd = new Random();

                    var chars1 = Enumerable.Range(0, 14)
               .Select(x => chars[rd.Next(0, chars.Length)]);
                    string barcode = new string(chars1.ToArray());

                    billing.Token_Number = (Guid.NewGuid()).ToString();
                    billing.Date = DateTime.Now.Date;
                    var mrchnttk = (from tkn in dc.Marchent_Accounts
                                    where tkn.Email_Id == User.Identity.Name
                                    select tkn.Token_number).Distinct().FirstOrDefault();
                    billing.Marchent_Token_number = mrchnttk;
                    var chkst = (from tkn in dc.Marchent_Accounts
                                 join st in dc.States on tkn.State_Code equals st.State_Code
                                 where tkn.Email_Id == User.Identity.Name
                                 select new { st.SGST, st.CGST, st.IGST, st.UTGST }).Distinct().FirstOrDefault();

                    billing.Total_tax = decimal.Parse("0.00");
                    billing.Rate_including_tax = decimal.Parse("0.00");
                    billing.Discount_percent = decimal.Parse("0.00");
                    billing.Total_discount = decimal.Parse("0.00");
                    billing.Total_amount = decimal.Parse("0.00");
                    billing.CGST = decimal.Parse("0.00");
                    billing.SGST = decimal.Parse("0.00");
                  
                    int b = 1;
                    int l = 0;
                    string customertoken = null;
                    foreach (var key in frm.Keys)
                    {
                        if (key.ToString() == "tokencust")
                        {
                            customertoken = frm["tokencust"];
                        }
                        else
                        {
                            if (l % 3 == 0)
                            {
                                var k = key.ToString();

                                int a = int.Parse(k.Substring(k.Length - 1, 1));
                                if (a == b)
                                { }
                                else
                                {
                                    b = a;
                                }
                                var Token = frm["token_" + a];
                                var Sellon = frm["sellon_" + a];
                                var Quant = int.Parse(frm["quantity_" + a]);

                                var stockitems = dc.Stocks.Where(x => x.Product_Token == Token).Distinct().FirstOrDefault();
                                var productforsaleitems = dc.Products_For_Sales.Where(x => x.Token_Number == Token).Distinct().FirstOrDefault();


                                if (stockitems != null)
                                {
                                        stockitems.Pieces = stockitems.Pieces - (productforsaleitems.Pieces * Quant);
                                   

                                    stockitems.Date = DateTime.Now.Date;

                                    if (stockitems.Pieces < 0)
                                    {


                                        var dcmlempty1 = decimal.Parse("0.000");
                                        var products_For_Sales1 = (from pdfs in db.Products_For_Sales
                                                                   join stk in db.Stocks on pdfs.Token_Number equals stk.Product_Token
                                                                   where (stk.Pieces != 0 ) && pdfs.Approve == true

                                                                   select pdfs).Distinct();
                                        ViewBag.productstks = await (from pdfs in db.Products_For_Sales
                                                                     join stk in db.Stocks on pdfs.Token_Number equals stk.Product_Token
                                                                     where (stk.Pieces != 0 ) && pdfs.Approve == true

                                                                     select stk).Distinct().ToListAsync();
                                        
                                        ViewBag.Error = "Please check your quantity";
                                        ModelState.AddModelError(string.Empty, "Please check your quantity");
                                        return View(products_For_Sales1.ToList().ToPagedList(page ?? 1, 9));
                                    }
                                    else
                                    {
                                        dc.SaveChanges();
                                    }
                                }

                                billing.Rate_including_tax = billing.Rate_including_tax + (productforsaleitems.Amout_after_tax*Quant);
                                billing.Total_discount = billing.Total_discount;
                                billing.Total_amount = billing.Total_amount + (productforsaleitems.Total*Quant);
                                billing.Total_tax = billing.Total_tax + ((productforsaleitems.Amout_after_tax*Quant) - (productforsaleitems.Selling_Price*Quant));
                                billing.Customer_Token_number = null;
                                blldtl.Billing_Token_number = billing.Token_Number;
                                blldtl.Billing_number = billing.Billing_Number;
                                blldtl.Date = billing.Date;
                                blldtl.Product_Token = Token;
                                blldtl.Pieces = productforsaleitems.Pieces * Quant;
                                
                                blldtl.Amount = productforsaleitems.Selling_Price * Quant;
                                blldtl.Taxable_amount = productforsaleitems.Pieces * productforsaleitems.Selling_Price * Quant;
                                blldtl.Tax = (productforsaleitems.Amout_after_tax * Quant) - (productforsaleitems.Selling_Price * Quant);
                                blldtl.Discount =decimal.Parse("0.00");
                                blldtl.Discount_percent = (((productforsaleitems.Amout_after_tax * Quant) - (productforsaleitems.Total * Quant)) * 100) / (productforsaleitems.Amout_after_tax * Quant);
                                blldtl.Sub_Total = productforsaleitems.Total * Quant;

                                stkout.Billing_Token_number = billing.Token_Number;
                                stkout.Billing_number = billing.Billing_Number;
                                stkout.Date = billing.Date;
                                stkout.Product_Token = Token;
                                stkout.Pieces = productforsaleitems.Pieces * Quant;
                               

                                stkout.CGST = (productforsaleitems.Selling_Price * Quant) + ((productforsaleitems.CGST * Quant) / 100);
                                stkout.SGST = (productforsaleitems.Selling_Price * Quant) + ((productforsaleitems.SGST * Quant) / 100);
                                stkout.Sub_Total = productforsaleitems.Total * Quant;
                                stkout.Marchent_Token_number = mrchnttk;

                                detail.Add(blldtl);
                                stkoutlst.Add(stkout);
                            }
                        }
                        l++;

                        blldtl = new Billing_Detail();
                        stkout = new Stockout();
                    }
                    if (chkst.SGST == true && chkst.CGST == true)
                    {
                        billing.CGST = billing.Total_tax / 2;
                        billing.SGST = billing.CGST;
                    }
                    else if (chkst.CGST == true && chkst.IGST == true)
                    {
                        billing.CGST = billing.Total_tax / 2;
                        
                    }
                    else if (chkst.CGST == true && chkst.UTGST == true)
                    {
                        billing.CGST = billing.Total_tax / 2;
                      
                    }
                    else if (chkst.SGST == true && chkst.IGST == true)
                    {
                        billing.SGST = billing.Total_tax / 2;
                        
                    }
                    else if (chkst.SGST == true && chkst.UTGST == true)
                    {
                        billing.SGST = billing.Total_tax / 2;
                        
                    }
                    
                    else
                    { }

                    brcdmstr.Barcode_Number = barcode;
                    brcdmstr.Billing_Number = billing.Billing_Number;
                    brcdmstr.Billing_Token_number = billing.Token_Number;
                    brcdmstr.Date = billing.Date;

                    billing.Barcode_Number = barcode;
                    billing.Customer_Token_number = customertoken;

                    // image save for barcode///

                    using (MemoryStream ms = new MemoryStream())
                    {
                        //The Image is drawn based on length of Barcode text.
                        using (Bitmap bitMap = new Bitmap(barcode.Length * 30, 90))
                        {
                            //The Graphics library object is generated for the Image.
                            using (Graphics graphics = Graphics.FromImage(bitMap))
                            {
                                //The installed Barcode font.
                                Font oFont = new Font("IDAutomationHC39M", 17);
                                PointF point = new PointF(2f, 2f);

                                //White Brush is used to fill the Image with white color.
                                SolidBrush whiteBrush = new SolidBrush(Color.White);
                                graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);

                                //Black Brush is used to draw the Barcode over the Image.
                                SolidBrush blackBrush = new SolidBrush(Color.Black);
                                graphics.DrawString("*" + barcode + "*", oFont, blackBrush, point);
                            }

                            //The Bitmap is saved to Memory Stream.
                            bitMap.Save(ms, ImageFormat.Png);

                            //The Image is finally converted to Base64 string.


                            byte[] imageBytes = Convert.FromBase64String(Convert.ToBase64String(ms.ToArray()));

                            MemoryStream ms1 = new MemoryStream(imageBytes, 0, imageBytes.Length);
                            ms1.Write(imageBytes, 0, imageBytes.Length);

                            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                            string spath = System.Web.HttpContext.Current.Server.MapPath("~/BillBarcode");
                            string path = spath + "/" + billing.Billing_Number + ".jpg";
                            image.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);

                            //p.photo = imageBytes;
                            billing.Barcode_photo = imageBytes;
                            brcdmstr.Image = imageBytes;
                        }

                    }
                    billing.Billing_Details = detail;
                    billing.Stockouts = stkoutlst;
                    billing.Barcode_Master.Add(brcdmstr);

                    dc.Billing_Masters.Add(billing);
                    dc.SaveChanges();
                }

                var dcmlempty = decimal.Parse("0.000");
                var products_For_Sales = (from pdfs in db.Products_For_Sales
                                          join stk in db.Stocks on pdfs.Token_Number equals stk.Product_Token
                                          where (stk.Pieces != 0 ) && pdfs.Approve == true

                                          select pdfs).Distinct();
                ViewBag.productstks = await (from pdfs in db.Products_For_Sales
                                             join stk in db.Stocks on pdfs.Token_Number equals stk.Product_Token
                                             where (stk.Pieces != 0 ) && pdfs.Approve == true

                                             select stk).Distinct().ToListAsync();
                ViewBag.Success = "Thanks for billing...";
                ViewBag.billno = billing.Billing_Number;
                return View( products_For_Sales.ToList().ToPagedList(page ?? 1, 9));
            }
            catch {
                var dcmlempty = decimal.Parse("0.000");
                var products_For_Sales = (from pdfs in db.Products_For_Sales
                                          join stk in db.Stocks on pdfs.Token_Number equals stk.Product_Token
                                          where (stk.Pieces != 0 ) && pdfs.Approve == true

                                          select pdfs).Distinct();
                ViewBag.productstks = await (from pdfs in db.Products_For_Sales
                                          join stk in db.Stocks on pdfs.Token_Number equals stk.Product_Token
                                          where (stk.Pieces != 0 ) && pdfs.Approve == true

                                          select stk).Distinct().ToListAsync();
                ModelState.AddModelError(string.Empty, "Something went wrong. Please try again");
                ViewBag.Error = "Something went wrong. Please try again...";
                return View(products_For_Sales.ToList().ToPagedList(page ?? 1, 9));
            }
        }

        [HttpPost]
        public JsonResult Bill(Billing_Master Billing)
        {
           
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:8087/api/Billing/PostProductBill");

                //HTTP POST
                var postTask = client.PostAsJsonAsync("http://localhost:8087/api/Billing/PostProductBill", Billing);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var nmbr = result.Content.ReadAsStringAsync()
                                               .Result
                                               .Replace("\\", "")
                                               .Trim(new char[1] { '"' });

                    return new JsonResult { Data = new { status = true,nmbr } };
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");


            return new JsonResult { Data = new { status = false } };
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
