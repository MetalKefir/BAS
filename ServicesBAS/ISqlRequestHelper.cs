using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAS
{
    namespace ServicesBAS
    {
        public interface ISqlRequestHelper<T>
        {
            void CUDQuery(string connectionString, List<SqlCommand> listSqlCommands);

            void CUDQuery(string connectionString, SqlCommand sqlCommand);

            IEnumerable<T> ReadQuery(string connectionString, SqlCommand command, Func<SqlDataReader, T> DataReaderConverter);
        }
    }
}