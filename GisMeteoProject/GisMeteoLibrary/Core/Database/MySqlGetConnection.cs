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
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="settings">Объект типа MySqlSettings</param>
        public MySqlGetConnection(MySqlSettings settings)
        {
            this.Settings = settings;
        }
        /// <summary>
        /// Метод создающий подключения
        /// </summary>
        /// <returns>Объект типа MySqlConnection</returns>
        public MySqlConnection GetConnection()
        {
            MySqlConnection connection = new MySqlConnection(Settings.ConnectionString);

            return connection;
        }




    }
}
