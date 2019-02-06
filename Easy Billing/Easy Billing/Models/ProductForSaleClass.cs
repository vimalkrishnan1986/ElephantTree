using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyBilling.Models
{
    public class ProductForSaleClass
    {

        public string Token_Number { get; set; }
        public string Product_Token { get; set; }
        public string Product_name { get; set; }
        public int Pieces { get; set; }
        public decimal Amout_after_tax { get; set; }
        public decimal Total { get; set; }
        public bool Approve { get; set; }
        public string Tyre_Size { get; set; }
        public string Supplier_token { get; set; }
        public string Supplier_name { get; set; }
        public System.DateTime Date { get; set; }
        public decimal Purchase_Price { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
        public string Administrator_Token_number { get; set; }
        public string Administrator_name { get; set; }
        public System.DateTime Approve_date { get; set; }

        public decimal StockIn { get; set; }
        public decimal Selling_Price { get; set; }
      
        public bool With_tube { get; set; }
        public virtual Product Product { get; set; }
    }
}