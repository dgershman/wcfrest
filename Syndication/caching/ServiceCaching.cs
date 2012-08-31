using System;
using System.Configuration;
using System.Web;
using System.Text.RegularExpressions;
using versomas.net.services.syndication.logging;

namespace versomas.net.services.syndication.caching
{
    public class ServiceCaching : IHttpModule
    {
        #region IHttpModule Members        

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public void Init(HttpApplication application)
        {
            application.BeginRequest += new EventHandler(application_BeginRequest);            
           
        }

        private void caching(HttpContext ctx)
        {
            string IfModifiedSinceHeaderString = ctx.Request.Headers["If-Modified-Since"];
            ServiceCachingConfiguration config = (ServiceCachingConfiguration)ConfigurationManager.GetSection("serviceCachingGroup/serviceCaching");            
            int CacheTimeSecondsSetting = int.Parse(ConfigurationManager.AppSettings["Default-Cache-Time-Seconds"].ToString());

            if (config.Enabled)
            {
                for (var x = 0; x < config.CachingRules.Count; x++)
                {
                    if (Regex.IsMatch(ctx.Request.Url.ToString(), config.CachingRules[x].Pattern))
                    {
                        CacheTimeSecondsSetting = config.CachingRules[x].CacheTime;
                        break;
                    }
                }
            }
                        
            DateTime CurrentUniversalDateTime = DateTime.Now.ToUniversalTime();

            if (IfModifiedSinceHeaderString != null)
            {               
                // Hotfix 73: Fix for weird If-Modified-Since headers in IE6
                if (IfModifiedSinceHeaderString.Contains(";")) IfModifiedSinceHeaderString = IfModifiedSinceHeaderString.Split(';')[0];
                
                DateTime IfModifiedSinceUniversalDateTime = DateTime.Parse(IfModifiedSinceHeaderString).ToUniversalTime();
                int difference = Convert.ToInt32((IfModifiedSinceUniversalDateTime - CurrentUniversalDateTime.AddSeconds(-CacheTimeSecondsSetting)).TotalSeconds);

                if (difference > 0)
                {
                    ctx.Response.AddHeader("Cache-Control", "max-age=" + difference);
                    ctx.Response.AddHeader("Last-Modified", IfModifiedSinceUniversalDateTime.ToString("r"));
                    ctx.Response.AddHeader("Expires", IfModifiedSinceUniversalDateTime.AddSeconds(CacheTimeSecondsSetting).ToString("r"));
                    ctx.Response.StatusCode = 304;
                    ctx.Response.SuppressContent = true;
                    ctx.Response.End();
                    return;
                }
            }

            ctx.Response.AddHeader("Cache-Control", "max-age=" + CacheTimeSecondsSetting.ToString() + (CacheTimeSecondsSetting == 0 ? ", no-cache" : ""));
            ctx.Response.AddHeader("Last-Modified", CurrentUniversalDateTime.ToString("r"));
            ctx.Response.AddHeader("Expires", CurrentUniversalDateTime.AddSeconds(CacheTimeSecondsSetting).ToString("r"));                           
        }

        void application_BeginRequest(object sender, EventArgs e)
        {

            var app = (HttpApplication)sender;
            var ctx = app.Context;        

            caching(ctx);
        }

        #endregion
    }
}
