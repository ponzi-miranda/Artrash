using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebImportaciones.DTOs
{
    [Serializable]
    public class SecurityDTO : DTOBase
    {
        public int IdRol { get; set; }
        public string Program { get; set; }
        public string Menu { get; set; }
    }
}