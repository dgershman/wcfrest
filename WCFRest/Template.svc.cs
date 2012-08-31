using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using versomas.net.services.datacontract;
using System.ServiceModel.Web;
using System.Data;
using versomas.net.services.syndication.engine;
using System.Data.SqlClient;
using System.Configuration;
using versomas.net.services.syndication.helpers;
using versomas.net.services.syndication.intrusion;
using System.ServiceModel.Activation;

namespace versomas.net.services
{
    // NOTE: If you change the class name "FeedOfFeeds" here, you must also update the reference to "FeedOfFeeds" in Web.config.    
    public class Template : ITemplateFeedContract
    {
        #region ITemplateFeedContract Members
        [WebGet(UriTemplate = "/gt/{format}/{table_name}?method={callback}")]   
        public System.IO.Stream GetTemplate(string format, string callback, string table_name)
        {
            var dt = GetTemplate(table_name);
            return ServiceEngine.GetServiceStream(format, callback, dt, null);
        }
        #endregion

        public static DataTable GetTemplate(string table_name)
        {            
            return SQLHelper.DataTableToSQL("E/X/E/C sp_columns @table_name = '" + table_name + "'");                       
        }

    }
}
