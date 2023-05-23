using ArtTrash.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtTrash.Models
{
    [Serializable]
    public class StockPrintModel
    {
        public string Evento { get; set; }
        public string Marca { get; set; }
        public string Codigo { get; set; }
        public string Producto { get; set; }
        public string Tipo { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
    }
}