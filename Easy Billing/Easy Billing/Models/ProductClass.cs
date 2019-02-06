using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace EasyBilling.Models
{
    public class ProductClass
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductClass()
        {
            
            this.Products_For_Sale = new HashSet<ProductForSaleClass>();
        }

        public string Token_Number { get; set; }
        public string Product_Code { get; set; }
        public string Product_name { get; set; }
        public string Description { get; set; }
        public string HSN_SAC_Code { get; set; }
        public bool IsActive { get; set; }
        public decimal Tax_rate { get; set; }
        public string GL_CODE { get; set; }
        [DisplayName("Tax group")]
        public string txgrp { get; set; }

        public string Marchent_Token_number { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductForSaleClass> Products_For_Sale { get; set; }
    }
}