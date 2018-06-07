using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Text;
using ServiceContractLibrary;
using DataModelLibrary;
using System.Data.SqlClient;
using System.Data;

namespace ServicesBAS
{
    public abstract class BaseService<T> where T: class
    {
        protected readonly Dictionary<string, string> storedProcedure;
        protected abstract Func<SqlDataReader, T> DataReaderConverter { get; set; }
        protected ISqlRequestHelper<T> RequestHelper { get; set; }

        public BaseService(Type entity)
        {
            string entityName = new StringBuilder(entity.Name).Append("s").ToString();

            storedProcedure = new Dictionary<string, string>
            {
                { "create", "insert" + entityName },
                { "getall", "getall" + entityName },
                { "getfromto", "getfromto" + entityName },
                { "update", "Update" + entityName },
                { "delete", "delete" + entityName }
            };

            RequestHelper = new SqlRequestHelper<T>();
        }
    }
}