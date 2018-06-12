using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Newtonsoft.Json;

namespace WcfGisService
{
    /// <summary>
    /// WCF сервис для работы с БД gis_database
    /// </summary>
    public class GisService : IGisService
    {
        private static string connectionString = "Server='localhost'; Database='gis_database'; port='3306';User Id='root';SslMode='none'";
        private MySqlConnection connection = new MySqlConnection(connectionString);

        public string GetAllCity()
        {
            string result = null;
            string sqlSelect = "SELECT * FROM gis_database.info";
            List<Info> infoData;
            
            try
            {
                if(connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(sqlSelect, connection);

                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        if(reader.HasRows)
                        {
                            infoData = new List<Info>();
                            while (reader.Read())
                            {
                                Info info = new Info
                                {
                                    Id = (int)reader.GetValue(0),
                                    City = (string)reader.GetValue(1),
                                    Link = (string)reader.GetValue(2),
                                };

                                infoData.Add(info);
                            }

                            result = JsonConvert.SerializeObject(infoData, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                        }
                        else
                        {
                            result = null;
                        }
                    }
                }
            }
            finally
            {
                connection.Close();
            }


            return result;
        }

        public string GetWeatherCity(int id)
        {
            string result = null;
            string sqlSelect = "SELECT i.Id, i.City, i.Link, w.Condition, w.Date, w.TempMin, w.TempMax, w.Precipitation FROM gis_database.info i JOIN gis_database.weather w ON i.Id = w.Info_Id WHERE i.Id=@id";
            ModelDatabase database;

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                MySqlCommand command = new MySqlCommand(sqlSelect, connection);
                MySqlParameter parameterId = new MySqlParameter("@id", MySqlDbType.Int32);
                parameterId.Value = id;
                command.Parameters.Add(parameterId);

                using (DbDataReader reader = command.ExecuteReader())
                {
                    if(reader.HasRows)
                    {
                        database = new ModelDatabase();
                        while (reader.Read())
                        {


                            database.Info = new Info
                            {
                                Id = (int)reader.GetValue(0),
                                City = (string)reader.GetValue(1),
                                Link = (string)reader.GetValue(2)
                            };
                            database.Weather = new Weather
                            {
                                WeatherCondition = (string)reader.GetValue(3),
                                Date = (string)reader.GetValue(4),
                                TemperatureMin = (string)reader.GetValue(5),
                                TemperatureMax = (string)reader.GetValue(6),
                                Precipitation = (string)reader.GetValue(7)
                            };
                        }

                        result = JsonConvert.SerializeObject(database, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                    }
                    else
                    {
                        result = null;
                    }
                }
            }
            finally
            {
                connection.Close();
            }

            return result;
        }
    }
    /// <summary>
    /// Модель строки из базы данных
    /// </summary>
    public class ModelDatabase
    {
        public Info Info { get; set; }
        public Weather Weather { get; set; }
    }
    /// <summary>
    /// Модель таблицы info
    /// </summary>
    public class Info
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Link { get; set; }
    }
    /// <summary>
    /// Модель таблицы weather
    /// </summary>
    public class Weather
    {
        public int Id { get; set; }
        public string WeatherCondition { get; set; }
        public string Date { get; set; }
        public string TemperatureMin { get; set; }
        public string TemperatureMax { get; set; }
        public string Precipitation { get; set; }
        public int? InfoId { get; set; }
    }
}
