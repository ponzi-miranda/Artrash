using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebImportaciones.DTOs
{
    public class StockDTO : DTOBase
    {
        public int brand_id { get; set; }
        public int product_id { get; set; }
        public int quantity { get; set; }
        public int event_id { get; set; }
    }
}