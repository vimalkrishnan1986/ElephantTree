//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EasyBilling.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Stockout
    {
        public int Stock_out_Id { get; set; }
        public string Billing_Token_number { get; set; }
        public string Billing_number { get; set; }
        public System.DateTime Date { get; set; }
        public string Product_Token { get; set; }
        public int Pieces { get; set; }
        public decimal Sub_Total { get; set; }
        public string Marchent_Token_number { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }

        public virtual Billing_Master Billing_Master { get; set; }
    }
}


