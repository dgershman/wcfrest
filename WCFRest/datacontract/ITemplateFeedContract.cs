using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;
using versomas.net.services.syndication.caching;

namespace versomas.net.services.datacontract
{
    // NOTE: If you change the interface name "IFeedOfFeeds" here, you must also update the reference to "IFeedOfFeeds" in Web.config.
    [ServiceContract()]
    public interface ITemplateFeedContract
    {
        [OperationContract]
        Stream GetTemplate(string format, string callback, string table_name);           
    }    
}
