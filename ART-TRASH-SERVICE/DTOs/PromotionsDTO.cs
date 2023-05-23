using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebImportaciones.DTOs
{
    [Serializable]
    public class PromotionsDTO : DTOBase
    {
        public string description { get; set; }
        public decimal price { get; set; }
        public string state { get; set; }
        public int idBrand { get; set; }
    }
}