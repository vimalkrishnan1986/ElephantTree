
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
    using System.ComponentModel.DataAnnotations;

    public partial class Proprietor
    {
        [Display(Name = "Token number")]
        public string Token_number { get; set; }
        [Display(Name = "Proprietor name")]
        public string Proprietor_name { get; set; }
        [Display(Name = "Email")]
        public string Email_Id { get; set; }

        public string Address { get; set; }
        public string Mobile { get; set; }
        public Nullable<int> State_Code { get; set; }
        [Display(Name = "Pan number")]
        public string Pan_Number { get; set; }
        [Display(Name = "Approve")]
        public bool IsActive { get; set; }
        [Display(Name = "Password")]
        public string Verification_code { get; set; }
        public string State_Name { get; set; }
    }
}

