using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Web.Caching;
using System.Configuration;
using System.Text.RegularExpressions;

namespace versomas.net.services.syndication.caching
{
    public static class DataCaching
    {                     
        public static void AddToCache(string cacheKey, object o)
        {                        
            WCFSerialCachingConfiguration config = (WCFSerialCachingConfiguration)ConfigurationManager.GetSection("wcfSerialCachingGroup/wcfSerialCaching");

            int CacheTimeSecondsSetting = int.Parse(ConfigurationManager.AppSettings["Default-Cache-Time-Seconds"].ToString());
            CacheItemPriority cacheItemPriority = ConvertStringToPriority(ConfigurationManager.AppSettings["Default-Cache-Item-Priority"].ToString());
            if (config.Enabled)
            {
                for (var x = 0; x < config.CachingRules.Count; x++)
                {
                    if (Regex.IsMatch(cacheKey, config.CachingRules[x].Pattern))
                    {
                        CacheTimeSecondsSetting = config.CachingRules[x].CacheTime;
                        cacheItemPriority = ConvertStringToPriority(config.CachingRules[x].ItemPriority);
                        break;
                    }
                }
            }
            HttpRuntime.Cache.Insert(cacheKey, o, null, DateTime.Now.AddSeconds(CacheTimeSecondsSetting), Cache.NoSlidingExpiration, cacheItemPriority, null);                
        }

        public static Object GetFromCache(string cacheKey)
        {
            object _o = null;
            try
            {
                _o = HttpRuntime.Cache.Get(cacheKey);
            }
            catch (NullReferenceException)
            {
                _o = null;
            }
            return _o;
        }

        public static void RemoveFromCache(string cacheKey)
        {
            HttpRuntime.Cache.Remove(cacheKey);
        }

        private static CacheItemPriority ConvertStringToPriority(string itemPriority)
        {
            if (Enum.IsDefined(typeof(CacheItemPriority), itemPriority))
                return (CacheItemPriority)Enum.Parse(typeof(CacheItemPriority), itemPriority);
            else
                return CacheItemPriority.Normal;
        }
    }

}
