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

    public partial class Transaction_amount_detail
    {
        public string Token_Number { get; set; }
        public string Marchent_Token { get; set; }
        public string Purchase_Token { get; set; }
        public string Purchase_number { get; set; }
        public string Sale_invoice_token { get; set; }
        public string Sale_invoice_number { get; set; }
        public string GL_CODE { get; set; }
        public decimal Amount { get; set; }
        public string Card_number { get; set; }
        public string Cheque_number { get; set; }
    }
}

