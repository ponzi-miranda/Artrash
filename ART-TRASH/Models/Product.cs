using ArtTrash.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtTrash.Models
{
    [Serializable]
    public class Product : ProductsDTO
    {
        public string product_type { get; set; }
        public int stock { get; set; }
        public string brand { get; set; }
        public string type { get; set; }
    }
}