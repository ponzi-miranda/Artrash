using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebImportaciones.DTOs
{
    [Serializable]
    public class Product_typeDTO : DTOBase
    {
        public string description { get; set; }
    }
}