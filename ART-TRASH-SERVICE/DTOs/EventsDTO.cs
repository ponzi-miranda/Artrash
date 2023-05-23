using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebImportaciones.DTOs
{
    [Serializable]
    public class EventsDTO : DTOBase
    {
        public string name { get; set; }
        public decimal inscription { get; set; }
        public DateTime start_date { get; set; }
        public DateTime finish_date { get; set; }
        public string state { get; set; }
    }
}
