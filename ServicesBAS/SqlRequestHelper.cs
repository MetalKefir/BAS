using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ServicesBAS
{
    public class SqlRequestHelper<T> : ISqlRequestHelper<T>
    {
        private int commandResult;
        private readonly string connectionString;

        public int CommandsResult
        {
            get
            {
                return commandResult;
            }
        }

        public SqlRequestHelper()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public void CUDQuery(List<SqlCommand> listSqlCommands, [CallerMemberName] string callerName = null)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (var command in listSqlCommands)
                {
                    command.Connection = connection;

                    command.ExecuteNonQuery();

                    commandResult++;
                }
            }
        }

        public void CUDQuery(SqlCommand sqlCommand, [CallerMemberName] string callerName = null)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                sqlCommand.Connection = connection;

                commandResult = sqlCommand.ExecuteNonQuery();

                if (callerName == "Create")
                    commandResult = (int)sqlCommand.Parameters["@Id"].Value;
            }
        }

        public IEnumerable<T> ReadQuery(SqlCommand command, Func<SqlDataReader, T> DataReaderConverter, [CallerMemberName] string callerName = null)
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