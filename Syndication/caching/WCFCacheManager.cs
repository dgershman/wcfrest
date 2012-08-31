using System;
using System.Collections;
using System.IO;

namespace versomas.net.services.syndication.caching
{
    public static class WCFCacheManager
    {
        /// <summary>
        /// Backing store
        /// </summary>        
        /// <summary>
        /// Lock object
        /// </summary>
        private static object lockInstance = new object();
        /// <summary>
        /// Puts an item in cache
        /// </summary>
        /// <param name="key">Cache Key</param>
        /// <param name="item">Item to be put into cache</param>
        public static void SetItemInCache(string key, object item)
        {
            lock (lockInstance)
            {
                DataCaching.AddToCache(key, item);
            }
        }
        /// <summary>
        /// Retrives item from cache
        /// </summary>
        /// <param name="key">Cache Key</param>
        /// <returns>Item stored in cache</returns>
        public static object GetItemFromCache(string key)
        {
            object item = null;
            lock (lockInstance)
            {
                item = DataCaching.GetFromCache(key);
            }
            
            return item;
        }

        /// <summary>
        /// Clears the cache
        /// </summary>
        public static void ClearCache(string cacheKey)
        {
            lock (lockInstance)
            {
                DataCaching.RemoveFromCache(cacheKey);
            }
        }
    }
}
