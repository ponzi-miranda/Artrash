using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebImportaciones.DTOs
{
    [Serializable]
    public class SalesDTO : DTOBase
    {
        public int event_id { get; set; }
        public int payment_method_id { get; set; }
        public DateTime date { get; set; }
        public decimal total { get; set; }
        public decimal profit { get; set; }
        public string state { get; set; }
    }
}