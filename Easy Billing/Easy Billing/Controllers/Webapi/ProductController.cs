using EasyBilling.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EasyBilling.Controllers.Webapi
{
    public class ProductController : ApiController
    {
        public IHttpActionResult GetAllProducts(bool includeAddress = false)
        {
            IList<ProductClass> product = null;

            using (var ctx = new EasyBillingEntities())
            {

                product = ctx.Products.Select(s => new ProductClass()
                {
                    Token_Number=s.Token_Number,
                    Product_Code = s.Product_Code,
                    Product_name = s.Product_name,
                    Description = s.Description,
                    Tax_rate = s.Tax_rate,
                    GL_CODE=s.GL_CODE,
                    txgrp= (from a in ctx.Tax_Groups
                            where a.GL_CODE == s.GL_CODE
                            select a.Tax_Name).Distinct().FirstOrDefault(),
                    HSN_SAC_Code = s.HSN_SAC_Code,
                    IsActive = s.IsActive,
                  
                  
                }).ToList<ProductClass>();


            }


            if (product.Count == 0)
            {
                return NotFound();
            }

            return Ok(product);
        }
        public IHttpActionResult PostNewProduct([FromBody]Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest("Sorry there is some problem. Please check and try again");

            using (var ctx = new EasyBillingEntities())
            {


                if (product.HSN_SAC_Code == null)
                {
                    product.Tax_rate = 0;
                }
                else if (product.HSN_SAC_Code != null)
                {
                       // product.GL_CODE = product.txgrp;
                        product.Tax_rate = Convert.ToDecimal((from a in ctx.Tax_Groups
                                           
                                            select a.Tax_Rate).Distinct().FirstOrDefault());
                        product.IsActive = true;
                        product.Token_Number = (Guid.NewGuid()).ToString();
                        ctx.Products.Add(product);
                        ctx.SaveChanges();
                    
                }
                else
                {
                    return BadRequest("Taxgroup does not exists");
                }
            }

            return Ok();
        }

        public IHttpActionResult GetAllProducts(string id)
        {
           
            ProductClass product = null;

            using (var ctx = new EasyBillingEntities())
            {

                product = ctx.Products.Where(x => x.Token_Number == id).Select(s => new ProductClass()
                {
                    Token_Number = s.Token_Number,
                    Product_Code = s.Product_Code,
                    Product_name = s.Product_name,
                    Description = s.Description,
                    Tax_rate = s.Tax_rate,
                    txgrp = (from a in ctx.Tax_Groups
                             where a.GL_CODE == s.GL_CODE
                             select a.Tax_Name).Distinct().FirstOrDefault(),
                    HSN_SAC_Code = s.HSN_SAC_Code,
                    GL_CODE=s.GL_CODE,
                    IsActive = s.IsActive,
                    
                }).FirstOrDefault();


            }


            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
        public IHttpActionResult PutProducts([FromBody]Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid data");

            using (var ctx = new EasyBillingEntities())
            {
                var existingProduct = ctx.Products.Where(s => s.Token_Number == product.Token_Number).FirstOrDefault();

                if (existingProduct != null)
                {
                    
                        //existingProduct.GL_CODE = product.txgrp;
                        product.Tax_rate = Convert.ToDecimal((from a in ctx.Tax_Groups
                                          
                                            select a.Tax_Rate).Distinct().FirstOrDefault());
                        existingProduct.Product_name = product.Product_name;
                        existingProduct.Product_Code = product.Product_Code;
                        existingProduct.Description = product.Description;
                        existingProduct.Tax_rate = product.Tax_rate;
                        existingProduct.HSN_SAC_Code = product.HSN_SAC_Code;
                        existingProduct.IsActive = true;

                        ctx.SaveChanges();
                   
                    
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok();
        }

        public IHttpActionResult Delete(string id)
        {
            if (id == null)
                return BadRequest("Not a valid student id");

            using (var ctx = new EasyBillingEntities())
            {
                var product = ctx.Products
                    .Where(s => s.Token_Number == id)
                    .FirstOrDefault();

                ctx.Entry(product).State = EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }
        [System.Web.Http.ActionName("PostRateUpdate")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult PostRateUpdate(JObject jsonData)
        {
            dynamic json = jsonData;
            string token = json.Token_number;
            string Upselling = json.UpSellingp;
            string Upcgst = json.Upcgst;
            string Upsgst = json.Upsgst;
            using (var ctx = new EasyBillingEntities())
            {
                var existingProduct = ctx.Products_For_Sales.Where(s => s.Token_Number == token).FirstOrDefault();

                if (existingProduct != null)
                {
                    existingProduct.Selling_Price =decimal.Parse(Upselling);
                    existingProduct.CGST = decimal.Parse(Upcgst);
                    existingProduct.SGST = decimal.Parse(Upsgst);
                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok();
        }
        [System.Web.Http.ActionName("PostActivate")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult PostActivate(JObject jsonData)
        {
            dynamic json = jsonData;
            string token = json.Token_number;
            using (var ctx = new EasyBillingEntities())
            {
                var existingProduct = ctx.Products.Where(s => s.Token_Number == token).FirstOrDefault();

                if (existingProduct != null)
                {
                    var chkkactive = (from a in ctx.Products
                                      where a.Token_Number == token
                                      select a.IsActive).Distinct().FirstOrDefault();
                    if (chkkactive == true)
                    {
                        existingProduct.IsActive = false;
                    }
                    else if (chkkactive == false)
                    {
                        existingProduct.IsActive = true;
                    }
                    else
                    {
                        existingProduct.IsActive = false;
                    }
                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok();
        }
     
     }
    }