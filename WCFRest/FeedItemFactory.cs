using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Syndication;

namespace versomas.net.services
{
    public sealed class FeedItemFactory
    {
        private FeedItemFactory() { }

        public static FeedItem GetInstance(string Title, string Summary, DateTime LastUpdatedTime)
        {
            FeedItem _FeedItem = new FeedItem();
            _FeedItem.Title = Title;
            _FeedItem.Summary = Summary;
            _FeedItem.LastUpdatedTime = LastUpdatedTime;

            return _FeedItem;
        }
    }
}
