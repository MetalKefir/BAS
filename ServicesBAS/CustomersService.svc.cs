using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BAS.ServiceContractLibrary;
using BAS.DataModelLibrary;
using System.Data.SqlClient;
using System.Data;

namespace BAS
{
    namespace ServicesBAS
    {
        public sealed class CustomersService : BaseService<Customer>, ICustomersServiceContract
        {
            protected override Func<SqlDataReader, Customer> DataReaderConverter { get; set; }

            public CustomersService() : base(typeof(Customer))
            {
                DataReaderConverter = (SqlDataReader reader) =>
                {
                    Customer customer = new Customer
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Age = Convert.ToByte(reader["Age"]),
                        FName = Convert.ToString(reader["FName"]),
                        MName = Convert.ToString(reader["MName"]),
                        LName = Convert.ToString(reader["LName"]),
                        CustomerAddress = Convert.ToString(reader["Address"]),
                        PhoneNumber = Convert.ToString(reader["PhoneNumber"]),
                        Email = Convert.ToString(reader["Email"])
                    };

                    return customer;
                };
            }

            private static List<SqlParameter> GetProcParameters(Customer customer) => new List<SqlParameter>
            {
                new SqlParameter() { ParameterName = "@age", Value = customer.Age, SqlDbType = SqlDbType.TinyInt},
                new SqlParameter() { ParameterName = "@fName", Value = customer.FName},
                new SqlParameter() { ParameterName = "@lName", Value = customer.LName},
                new SqlParameter() { ParameterName = "@mName", Value = customer.MName},
                new SqlParameter() { ParameterName = "@address", Value = customer.CustomerAddress},
                new SqlParameter() { ParameterName = "@phoneNumber", Value = customer.PhoneNumber},
                new SqlParameter() { ParameterName = "@email", Value = customer.Email}
            };

            public (bool IsSuccessful, object messeage) Create(Customer customer)
            {
                (bool IsSuccessful, object messeage) result = (IsSuccessful: true, messeage: "");

                if (customer == null)
                { 
                result.IsSuccessful = false;
                result.messeage = "NullRef";

                return result;
                }

                SqlCommand command = new SqlCommand(storedProcedure["create"])
                {
                    CommandType = CommandType.StoredProcedure
                };

                var sqlParam = GetProcParameters(customer)?.ToArray();
                if (sqlParam == null)
                {
                    result.IsSuccessful = false;
                    result.messeage = "Failure";
                    return result;
                }
                else
                    command.Parameters.AddRange(sqlParam);

                command.Parameters.Add("@Id", SqlDbType.Int);
                command.Parameters["@Id"].Direction = ParameterDirection.Output;

                RequestHelper.CUDQuery(command);
                result.messeage = RequestHelper.CommandsResult;

                return result;
            }

            public (bool IsSuccessful, string messeage) Delete(ICollection<Customer> customers)
            {
                (bool IsSuccessful, string messeage) result = (IsSuccessful: true, messeage: "");

                if (customers==null || customers.Count == 0)
                {
                    result.IsSuccessful = false;
                    result.messeage = "List of Parameters empty";

                    return result;
                }

                DataTable listOfDelete = new DataTable();
                listOfDelete.Columns.Add("id", typeof(int));

                foreach (var customer in customers)
                {
                    listOfDelete.Rows.Add(customer.Id);
                }

                SqlCommand command = new SqlCommand(storedProcedure["delete"])
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("@ids", SqlDbType.Structured);
                command.Parameters["@ids"].TypeName = "intTable";
                command.Parameters["@ids"].Value = listOfDelete;

                RequestHelper.CUDQuery(command);
                result.messeage = "Delete " + RequestHelper.CommandsResult + " obj";

                return result;
            }

            public (bool IsSuccessful, string messeage) Update(ICollection<Customer> customers)
            {
                (bool IsSuccessful, string messeage) result = (IsSuccessful: true, messeage: "");

                if (customers == null || customers.Count == 0)
                {
                    result.IsSuccessful = false;
                    result.messeage = "List of Parameters empty";

                    return result;
                }

                List<SqlParameter[]> listSqlParameters = new List<SqlParameter[]>();
                List<SqlCommand> listSqlCommands = new List<SqlCommand>();

                foreach (var customer in customers)
                {
                    var sqlPrametrs = GetProcParameters(customer);
                    sqlPrametrs.Add(new SqlParameter() { ParameterName = "@cusid", Value = customer.Id, SqlDbType = SqlDbType.Int});

                    listSqlParameters.Add(sqlPrametrs?.ToArray());
                }
                
                foreach (var sqlParameters in listSqlParameters)
                {
                    listSqlCommands.Add(new SqlCommand()
                    {
                        CommandText = storedProcedure["update"],
                        CommandType = CommandType.StoredProcedure,
                    });

                    listSqlCommands[listSqlCommands.Count - 1].Parameters.AddRange(sqlParameters);
                }

                RequestHelper.CUDQuery(listSqlCommands);
                result.messeage = "Update " + RequestHelper.CommandsResult + " obj";

                return result;
            }

            public ICollection<Customer> GetAll()
            {
                ICollection<Customer> customers = new List<Customer>();

                SqlCommand command = new SqlCommand(storedProcedure["getall"])
                {
                    CommandType = CommandType.StoredProcedure
                };

                foreach (var customer in RequestHelper.ReadQuery(command, DataReaderConverter))
                {
                    customers.Add(customer);
                }

                return customers;
            }

            public ICollection<Customer> GetBy(string fieldName, object value)
            {
                throw new NotImplementedException();
            }
        }
    }
}