using ArtTrash.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtTrash.Models
{
        [Serializable]
        public class Sale : SalesDTO
        {
            public string description { get; set; }
            public string payment_method { get; set; }
            public string event_name { get; set; }
            public string brand_name { get; set; }
            public decimal price { get; set; }
            public decimal brandTotal { get; set; }
            public int quantity { get; set; }
        }
    }