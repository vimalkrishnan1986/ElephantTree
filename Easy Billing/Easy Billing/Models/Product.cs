
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

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.Products_For_Sale = new HashSet<Products_For_Sale>();
        }

        public string Token_Number { get; set; }
        public string Product_Code { get; set; }
        public string Product_name { get; set; }
        public string Description { get; set; }
        public string HSN_SAC_Code { get; set; }
        public decimal Tax_rate { get; set; }
        public string GL_CODE { get; set; }
        public bool IsActive { get; set; }
        public string Marchent_Token_number { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Products_For_Sale> Products_For_Sale { get; set; }
    }
}

