using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Configuration;

namespace versomas.net.services.syndication.logging
{
    public static class SimpleTextWriter
    {
        public static void WriteToLog(string message) 
        {
            var sw = new StreamWriter(ConfigurationManager.AppSettings["SimpleLogPath"].ToString() + "application.log", true);
            sw.WriteLine(DateTime.Now.ToString() + ": " + message);
            sw.Close();
        }
    }
}
