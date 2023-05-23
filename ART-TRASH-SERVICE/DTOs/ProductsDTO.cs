using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebImportaciones.DTOs
{
    [Serializable]
    public class ProductsDTO : DTOBase
    {
        public string serial_number { get; set; }
        public string description { get; set; }
        public int product_type_id { get; set; }
        public decimal price { get; set; }
        public int brand_id { get; set; }
        public string state { get; set; }
    }
}