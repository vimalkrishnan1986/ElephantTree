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

    public partial class User
    {
        public string Token_number { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone_number { get; set; }
        public string Alternate_phone_number { get; set; }
        public Nullable<int> State { get; set; }
        public string State_Name { get; set; }
        public string Address { get; set; }
        public string Second_Address { get; set; }
        public bool IsActive { get; set; }
        public string Marchent_Token_number { get; set; }
        public System.DateTime Applied_date { get; set; }
    }
}

