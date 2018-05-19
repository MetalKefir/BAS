using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Reflection;

namespace BAS
{
    namespace ServicesBAS
    {
        public class SqlRequestHelper<T> : ISqlRequestHelper<T> where T : class
        {
            public int CommandsResult { get; private set; }

            public void CUDQuery(string connectionString, List<SqlCommand> listSqlCommands)
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    foreach (var command in listSqlCommands)
                    {
                        command.Connection = connection;

                        command.ExecuteNonQuery();

                        CommandsResult++;
                    }
                }
            }

            public void CUDQuery(string connectionString, SqlCommand sqlCommand)
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    sqlCommand.Connection = connection;

                    CommandsResult = sqlCommand.ExecuteNonQuery();

                    if (sqlCommand.CommandText.Contains("insert"))
                        CommandsResult = (int)sqlCommand.Parameters["@Id"].Value;
                }
            }

            public IEnumerable<T> ReadQuery(string connectionString, SqlCommand command, Func<SqlDataReader, T> DataReaderConverter)
            {
                SqlDataReader reader = null;

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    command.Connection = connection;

                    reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            T newRecord = DataReaderConverter(reader);

                            yield return newRecord;
                        }
                    }
                }

                reader.Close();
            }
        }
    }
}