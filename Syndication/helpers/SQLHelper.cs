
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using versomas.net.services.syndication.intrusion;
using versomas.net.services.syndication.caching;

namespace versomas.net.services.syndication.helpers
{
    public static class SQLHelper
    {
        private static readonly string ConnStr = ConfigurationManager.ConnectionStrings["connstr"].ToString();
        
        public static DataTable DataTableToSQL(string query)
        {
            // SQL Injection scan
            if (!SQLInjection.IsAttack(query))
            {
                query = query.Replace("/", ""); //Remove attack prevention symbols
                //var dc = DataCaching.GetInstance;                
                //if (dc.GetFromCache(query) == null)
                //{
                    var sqlconn = new SqlConnection(ConnStr);
                    var sqlda = new SqlDataAdapter(query, sqlconn);
                    var ds = new DataSet();
                    sqlconn.Open();
                    sqlda.Fill(ds);
                    sqlconn.Close();
                    //dc.AddToCache(query, ds.Tables[0]);
                    return ds.Tables[0];
                //}
                //else
                //{
                //    return (DataTable)dc.GetFromCache(query);
                //}
            }
            else
            {
                return null;
            }
        }
    }
}
