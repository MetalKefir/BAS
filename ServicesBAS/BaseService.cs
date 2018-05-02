﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Text;

namespace BAS
{
    namespace ServicesBAS
    {
        public abstract class BaseService
        {
            protected readonly string connectionString;
            protected readonly Dictionary<string, string> storedProcedure;

            public BaseService(Type entity)
            {
                connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                string entityName = new StringBuilder(entity.Name).Append("s").ToString();

                storedProcedure = new Dictionary<string, string>();
                storedProcedure.Add("create","insert" + entityName);
                storedProcedure.Add("update", "update" + entityName);
                storedProcedure.Add("delete", "delete" + entityName);
            }
        }
    }
}