using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GisMeteoLibrary.Core.DatabaseContext
{
    public class MySqlGetConnect
    {
        private MySqlSettings Settings;

        public MySqlGetConnect(MySqlSettings settings)
        {
            this.Settings = settings;
        }

        public MySqlConnection GetConnection()
        {
            string connectionString = "Server=" + Settings.Host + ";Database=" + Settings.DatabaseName + ";port=" + Settings.Port + ";User Id=" + Settings.UserName + ";password=" + Settings.Password;
            MySqlConnection connection = new MySqlConnection(connectionString);

            return connection;
        }
    }
}
