using System;
using System.Collections.Generic;
using Microsoft.Http;
using System.Web;
using System.Data;
using System.Xml.Serialization;
using System.IO;
using System.Text;

namespace versomas.net.services.syndication.helpers
{
    public static class RemoteData
    {
        public static Object GetRemoteObject(string serviceUrl, Type dataType)
        {
            var httpClient = new HttpClient();
            var response = new HttpResponseMessage();

            try
            {
                response = httpClient.Get(serviceUrl);
                var xmlSerializer = new XmlSerializer(dataType);
                return xmlSerializer.Deserialize(response.Content.ReadAsStream());
            }
            catch
            {
                return null;
            }

        }

        public static Stream GetRemoteStream(string serviceUrl)
        {
            var httpClient = new HttpClient();
            var response = new HttpResponseMessage();
            Stream tStream = new MemoryStream();

            try
            {
                response = httpClient.Get(serviceUrl);
                var sr = new StreamReader(response.Content.ReadAsStream());
                tStream = sr.BaseStream;
                return tStream;
            }
            catch (Exception e)
            {
                byte[] exceptionByteStream = Encoding.UTF8.GetBytes("{\"Exception\":\"" + e.Message.ToString() + "\"}");
                tStream.Write(exceptionByteStream, 0, exceptionByteStream.Length);
                return tStream;
            }
        }


        public static string GetRemoteData(string serviceUrl)
        {
            var httpClient = new HttpClient();
            var response = new HttpResponseMessage();

            try
            {
                response = httpClient.Get(serviceUrl);
                var sr = new StreamReader(response.Content.ReadAsStream());                                                
                var s = sr.ReadToEnd();
                sr.Close();                
                return s;
            }
            catch (Exception e)
            {
                return "{\"Exception\":\"" + e.Message.ToString() + "\"}";
            }
        }
    }
}