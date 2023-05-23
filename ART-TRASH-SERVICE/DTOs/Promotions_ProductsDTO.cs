using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebImportaciones.DTOs
{
    [Serializable]
    public class Promotions_ProductsDTO : DTOBase
    {
        public int idPromotion { get; set; }
        public int idProduct { get; set; }
        public int quantity { get; set; }
    }
}