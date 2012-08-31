using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace versomas.net.services.syndication.intrusion
{
    public static class SQLInjection
    {
        public static string[] blackList = {"--",";--",";","/*","*/",
                                       "char","nchar","varchar","nvarchar",
                                       "alter","begin","cast","create","cursor","declare","delete","drop"," end","exec","execute",
                                       "fetch","insert","kill","open",
                                       "select", "sys","sysobjects","syscolumns",
                                       "table","update"};

        public static bool IsAttack(string parameter)
        {
            for (var i = 0; i < blackList.Length; i++)
            {
                if ((parameter.IndexOf(blackList[i], StringComparison.OrdinalIgnoreCase) >= 0)) 
                    return true;                
            }

            return false;
        }

    }
}
