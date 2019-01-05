using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestioneIndirizzi.Models
{
    public partial class LogInfo
    {
        public int id { get; set; }
        public string msg { get; set; }
        public string log_type { get; set; }
        public DateTime date { get; set; }
    }
}
