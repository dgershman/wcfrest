using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Runtime.InteropServices;

namespace versomas.net.services.syndication.query
{
    public class INIQuery : IQuery
    {
        private XmlDocument xmldoc = new XmlDocument();
        private string _filename { get; set; }

        [DllImport("kernel32.dll", CharSet=CharSet.Unicode)]
        static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString,uint nSize, string lpFileName);
        
        public INIQuery(string filename)
        {
            this._filename = filename;
        }

        public string GetSingleNodeValue(string query)
        {
            var retVal = new StringBuilder(8000);
            var splitArray = query.Split('|');
            GetPrivateProfileString(splitArray[0], splitArray[1], "", retVal, 8000, _filename);
            return retVal.ToString();
        }
    }
}
