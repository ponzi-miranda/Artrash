using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebImportaciones.DTOs
{
    [Serializable]
    public class Sales_detailsDTO : DTOBase
    {
        public int sale_id { get; set; }
        public int product_id { get; set; }
        public int brand_id { get; set; }
        public int quantity { get; set; }
        public decimal total { get; set; }
        public decimal profit { get; set; }
    }
}