using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace AdventureWorksSales.Web {
    public class dbObj {
        public const  string conStr = "data Source=DESKTOP-OTH56VP;Initial Catalog=AdventureWorksSales;Integrated Security=True"; //ConfigurationManager.ConnectionStrings["AdventureWorksSalesDB"].ConnectionString; 
        public string cmdStr;
        public SqlCommand cmdObj;
        public SqlDataReader drObj;
    }
}