using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Text;
using BAS.ServiceContractLibrary;
using BAS.DataModelLibrary;
using System.Data.SqlClient;
using System.Data;

namespace BAS
{
    namespace ServicesBAS
    {
        public abstract class BaseService<T> where T: class
        {
            protected readonly string  connectionString;
            protected readonly Dictionary<string, string> storedProcedure;
            protected abstract Func<SqlDataReader, T> DataReaderConverter { get; set; }
            protected SqlRequestHelper<T> RequestHelper { get; set; }

            public BaseService(Type entity)
            {
                connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                string entityName = new StringBuilder(entity.Name).Append("s").ToString();

                storedProcedure = new Dictionary<string, string>
                {
                    { "create", "insert" + entityName },
                    { "getall", "getall" + entityName },
                    { "update", "Update" + entityName },
                    { "delete", "delete" + entityName }
                };

                RequestHelper = new SqlRequestHelper<T>();
            }
        }
    }
}