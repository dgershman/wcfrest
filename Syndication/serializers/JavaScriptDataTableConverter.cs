using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;

namespace versomas.net.services.syndication.serializers
{
    public class JavaScriptDataTableConverter : JavaScriptConverter
    {
        private string jsonObjectName = "rows";        

        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            throw new NotImplementedException("Deserialize is not implemented.");
        }
        
        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            var dt = obj as DataTable;
            var result = new Dictionary<string, object>();            

            if (dt != null && dt.Rows.Count > 0)
            {
                // List for row values                
                var rowValues = new List<object>();               

                foreach (DataRow dr in dt.Rows)
                {
                    // Dictionary for col name / col value
                    var colValues = new Dictionary<string, object>();                    
                    foreach (DataColumn dc in dt.Columns)
                    {                        
                        if (dr[dc].ToString().IndexOf("&obj;") > 0)
                        {
                            var tempArr = Regex.Split(dr[dc].ToString(), "&obj;");
                            var arrOfDict = new ArrayList();
                            for(var t = 0; t< tempArr.Length; t++)
                            {
                                arrOfDict.Add(PopPairsInDictionary(Regex.Split(tempArr[t], "&arr;")));
                            }
                            colValues.Add(dc.ColumnName, arrOfDict);
                        }
                        else if (dr[dc].ToString().IndexOf("&kvp;") > 0)
                        {
                            var tempArr = Regex.Split(dr[dc].ToString(), "&arr;");                               
                            colValues.Add(dc.ColumnName, PopPairsInDictionary(tempArr));
                        }
                        else if (dr[dc].ToString().IndexOf("&arr;") > 0) 
                        {
                            colValues.Add(dc.ColumnName, Regex.Split(dr[dc].ToString(), "&arr;"));
                        }
                        else 
                        {
                            colValues.Add(dc.ColumnName, // col name
                                (string.Empty == dr[dc].ToString()) ? null : dr[dc]); // col value
                        }
                    }

                    // Add values to row
                    rowValues.Add(colValues);
                }

                // Add rows to serialized object
                result[JsonObjectName] = rowValues;
            }

            return result;
        }

        private Dictionary<string, object> PopPairsInDictionary(string[] pairs)
        {
            var tempDict = new Dictionary<string, object>();
            for (var t = 0; t < pairs.Length; t++)
            {
                var value = Regex.Split(pairs[t], "&kvp;")[1].ToLower();
                if (value == "false" || value == "true")
                    tempDict.Add(Regex.Split(pairs[t], "&kvp;")[0], Boolean.Parse(value));
                else
                    tempDict.Add(Regex.Split(pairs[t], "&kvp;")[0], Regex.Split(pairs[t], "&kvp;")[1]);
            }

            return tempDict;
        }

        public override IEnumerable<Type> SupportedTypes
        {
            //Define the DataTable as a supported type.
            get
            {
                return new System.Collections.ObjectModel.ReadOnlyCollection<Type>(
                 new List<Type>(
                  new Type[] { typeof(DataTable) }
                 )
                );
            }
        }

        public string JsonObjectName
        {
            get { return jsonObjectName; }
            set { jsonObjectName = value; }
        }
    }
}
