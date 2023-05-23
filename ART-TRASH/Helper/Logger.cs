using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtTrash.Helpers
{
    public class Logger
    {
        private static string PATH = @"C:\Logs\";

        public static void LogException(string source, Exception e)
        {
            LogText(source, e.Message, "EXCEPTION");
            if (e.InnerException == null)
            {
                return;
            }
            LogException(source, e.InnerException);
        }

        public static void LogInfo(string source, string message)
        {
            LogText(source, message, "INFORMATION");
        }

        public static void LogError(string source, string message)
        {
            LogText(source, message, "ERROR");
        }

        public static void LogAppStart()
        {
            LogText("Application_Start", "Start", "START");
        }

        private static void LogText(string source, string message, string concept)
        {
            try
            {
                System.IO.File.AppendAllText(PATH + DateTime.Now.ToString("ddMMyy_") + "COMPRAS_LOGS.txt",
                                "[" + concept + "]:" + "[" + DateTime.Now.ToString("hh:mm:ss") + "]" + " SOURCE: " + source + " | MESSAGE: " + message + System.Environment.NewLine);
            }
            catch
            {
                //DO NOTHING
            }
        }
    }
}