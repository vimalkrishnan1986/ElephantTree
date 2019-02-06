
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

    public partial class Billing_Master
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Billing_Master()
        {
            this.Barcode_Master = new HashSet<Barcode_Master>();
            this.Billing_Details = new HashSet<Billing_Detail>();
            this.Stockouts = new HashSet<Stockout>();
        }

        public string Token_Number { get; set; }
        public string Billing_Number { get; set; }
        public System.DateTime Date { get; set; }
        public string Marchent_Token_number { get; set; }
        public string Customer_Token_number { get; set; }
        public string Tax_token { get; set; }
        public decimal Total_tax { get; set; }
        public decimal Rate_including_tax { get; set; }
        public decimal Discount_percent { get; set; }
        public decimal Total_discount { get; set; }
        public decimal Total_amount { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
        public string Narration { get; set; }
        public string Barcode_Number { get; set; }
        public byte[] Barcode_photo { get; set; }
        public string Payment_Statement { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Barcode_Master> Barcode_Master { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Billing_Detail> Billing_Details { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Stockout> Stockouts { get; set; }
    }
}
