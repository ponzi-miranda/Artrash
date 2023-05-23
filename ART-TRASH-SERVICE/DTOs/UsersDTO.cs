using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebImportaciones.DTOs
{
    [Serializable]
    public class UsersDTO : DTOBase
    {
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string contact_person { get; set; }
        public string mobile { get; set; }
        public int roleId { get; set; }
        public bool active { get; set; }
    }
}