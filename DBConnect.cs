using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.Odbc;

namespace SmitService.WCF
{
       public class DBConnect:IDisposable
    {
        public string userid;
        public string dsn;
        public string pwd;

        public OdbcConnection conn;
        public DBConnect()
        {
            //
            //
            //conn.ConnectionString = "DSN=kfz;uid=bungert;pwd=sesam--06";
            conn = new OdbcConnection();
        }

        public void Connect()
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["dsn"].ConnectionString;
            //conn.ConnectionString = "dsn=" + dsn + ";uid=" + userid + ";pwd=" + pwd;
            conn.Open();

        }

        public void Disconnect()
        {
            conn.Close();
        }


        public void Dispose()
        {
            if (conn.State == ConnectionState.Open)
                conn.Dispose();
        }
    }
}