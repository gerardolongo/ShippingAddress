using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestioneIndirizzi.Models
{
    public partial class ShippingAddress
    {
        public int id { get; set; }
        public string label { get; set; }
        [Required]
        public string first_name { get; set; }
        [Required]
        public string last_name { get; set; }
        public string company { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        public string city { get; set; }
        [Required]
        public string country { get; set; }
        [Required]
        public string province { get; set; }
        [Required]
        public string postal_code { get; set; }
        [Required]
        public string phone { get; set; }
        public int tenant_id { get; set; }

        public Tenant Tenant { get; set; }
    }
}
