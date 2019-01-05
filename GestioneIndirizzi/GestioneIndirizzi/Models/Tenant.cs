using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestioneIndirizzi.Models
{
    public partial class Tenant
    {
        public Tenant()
        {
            ShippingAddress = new HashSet<ShippingAddress>();
            BillingAddress = new HashSet<BillingAddress>();
        }

        [JsonIgnore]
        public int id { get; set; }
        [Required]
        public string tenant_id { get; set; }

        [JsonIgnore]
        public ICollection<BillingAddress> BillingAddress { get; set; }
        [JsonIgnore]
        public ICollection<ShippingAddress> ShippingAddress { get; set; }
    }
}
