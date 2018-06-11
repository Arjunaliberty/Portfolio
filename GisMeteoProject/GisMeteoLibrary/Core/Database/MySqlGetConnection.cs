using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GisMeteoLibrary.Core.DatabaseContext
{
    public class MySqlGetConnection
    {
        private MySqlSettings Settings;

        public MySqlGetConnection(MySqlSettings settings)
        {
            this.Settings = settings;
        }

        public MySqlConnection GetConnection()
        {
            MySqlConnection connection = new MySqlConnection(Settings.ConnectionString);

            return connection;
        }




    }
}
