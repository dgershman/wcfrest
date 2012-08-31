using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Resources;
using System.Web;
using System.Configuration;
using System.Reflection;
using System.Globalization;

namespace versomas.net.services.syndication.localization
{
    public class ResourceJS : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            ResourceManager rm = new ResourceManager(ConfigurationManager.AppSettings["JSResourcesAssemblyType"].ToString(),
                Assembly.LoadFile(ConfigurationManager.AppSettings["JSResourcesAssemblyPath"].ToString()));
            if (context.Request.QueryString["CultureCode"] == null) return;
            var culture = context.Request.QueryString["CultureCode"].ToString();            
            ResourceSet rs = rm.GetResourceSet(new CultureInfo(culture), true, true);            
            var sbInitial = "var rm = {";
            var sb = new StringBuilder(sbInitial);            
            var resEnum = rs.GetEnumerator();
            while (resEnum.MoveNext())
            {
                if (sb.ToString() != sbInitial) sb.Append(",");
                sb.Append("\"" + resEnum.Key + "\":\"" + 
                    resEnum.Value.ToString().Replace("\r\n", "").Replace("\"", "\\\"") + "\"");
            }

            sb.Append("}");
            sb.ToString();

            context.Response.ContentType = "text/javascript";
            context.Response.Write(sb.ToString());
        }
    }
}
