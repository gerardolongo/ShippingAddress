using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestioneIndirizzi.Models
{
    public class BillingAddressAudit
    {
        public int id { get; set; }
        public int id_billing { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string company { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string province { get; set; }
        public string postal_code { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string vat_code { get; set; }
        public string fiscal_code { get; set; }
        public DateTime date_upd { get; set; }
    }
}
