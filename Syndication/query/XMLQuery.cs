using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace versomas.net.services.syndication.query
{
    public class XMLQuery : IQuery
    {
        private XmlDocument xmldoc = new XmlDocument();

        public XMLQuery(string filename)
        {
            xmldoc.Load(filename);
        }

        public string GetSingleNodeValue(string query)
        {
            var node = xmldoc.SelectSingleNode(query);
            return (node != null ? node.Value : "");
        }
    }
}
