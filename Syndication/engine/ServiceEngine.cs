using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using versomas.net.services.syndication.serializers;
using System.Xml.Serialization;
using System.IO;
using System.Data;
using System.Web.Script.Serialization;
using System.ServiceModel.Web;
using System.Xml;
using System.ServiceModel.Syndication;
using System.Configuration;
using System.Xml.Xsl;
using versomas.net.services.syndication.helpers;

namespace versomas.net.services.syndication.engine
{
    public static class ServiceEngine
    {

        public static Stream GetServiceStreamTransform(string template, DataTable dt)
        {
            MemoryStream stream = new MemoryStream();
            Stream tStream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream, Encoding.UTF8);
            XmlSerializer xmls = new XmlSerializer(typeof(DataTable));            
            xmls.Serialize(writer, dt);
            using (stream)
            {
                stream.Position = 0;
                tStream = XMLHelper.TransformXmlStream(stream, ConfigurationManager.AppSettings["Templates"].ToString() + template + ".xslt");
                //ream.Close();
                WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
                writer.Flush();
                tStream.Position = 0;
            }
            return tStream;       
        }

        public static Stream GetFileStreamTransform(string xsltfilename, string xmlfilename)
        {            
            var stream = new MemoryStream();
            Stream tStream = new MemoryStream();
            var reader = new StreamReader(xmlfilename);
            var writer = new StreamWriter(stream, Encoding.UTF8);
            using (stream)
            {
                stream.Position = 0;
                tStream = XMLHelper.TransformXmlStream(stream, xsltfilename);
                //ream.Close();
                WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
                writer.Flush();
                tStream.Position = 0;
            }
            reader.Close();
            return tStream;
        }

        public static Stream GetHttpStreamTransform(string xsltfilename, string xmlfilename)
        {
            var stream = new MemoryStream();
            Stream tStream = new MemoryStream();
            Stream xmlFileReader = RemoteData.GetRemoteStream(xmlfilename);
            Stream xsltFileReader = RemoteData.GetRemoteStream(xsltfilename);
            var writer = new StreamWriter(stream, Encoding.UTF8);
            XmlReader xmlr = XmlReader.Create(xsltFileReader);
            using (stream)
            {
                stream.Position = 0;
                if (xsltFileReader.ToString().IndexOf("OK") > -1)
                {
                    tStream = XMLHelper.ApplyTransformation(xmlFileReader, xmlr);
                    xmlFileReader.Close();
                }
                else
                {
                    tStream = xmlFileReader;
                }
                WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
                writer.Flush();
                tStream.Position = 0;
            }
            xsltFileReader.Close();
            xmlr.Close();
            return tStream;
        }

        public static Stream GetServiceStream(string format, string callback, DataTable dt, SyndicationFeed sf)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream, Encoding.UTF8);
            if (format == "xml")
            {
                XmlSerializer xmls = new XmlSerializer(typeof(DataTable));
                xmls.Serialize(writer, dt);                     
                WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
            }
            else if (format == "json")
            {
                var toJSON = new JavaScriptSerializer();
                toJSON.RegisterConverters(new JavaScriptConverter[] { new JavaScriptDataTableConverter() });
                writer.Write(toJSON.Serialize(dt));
                WebOperationContext.Current.OutgoingResponse.ContentType = "text/json";
            }
            else if (format == "jsonp")
            {
                var toJSON = new JavaScriptSerializer();
                toJSON.RegisterConverters(new JavaScriptConverter[] { new JavaScriptDataTableConverter() });
                writer.Write(callback + "( " + toJSON.Serialize(dt) + " );");
                WebOperationContext.Current.OutgoingResponse.ContentType = "text/json";
            }
            else if (format == "rss")
            {
                XmlWriter xmlw = new XmlTextWriter(writer);
                sf.SaveAsRss20(xmlw);
                WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
            }
            else if (format == "atom")
            {
                XmlWriter xmlw = new XmlTextWriter(writer);
                sf.SaveAsAtom10(xmlw);
                WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
            }
            else if (format == "help")
            {
                StreamReader sr = new StreamReader(ConfigurationManager.AppSettings["ServiceDocumentation"].ToString());
                writer.Write(sr.ReadToEnd());
                sr.Close();
                WebOperationContext.Current.OutgoingResponse.ContentType = "text/html";
            }
            else
            {
                writer.Write("Invalid formatting specified.");
                WebOperationContext.Current.OutgoingResponse.ContentType = "text/html";
            }

            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }    
}
