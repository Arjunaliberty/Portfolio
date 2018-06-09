using GisMeteoLibrary.Core.Abstract;
using GisMeteoLibrary.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GisMeteoLibrary.Core.Database
{
    public class MySqlContext : IMySqlContext<MySqlDatabase>
    {
        MySqlConnection connection;

        public MySqlContext(MySqlConnection connection)
        {
            this.connection = connection;    
        }

        /// <summary>
        /// Получение данных из БД по id
        /// </summary>
        /// <param name="id">Id записи</param>
        /// <param name="selectTable">Таблица для выборки</param>
        /// <returns>Возвращает MySqlDatabase или null если запись не найдена</returns>
        public MySqlDatabase GetItem(int id, SelectTable selectTable)
        {
            string sqlSelect;
            MySqlDatabase result;

            switch (selectTable)
            {
                case SelectTable.Informations:
                    sqlSelect = "SELECT * FROM info WEHER `id`=@id";

                    try
                    {
                        connection.Open();

                        MySqlCommand command = new MySqlCommand(sqlSelect, connection);
                        MySqlParameter parameterId = new MySqlParameter("@id", MySqlDbType.Int32);
                        parameterId.Value = id;
                        command.Parameters.Add(parameterId);

                        using(DbDataReader reader = command.ExecuteReader())
                        {
                            if(reader.HasRows)
                            {
                                result = new MySqlDatabase();
                                while (reader.Read())
                                {
                                    result.Informations.Id = (int)reader.GetValue(0);
                                    result.Informations.City = (string)reader.GetValue(1);
                                    result.Informations.Link = (string)reader.GetValue(2);
                                }
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

                    break;
                case SelectTable.Weathers:
                    sqlSelect = "SELECT * FROM weather WEHER `id`= @id";

                    try
                    {
                        connection.Open();

                        MySqlCommand command = new MySqlCommand(sqlSelect, connection);
                        MySqlParameter parameterId = new MySqlParameter("@id", MySqlDbType.Int32);
                        parameterId.Value = id;
                        command.Parameters.Add(parameterId);

                        using (DbDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                result = new MySqlDatabase();
                                while (reader.Read())
                                {
                                    result.Weathers.Id = (int)reader.GetValue(0);
                                    result.Weathers.WeatherCondition = (string)reader.GetValue(1);
                                    result.Weathers.Date = (string)reader.GetValue(2);
                                    result.Weathers.TemperatureMin = (string)reader.GetValue(3);
                                    result.Weathers.TemperatureMax = (string)reader.GetValue(4);
                                    result.Weathers.Precipitation = (string)reader.GetValue(5);
                                    result.Weathers.InfoId = (int)reader.GetValue(6);
                                }
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
                
                    break;
                case SelectTable.All:
                    sqlSelect = "SELECT * FROM info JOIN weather ON info.Id=weather.Info_Id WHERE info.Id=@id";

                    try
                    {
                        connection.Open();

                        MySqlCommand command = new MySqlCommand(sqlSelect, connection);
                        MySqlParameter parameterId = new MySqlParameter("@id", MySqlDbType.Int32);
                        parameterId.Value = id;
                        command.Parameters.Add(parameterId);

                        using(DbDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                result = new MySqlDatabase();

                                while (reader.Read())
                                {
                                    result.Informations.Id = (int)reader.GetValue(0);
                                    result.Informations.City = (string)reader.GetValue(1);
                                    result.Informations.Link = (string)reader.GetValue(2);
                                    result.Weathers.Id = (int)reader.GetValue(3);
                                    result.Weathers.WeatherCondition = (string)reader.GetValue(4);
                                    result.Weathers.Date = (string)reader.GetValue(5);
                                    result.Weathers.TemperatureMin = (string)reader.GetValue(6);
                                    result.Weathers.TemperatureMax = (string)reader.GetValue(7);
                                    result.Weathers.Precipitation = (string)reader.GetValue(8);
                                    result.Weathers.InfoId = (int)reader.GetValue(9);
                                }
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

                    break;
                default:
                    result = null;
                    break;
            }

            return result;
        }

        public List<MySqlDatabase> GetItems(SelectTable selectTable)
        {
            string sqlSelect;
            List<MySqlDatabase> result;

            switch (selectTable)
            {
                case SelectTable.Informations:
                    sqlSelect = "SELECT * FROM info WEHER `id`=@id";

                    try
                    {
                        connection.Open();

                        MySqlCommand command = new MySqlCommand(sqlSelect, connection);
                        
                        using (DbDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                result = new List<MySqlDatabase>();
                                while (reader.Read())
                                {
                                    MySqlDatabase database = new MySqlDatabase
                                    {
                                        Informations = new Info
                                        {
                                            Id = (int)reader.GetValue(0),
                                            City = (string)reader.GetValue(1),
                                            Link = (string)reader.GetValue(2),
                                        }   
                                    };

                                    result.Add(database);
                                }
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

                    break;
                case SelectTable.Weathers:
                    sqlSelect = "SELECT * FROM weather WEHER `id`= @id";

                    try
                    {
                        connection.Open();

                        MySqlCommand command = new MySqlCommand(sqlSelect, connection);

                        using (DbDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                result = new List<MySqlDatabase>();
                                while (reader.Read())
                                {
                                    MySqlDatabase database = new MySqlDatabase
                                    {
                                        Weathers = new Weather
                                        {
                                            Id = (int)reader.GetValue(0),
                                            WeatherCondition = (string)reader.GetValue(1),
                                            Date = (string)reader.GetValue(2),
                                            TemperatureMin = (string)reader.GetValue(3),
                                            TemperatureMax = (string)reader.GetValue(4),
                                            Precipitation = (string)reader.GetValue(5),
                                            InfoId = (int)reader.GetValue(6)
                                        }
                                    };

                                    result.Add(database);
                                }
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

                    break;
                case SelectTable.All:
                    sqlSelect = "SELECT * FROM info JOIN weather ON info.Id=weather.Info_Id WHERE info.Id=@id";

                    try
                    {
                        connection.Open();

                        MySqlCommand command = new MySqlCommand(sqlSelect, connection);
                       
                        using (DbDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                result = new List<MySqlDatabase>();

                                while (reader.Read())
                                {
                                    MySqlDatabase database = new MySqlDatabase
                                    {
                                        Informations = new Info
                                        {
                                            Id = (int)reader.GetValue(0),
                                            City = (string)reader.GetValue(1),
                                            Link = (string)reader.GetValue(2)
                                        },
                                        Weathers = new Weather
                                        {
                                            Id = (int)reader.GetValue(3),
                                            WeatherCondition = (string)reader.GetValue(4),
                                            Date = (string)reader.GetValue(5),
                                            TemperatureMin = (string)reader.GetValue(6),
                                            TemperatureMax = (string)reader.GetValue(7),
                                            Precipitation = (string)reader.GetValue(8),
                                            InfoId = (int)reader.GetValue(9)
                                        }
                                    };

                                    result.Add(database);
                                }
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

                    break;
                default:
                    result = null;
                    break;
            }

            return result;
        }

        public void Insert(MySqlDatabase param, SelectTable selectTable)
        {
            switch (selectTable)
            {
                case SelectTable.Informations:
                    break;
                case SelectTable.Weathers:
                    break;
                case SelectTable.All:
                    break;
                default:
                    break;
            }
        }

        public void Update(MySqlDatabase param, SelectTable selectTable)
        {
            switch (selectTable)
            {
                case SelectTable.Informations:
                    break;
                case SelectTable.Weathers:
                    break;
                case SelectTable.All:
                    break;
                default:
                    break;
            }
        }

        public void Delete(MySqlDatabase param, SelectTable selectTable)
        {
            switch (selectTable)
            {
                case SelectTable.Informations:
                    break;
                case SelectTable.Weathers:
                    break;
                case SelectTable.All:
                    break;
                default:
                    break;
            }
        }
    }
}
