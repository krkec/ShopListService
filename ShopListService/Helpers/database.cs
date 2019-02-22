using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopListService.Helpers
{
    public class Database
    {
        public static string ConnectionString
        {
            get
            {
                //return WebConfigurationManager.ConnectionStrings["MyMSSOLS"].ConnectionString;
                string conS = "Data Source = 198.38.83.200; initial catalog = krkeec_shop; user id = krkeec_admin; password = m62rm8F4";
                return conS;
            }
        }
        public Database()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
