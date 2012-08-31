using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace versomas.net.services.syndication.caching
{
    public class WCFCacheItem
    {
        /// <summary>
        /// Output parameters
        /// </summary>
        public object[] Outputs { get; set; }
        /// <summary>
        /// Return value
        /// </summary>
        public object  ReturnValue { get; set; }
        /// <summary>
        /// Content type for the response
        /// </summary>
        public string ContentType { get; set; }

    }
}
