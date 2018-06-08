using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GisMeteoLibrary.Core.DatabaseContext
{
    public class MySqlSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SslMode { get; set; }

        public MySqlSettings()
        {
            this.Host = "localhost";
            this.Port = 3306;
            this.DatabaseName = "gis_database";
            this.UserName = "root";
            this.Password = " ";
            this.SslMode = "none";
        }

        public MySqlSettings(string host, int port, string databaseName, string username, string password, string sslMode)
        {
            this.Host = host;
            this.Port = port;
            this.DatabaseName = databaseName;
            this.UserName = username;
            this.Password = password;
            this.SslMode = sslMode;
        }
    }
}
