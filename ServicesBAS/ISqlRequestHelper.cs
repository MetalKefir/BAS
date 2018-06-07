using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace ServicesBAS
{
    public interface ISqlRequestHelper<T>
    {
        int CommandsResult { get; }

        void CUDQuery(List<SqlCommand> listSqlCommands, [CallerMemberName] string callerName = null);

        void CUDQuery(SqlCommand sqlCommand, [CallerMemberName] string callerName = null);

        IEnumerable<T> ReadQuery(SqlCommand command, Func<SqlDataReader, T> DataReaderConverter, [CallerMemberName] string callerName = null);
    }
}