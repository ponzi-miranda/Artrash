using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebImportaciones.DTOs
{
    [Serializable]
    public class InscriptionsDTO : DTOBase
    {
        public int event_id { get; set; }
        public int brand_id { get; set; }
    }
}