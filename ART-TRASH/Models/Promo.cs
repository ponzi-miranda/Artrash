using ArtTrash.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtTrash.Models
{
    [Serializable]
    public class PromoModel : PromotionsDTO
    {
        public int quantity { get; set; }
    }
}