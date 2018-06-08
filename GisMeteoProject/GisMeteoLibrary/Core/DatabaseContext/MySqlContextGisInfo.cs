using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using GisMeteoLibrary.Core.Abstract;
using GisMeteoLibrary.Models;
using MySql.Data.MySqlClient;

namespace GisMeteoLibrary.Core.DatabaseContext
{
    /// <summary>
    /// Класс - контекст для работы с БД
    /// </summary>
    public class MySqlContextGisInfo : IMySqlContextd<Info>
    {
        private MySqlGetConnect db;

        public MySqlContextGisInfo(MySqlGetConnect db)
        {
            this.db = db;
        }

        public Info GetItem(int id)
        {
            Info result = null;
            MySqlConnection connections = db.GetConnection();
            string sql = "SELECT * FROM gis_database.info WHERE `id`=@id";

            try
            {
                connections.Open();
                             
                MySqlCommand command = new MySqlCommand(sql, connections);

                MySqlParameter parameterId = new MySqlParameter("@id", SqlDbType.Int);
                parameterId.Value = id;
                command.Parameters.Add(parameterId);

                using (DbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result = new Info
                            {
                                Id = (int)reader.GetValue(0),
                                City = (string)reader.GetValue(1),
                                Link = (string)reader.GetValue(2)
                            };
                        }
                    }
                }
            }
            finally
            {
                connections.Close();
                connections.Dispose();
                connections = null;
            }
            
            return result;
        }

        public List<Info> GetItems()
        {
            List<Info> result = null;
            MySqlConnection connections = db.GetConnection();
            string sql = "SELECT * FROM gis_database.info";

            try
            {
                connections.Open();

                MySqlCommand command = new MySqlCommand(sql, connections);

                using (DbDataReader reader = command.ExecuteReader())
                {
                    if(reader.HasRows)
                    {
                        result = new List<Info>();
                        while (reader.Read())
                        {
                            result.Add(new Info
                            {
                                Id = (int)reader.GetValue(0),
                                City = (string)reader.GetValue(1),
                                Link = (string)reader.GetValue(2)
                            });
                        }
                    }
                }
            }
            finally
            {
                connections.Close();
                connections.Dispose();
                connections = null;
            }
            
            return result;
        }

        public void Insert(Info param)
        {
            string sqlInsert = "INSERT INTO  gis_database.info(city, link) VALUES (@city, @link)";

            MySqlConnection connectionInsert = db.GetConnection();
                      
            try
            {
                connectionInsert.Open();

                MySqlCommand commandInsert = new MySqlCommand(sqlInsert, connectionInsert);

                MySqlParameter parametrCity = new MySqlParameter("@city", MySqlDbType.String);
                parametrCity.Value = param.City;
                commandInsert.Parameters.Add(parametrCity);

                MySqlParameter parametrLink = new MySqlParameter("@link", MySqlDbType.String);
                parametrLink.Value = param.Link;
                commandInsert.Parameters.Add(parametrLink);

                commandInsert.ExecuteNonQuery();
            }
            finally
            {
                connectionInsert.Close();
                connectionInsert.Dispose();
                connectionInsert = null;
            }
        }

        public void Update(Info param)
        {

            string sqlUpdate = "UPDATE gis_database.info SET `city`=@city, `link`=@link WHERE `id`=@id";
            MySqlConnection connections = db.GetConnection();

            try
            {
                connections.Open();

                MySqlCommand commandUpdate = new MySqlCommand(sqlUpdate, connections);

                MySqlParameter parameterId = new MySqlParameter("@id", MySqlDbType.Int32);
                parameterId.Value = param.Id;
                commandUpdate.Parameters.Add(parameterId);
                    
                MySqlParameter parameterCity = new MySqlParameter("@city", MySqlDbType.String);
                parameterCity.Value = param.City;
                commandUpdate.Parameters.Add(parameterCity);

                MySqlParameter parameterLink = new MySqlParameter("@link", MySqlDbType.String);
                parameterLink.Value = param.Link;
                commandUpdate.Parameters.Add(parameterLink);

                commandUpdate.ExecuteNonQuery();
            }
            finally
            {
                connections.Close();
                connections.Dispose();
                connections = null;
            }
        }

        public void Delete(Info param)
        {
            string sqlDelete = "DELETE gis_database.info WHERE `id`=@id";
            MySqlConnection connections = db.GetConnection();

            try
            {
                connections.Open();

                MySqlCommand commandDelete = new MySqlCommand(sqlDelete, connections);
                MySqlParameter parameterId = new MySqlParameter("@id", MySqlDbType.Int32);
                parameterId.Value = param.Id;
                commandDelete.Parameters.Add(parameterId);

                commandDelete.ExecuteNonQuery();
            }
            finally
            {
                connections.Close();
                connections.Dispose();
                connections = null;
            }
        }
    }
}
