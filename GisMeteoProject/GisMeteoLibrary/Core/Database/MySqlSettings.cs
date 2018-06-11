using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GisMeteoLibrary.Core.DatabaseContext
{
    public class MySqlSettings
    {
        public string Host { get; }
        public int Port { get; }
        public string DatabaseName { get; }
        public string UserName { get; }
        public string Password { get; }
        public string SslMode { get; }

        public string ConnectionString { get; set; }

        public MySqlSettings()
        {
            this.Host = "localhost";
            this.Port = 3306;
            this.DatabaseName = "gis_database";
            this.UserName = "root";
            this.SslMode = "none";

            this.ConnectionString = "Server=" + Host + ";Database=" + DatabaseName + ";port=" + Port + ";User Id=" + UserName + ";SslMode=" + SslMode + "";

        }

        public MySqlSettings(string host, int port, string databaseName, string username, string sslMode)
        {
            this.Host = host;
            this.Port = port;
            this.DatabaseName = databaseName;
            this.UserName = username;
            this.SslMode = sslMode;

            this.ConnectionString = "Server=" + Host + ";Database=" + DatabaseName + ";port=" + Port + ";User Id=" + UserName + ";SslMode=" + SslMode + "";
        }

        public MySqlSettings(string host, int port, string databaseName, string username, string password, string sslMode)
        {
            this.Host = host;
            this.Port = port;
            this.DatabaseName = databaseName;
            this.UserName = username;
            this.Password = password;
            this.SslMode = sslMode;

            this.ConnectionString =  "Server=" + Host + ";Database=" + DatabaseName + ";port=" + Port + ";User Id=" + UserName + ";password=" + Password + ";SslMode=" + SslMode + "";
        }
    }
}
