using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Xsl;
using System.Xml;
using System.Configuration;

namespace versomas.net.services.syndication.helpers
{
    public static class XMLHelper
    {
        public static Stream TransformXmlStream(Stream xmlStream, string templateFilename)
        {
            var sr = new StreamReader(templateFilename);
            Stream tStream;
            using (sr)
            {
                XmlReader xmlr = XmlReader.Create(sr);
                tStream = ApplyTransformation(xmlStream, xmlr);
            }
            sr.Close();
            return tStream;
        }

        public static Stream ApplyTransformation(Stream xmlStream, XmlReader xsltStream)
        {
            var xslt = new XslCompiledTransform();            
            xslt.Load(xsltStream);
            var xmld = new XmlDocument();
            xmld.Load(xmlStream);
            var ms = new MemoryStream();
            xslt.Transform(xmld, null, ms);
            ms.Position = 0;
            return ms;
        }

    }
}
