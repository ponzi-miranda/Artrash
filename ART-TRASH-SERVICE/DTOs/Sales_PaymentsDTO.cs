using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebImportaciones.DTOs
{
    [Serializable]
    public class Sales_PaymentsDTO : DTOBase
    {
        public int IdPayments { get; set; }
        public int IdSale { get; set; }
        public decimal amount { get; set; }
    }
}