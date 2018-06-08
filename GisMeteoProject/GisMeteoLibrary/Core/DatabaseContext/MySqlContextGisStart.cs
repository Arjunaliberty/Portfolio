using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GisMeteoLibrary.Core.Abstract;
using GisMeteoLibrary.Models;
using MySql.Data.MySqlClient;

namespace GisMeteoLibrary.Core.DatabaseContext
{
    public class MySqlContextGisStart : IMySqlContextd<Info>
    {
        private MySqlGetConnect db;

        public MySqlContextGisStart(MySqlGetConnect db)
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

        public void Insert(List<Info> param)
        {
            string sqlInsert = "INSERT INTO  gis_database.info(city, link) VALUES (@city, @link)";
            string sqlCheck = "SELECT `city` FROM gis_database.info WHERE `city`= @city";
            MySqlConnection connections = db.GetConnection();
           
            try
            {
                connections.Open();
                foreach (var item in param)
                {
                    MySqlCommand commandCheck = new MySqlCommand(sqlCheck, connections);
                    MySqlParameter cityCheck = new MySqlParameter("@city", MySqlDbType.String);
                    cityCheck.Value = item.City;
                    commandCheck.Parameters.Add(cityCheck);

                    using(DbDataReader reader = commandCheck.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {

                            MySqlConnection connectionInsert = db.GetConnection();

                            try
                            {
                                connectionInsert.Open();

                                MySqlCommand commandInsert = new MySqlCommand(sqlInsert, connectionInsert);

                                MySqlParameter cityIsert = new MySqlParameter("@city", MySqlDbType.String);
                                cityIsert.Value = item.City;
                                commandInsert.Parameters.Add(cityIsert);

                                MySqlParameter linkInsert = new MySqlParameter("@link", MySqlDbType.String);
                                linkInsert.Value = item.Link;
                                commandInsert.Parameters.Add(linkInsert);

                                commandInsert.ExecuteNonQuery();
                            }
                            finally
                            {
                                connectionInsert.Close();
                                connectionInsert.Dispose();
                                connectionInsert = null;
                            }
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
        }

        public void Update(List<Info> param)
        {
            
        }

        public void Delete(Info param)
        {
           
        }
    }
}
