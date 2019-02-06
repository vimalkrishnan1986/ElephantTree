using System;
using System.ComponentModel;

namespace EasyBilling.Models
{
    public class DealerAccount
    {
        public string Token_number { get; set; }
        public string Dealer_code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone_number { get; set; }
        public Nullable<int> State { get; set; }
        public Nullable<bool> Isactive { get; set; }
        public string State_Name { get; set; }
        [DisplayName("State name")]
        public string stcd { get; set; }
        public StateData Statecd { get; set; }
    }
}