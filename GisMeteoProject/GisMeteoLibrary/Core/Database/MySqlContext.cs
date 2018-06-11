using GisMeteoLibrary.Core.Abstract;
using GisMeteoLibrary.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace GisMeteoLibrary.Core.Database
{
    /// <summary>
    /// Класс - контекст для работы с БД
    /// </summary>
    public class MySqlContext : IMySqlContext<MySqlDatabase>
    {
        MySqlConnection connection;
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="connection">Объект типа MySqlConnection</param>
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
                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }

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
                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }

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
        /// <summary>
        /// Получение списка данных из БД по id
        /// </summary>
        /// <param name="selectTable">Таблица для выборки</param>
        /// <returns>Возвращает List<MySqlDatabase> или null если запись не найдена</returns>
        public List<MySqlDatabase> GetItems(SelectTable selectTable)
        {
            string sqlSelect;
            List<MySqlDatabase> result;

            switch (selectTable)
            {
                case SelectTable.Informations:
                    sqlSelect = "SELECT * FROM info";

                    try
                    {
                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }

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
                    sqlSelect = "SELECT * FROM weather";

                    try
                    {
                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }

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
                    sqlSelect = "SELECT * FROM info JOIN weather ON info.Id=weather.Info_Id";

                    try
                    {
                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }

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
        /// <summary>
        /// Добавление данных в БД
        /// </summary>
        /// <param name="param">Параметр типа MySqlDatabase</param>
        /// <param name="selectTable">Таблица для встакви</param>
        public void Insert(MySqlDatabase param, SelectTable selectTable)
        {
            string sqlInsert;

            switch (selectTable)
            {
                case SelectTable.Informations:
                    if (param.Informations != null)
                    {
                        sqlInsert = "INSERT INTO info(city, link) VALUES(@city, @link)";
                        MySqlTransaction transaction = null;
                        try
                        {
                            if (connection.State == ConnectionState.Closed)
                            {
                                connection.Open();
                            }
                            
                            transaction = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
                            MySqlCommand command = new MySqlCommand(sqlInsert, connection, transaction);

                            MySqlParameter parameterCity = new MySqlParameter("@city", MySqlDbType.String);
                            parameterCity.Value = param.Informations.City;
                            command.Parameters.Add(parameterCity);

                            MySqlParameter parameterLink = new MySqlParameter("@link", MySqlDbType.String);
                            parameterLink.Value = param.Informations.Link;
                            command.Parameters.Add(parameterLink);

                            command.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw new Exception("Произошла ошибка при добавлении данных в БД");
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                    else
                    {
                        throw new Exception("Отсутсвует таблица для вставки");
                    }

                    break;
                case SelectTable.Weathers:
                    if (param.Weathers != null && param.Weathers.InfoId > 0)
                    {
                        sqlInsert = "INSERT INTO weather(Condition, Date, TempMin, TempMax, Precipitation, Info_Id) VALUES(@condition, @date, @tempMin, @tempMax, @precipitation, @info_Id)";
                        MySqlTransaction transaction = null;

                        try
                        {
                            if (connection.State == ConnectionState.Closed)
                            {
                                connection.Open();
                            }

                            transaction = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
                            MySqlCommand command = new MySqlCommand(sqlInsert, connection, transaction);

                            MySqlParameter parameterCondition = new MySqlParameter("@condition", MySqlDbType.String);
                            parameterCondition.Value = param.Weathers.WeatherCondition;
                            command.Parameters.Add(parameterCondition);

                            MySqlParameter parameterDate = new MySqlParameter("@date", MySqlDbType.String);
                            parameterDate.Value = param.Weathers.Date;
                            command.Parameters.Add(parameterDate);

                            MySqlParameter parameterTempMin = new MySqlParameter("@tempMin", MySqlDbType.String);
                            parameterTempMin.Value = param.Weathers.TemperatureMin;
                            command.Parameters.Add(parameterTempMin);

                            MySqlParameter parameterTempMax = new MySqlParameter("@tempMax", MySqlDbType.String);
                            parameterTempMax.Value = param.Weathers.TemperatureMax;
                            command.Parameters.Add(parameterTempMax);

                            MySqlParameter parameterPrecipitation = new MySqlParameter("@precipitation", MySqlDbType.String);
                            parameterPrecipitation.Value = param.Weathers.Precipitation;
                            command.Parameters.Add(parameterPrecipitation);

                            MySqlParameter parameterInfo_Id = new MySqlParameter("@info_Id", MySqlDbType.String);
                            parameterInfo_Id.Value = param.Weathers.InfoId;
                            command.Parameters.Add(parameterInfo_Id);

                            command.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw new Exception("Произошла ошибка при добавлении данных в БД");
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                    else
                    {
                        throw new Exception("Объект не существует либо отсуствует внешний ключ Info_Id");
                    }

                    break;
                case SelectTable.All:
                    string sqlInsertFirst = "INSERT INTO info(`city`, `link`) VALUES(@city, @link)";
                    string sqlInsertSecond = "INSERT INTO weather(`Condition`, `Date`, `TempMin`, `TempMax`, `Precipitation`, `Info_Id`) VALUES(@condition, @date, @tempMin, @tempMax, @precipitation, LAST_INSERT_ID())";

                    if(param.Informations != null && param.Weathers != null)
                    {
                        MySqlTransaction transaction = null;

                        try
                        {
                            if (connection.State == ConnectionState.Closed)
                            {
                                connection.Open();
                            }

                            transaction = connection.BeginTransaction();

                            MySqlCommand commandFirst = new MySqlCommand(sqlInsertFirst, connection, transaction);
                            MySqlParameter parameterCity = new MySqlParameter("@city", MySqlDbType.String);
                            parameterCity.Value = param.Informations.City;
                            commandFirst.Parameters.Add(parameterCity);
                            MySqlParameter parameterLink = new MySqlParameter("@link", MySqlDbType.String);
                            parameterLink.Value = param.Informations.Link;
                            commandFirst.Parameters.Add(parameterLink);
                            commandFirst.ExecuteNonQuery();

                            MySqlCommand commandSecond = new MySqlCommand(sqlInsertSecond, connection, transaction);
                            MySqlParameter parameterCondition = new MySqlParameter("@condition", MySqlDbType.String);
                            parameterCondition.Value = param.Weathers.WeatherCondition;
                            commandSecond.Parameters.Add(parameterCondition);
                            MySqlParameter parameterDate = new MySqlParameter("@date", MySqlDbType.String);
                            parameterDate.Value = param.Weathers.Date;
                            commandSecond.Parameters.Add(parameterDate);
                            MySqlParameter parameterTempMin = new MySqlParameter("@tempMin", MySqlDbType.String);
                            parameterTempMin.Value = param.Weathers.TemperatureMin;
                            commandSecond.Parameters.Add(parameterTempMin);
                            MySqlParameter parameterTempMax = new MySqlParameter("@tempMax", MySqlDbType.String);
                            parameterTempMax.Value = param.Weathers.TemperatureMax;
                            commandSecond.Parameters.Add(parameterTempMax);
                            MySqlParameter parameterPrecipitation = new MySqlParameter("@precipitation", MySqlDbType.String);
                            parameterPrecipitation.Value = param.Weathers.Precipitation;
                            commandSecond.Parameters.Add(parameterPrecipitation);
                            commandSecond.ExecuteNonQuery();

                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw new Exception("Ошибка при добавлении данных в БД");
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                    else
                    {
                        throw new Exception("Отсутсвует таблица для вставки");
                    }

                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Обновление данных в БД
        /// </summary>
        /// <param name="param">Параметр типа MySqlDatabase</param>
        /// <param name="selectTable">Таблица для обновления</param>
        public void Update(MySqlDatabase param, SelectTable selectTable)
        {
            switch (selectTable)
            {
                case SelectTable.Informations:
                    if(param.Informations != null && param.Informations.Id > 0)
                    {
                        string sqlUpdate = "UPDATE info i SET i.City=@city, i.Link=@link WHERE i.Id=@id";
                        MySqlTransaction transaction = null;

                        try
                        {
                            if (connection.State == ConnectionState.Closed)
                            {
                                connection.Open();
                            }

                            transaction = connection.BeginTransaction();

                            MySqlCommand command = new MySqlCommand(sqlUpdate, connection, transaction);

                            MySqlParameter parameterId = new MySqlParameter("@id", MySqlDbType.String);
                            parameterId.Value = param.Informations.Id;
                            command.Parameters.Add(parameterId);

                            MySqlParameter parameterCity = new MySqlParameter("@city", MySqlDbType.String);
                            parameterCity.Value = param.Informations.City;
                            command.Parameters.Add(parameterCity);

                            MySqlParameter parameterLink = new MySqlParameter("@link", MySqlDbType.String);
                            parameterLink.Value = param.Informations.Link;
                            command.Parameters.Add(parameterLink);

                            command.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw new Exception("Ошибка при обновлении данных в БД");
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                    else
                    {
                        throw new Exception("Отсутсвует таблица для вставки или неверный идентификатор");
                    }
                    break;
                case SelectTable.Weathers:
                    if (param.Weathers != null)
                    {
                        string sqlUpdate = "UPDATE weather w SET w.Condition=@condition, w.Date=@date, w.TempMin=@tempMin, w.TempMax=@tempMax, w.Precipitation=@precipitation WHERE w.Id=@id";
                        MySqlTransaction transaction = null;

                        try
                        {
                            if (connection.State == ConnectionState.Closed)
                            {
                                connection.Open();
                            }

                            transaction = connection.BeginTransaction();

                            MySqlCommand command = new MySqlCommand(sqlUpdate, connection, transaction);

                            MySqlParameter parameterId = new MySqlParameter("@id", MySqlDbType.String);
                            parameterId.Value = param.Weathers.Id;
                            command.Parameters.Add(parameterId);
                            MySqlParameter parameterCondition = new MySqlParameter("@condition", MySqlDbType.String);
                            parameterCondition.Value = param.Weathers.WeatherCondition;
                            command.Parameters.Add(parameterCondition);
                            MySqlParameter parameterDate = new MySqlParameter("@date", MySqlDbType.String);
                            parameterDate.Value = param.Weathers.Date;
                            command.Parameters.Add(parameterDate);
                            MySqlParameter parameterTempMin = new MySqlParameter("@tempMin", MySqlDbType.String);
                            parameterTempMin.Value = param.Weathers.TemperatureMin;
                            command.Parameters.Add(parameterTempMin);
                            MySqlParameter parameterTempMax = new MySqlParameter("@tempMax", MySqlDbType.String);
                            parameterTempMax.Value = param.Weathers.TemperatureMax;
                            command.Parameters.Add(parameterTempMax);
                            MySqlParameter parameterPrecipitation = new MySqlParameter("@precipitation", MySqlDbType.String);
                            parameterPrecipitation.Value = param.Weathers.Precipitation;
                            command.Parameters.Add(parameterPrecipitation);
                           
                            command.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw new Exception("Ошибка при обновлении данных в БД");
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                    else
                    {
                        throw new Exception("Отсутсвует таблица для вставки или неверный идентификатор");
                    }
                    break;
                case SelectTable.All:
                    if ((param.Informations != null && param.Informations.Id > 0) && (param.Weathers != null))
                    {
                        string sqlUpdateFirst = "UPDATE info i SET i.City = @city, i.Link = @link WHERE Id = @id";
                        string sqlUpdateSecond = "UPDATE weather w SET w.Condition=@condition, w.Date=@date, w.TempMin=@tempMin, w.TempMax=@tempMax, w.Precipitation=@precipitation WHERE w.Info_Id=@info_Id";
                        MySqlTransaction transaction = null;

                        try
                        {
                            if (connection.State == ConnectionState.Closed)
                            {
                                connection.Open();
                            }

                            transaction = connection.BeginTransaction();

                            MySqlCommand commandFirst = new MySqlCommand(sqlUpdateFirst, connection, transaction);
                            MySqlParameter parameterId = new MySqlParameter("@id", MySqlDbType.String);
                            parameterId.Value = param.Informations.Id;
                            commandFirst.Parameters.Add(parameterId);
                            MySqlParameter parameterCity = new MySqlParameter("@city", MySqlDbType.String);
                            parameterCity.Value = param.Informations.City;
                            commandFirst.Parameters.Add(parameterCity);
                            MySqlParameter parameterLink = new MySqlParameter("@link", MySqlDbType.String);
                            parameterLink.Value = param.Informations.Link;
                            commandFirst.Parameters.Add(parameterLink);
                            commandFirst.ExecuteNonQuery();

                            MySqlCommand commandSecond = new MySqlCommand(sqlUpdateSecond, connection, transaction);
                            MySqlParameter parameterCondition = new MySqlParameter("@condition", MySqlDbType.String);
                            parameterCondition.Value = param.Weathers.WeatherCondition;
                            commandSecond.Parameters.Add(parameterCondition);
                            MySqlParameter parameterDate = new MySqlParameter("@date", MySqlDbType.String);
                            parameterDate.Value = param.Weathers.Date;
                            commandSecond.Parameters.Add(parameterDate);
                            MySqlParameter parameterTempMin = new MySqlParameter("@tempMin", MySqlDbType.String);
                            parameterTempMin.Value = param.Weathers.TemperatureMin;
                            commandSecond.Parameters.Add(parameterTempMin);
                            MySqlParameter parameterTempMax = new MySqlParameter("@tempMax", MySqlDbType.String);
                            parameterTempMax.Value = param.Weathers.TemperatureMax;
                            commandSecond.Parameters.Add(parameterTempMax);
                            MySqlParameter parameterPrecipitation = new MySqlParameter("@precipitation", MySqlDbType.String);
                            parameterPrecipitation.Value = param.Weathers.Precipitation;
                            commandSecond.Parameters.Add(parameterPrecipitation);
                            MySqlParameter parameterInfo_Id = new MySqlParameter("@info_Id", MySqlDbType.String);
                            parameterInfo_Id.Value = param.Informations.Id;
                            commandSecond.Parameters.Add(parameterInfo_Id);
                            
                            commandSecond.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw new Exception("Ошибка при обновлении данных в БД");
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                    else
                    {
                        throw new Exception("Отсутсвует таблица для обновления или неверный идентификатор");
                    }
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Удаление данных из БД
        /// </summary>
        /// <param name="param">Параметр типа MySqlDatabase/param>
        public void Delete(MySqlDatabase param)
        {
            if(param.Informations !=null && param.Informations.Id > 0)
            {
                string sqlDelete = "DELETE FROM info WHERE `id`=@id";
                MySqlTransaction transaction = null;

                try
                {
                    if(connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    transaction = connection.BeginTransaction(IsolationLevel.Serializable);
                    MySqlCommand command = new MySqlCommand(sqlDelete, connection, transaction);

                    MySqlParameter parameterId = new MySqlParameter("@id", MySqlDbType.Int32);
                    parameterId.Value = param.Informations.Id;
                    command.Parameters.Add(parameterId);

                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw new Exception("Ошибка при удалении данных из БД");
                }
                finally
                {
                    connection.Close();
                }
            }
            else
            {
                throw new Exception("Объект не существует либо отсуствует параметр id главной таблицы");
            }
        }
    }
}
                                                                                                       