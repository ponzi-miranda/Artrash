using ArtTrash.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtTrash.Models
{
    [Serializable]
    public class StockModel : StockDTO
    {
        public string serial_number { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public string brand { get; set; }
        public decimal price { get; set; }
    }
}