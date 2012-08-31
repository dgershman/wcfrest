using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ServiceModel.Syndication;
using System.Collections;

namespace versomas.net.services
{      
    public class FeedItem 
    {
        private SyndicationItem _si = new SyndicationItem();
      
        public string Title { get; set; }
        public string Summary { get; set; }
        public DateTime LastUpdatedTime {get; set;}       

        public SyndicationItem GetSyndicationItem()
        {
            _si.Title = new TextSyndicationContent(Title);
            _si.Summary = new TextSyndicationContent(Summary);
            _si.LastUpdatedTime = LastUpdatedTime;            

            return _si;
        }
    }
}



