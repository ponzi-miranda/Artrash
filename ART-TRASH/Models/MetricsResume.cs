using ArtTrash.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtTrash.Models
{
    [Serializable]
    public class MetricsResume : DTOBase
    {
        public string event_name { get; set; }
        public string brand_name { get; set; }
        public string product_name { get; set; }
        public int sales { get; set; }
        public int quantity { get; set; }
        public string percentage { get; set; }
        public decimal total { get; set; }
        public decimal profit { get; set; }
        public string type_total { get; set; }
        public string denomination { get; set; }
    }
}