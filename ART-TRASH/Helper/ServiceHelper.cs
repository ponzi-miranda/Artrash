using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtTrash.Helpers
{
    public static class ServiceHelper
    {
        public const string NOMBRE_PROGRAMA = "ArtTrash";
        public static Service.Service ws;

        static ServiceHelper()
        {
            ws = new Service.Service();
            //ws.Url = [config]
        }
    }
}

