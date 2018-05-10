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
        // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "CustomersService" в коде, SVC-файле и файле конфигурации.
        // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы CustomersService.svc или CustomersService.svc.cs в обозревателе решений и начните отладку.
        public sealed class CustomersService : BaseService, ICustomersServiceContract
        {
            
            public CustomersService() : base(typeof(Customer)) { }

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
                (bool IsSuccessful, object messeage) result = (IsSuccessful : true, messeage : "");

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(storedProcedure["create"], connection)
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
                        cmd.Parameters.AddRange(sqlParam);

                    cmd.Parameters.Add("@cusid", SqlDbType.Int);
                    cmd.Parameters["@cusid"].Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    result.messeage = cmd.Parameters["@cusid"].Value;
                }
                
                return result;
            }

            public (bool IsSuccessful, string messeage) Delete(ICollection<Customer> parametrs)
            {
                (bool IsSuccessful, string messeage) result = (IsSuccessful: true, messeage: "");

                if (parametrs==null || parametrs.Count == 0)
                {
                    result.IsSuccessful = false;
                    result.messeage = "List of parametrs empty";

                    return result;
                }

                DataTable listOfDelete = new DataTable();
                listOfDelete.Columns.Add("id", typeof(int));

                foreach (var cus in parametrs)
                {
                    listOfDelete.Rows.Add(cus.Id);
                }

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                 
                    SqlCommand cmd = new SqlCommand(storedProcedure["delete"], connection)
                    {
                        CommandType = CommandType.StoredProcedure 
                    };
                    cmd.Parameters.Add("@ids", SqlDbType.Structured);
                    cmd.Parameters["@ids"].TypeName = "intTable";
                    cmd.Parameters["@ids"].Value = listOfDelete;

                    int number = cmd.ExecuteNonQuery();

                    result.messeage = "Delete " + number.ToString() + " obj";
                }

                return result;
            }

            public (bool IsSuccessful, string messeage) Update(ICollection<Customer> parametrs)
            {
                (bool IsSuccessful, string messeage) result = (IsSuccessful: true, messeage: "");

                if (parametrs == null || parametrs.Count == 0)
                {
                    result.IsSuccessful = false;
                    result.messeage = "List of parametrs empty";

                    return result;
                }

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(storedProcedure["update"], connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    foreach (var customer in parametrs)
                    {
                        var sqlParam = GetProcParameters(customer)?.ToArray();
                        if (sqlParam == null)
                        {
                            result.IsSuccessful = false;
                            result.messeage = "Failure";
                            return result;
                        }
                        else
                            cmd.Parameters.AddRange(sqlParam);

                        cmd.Parameters.Add("@cusid", SqlDbType.Int);
                        cmd.Parameters["@cusid"].Direction = ParameterDirection.Input;
                        cmd.Parameters["@cusid"].Value = customer.Id;

                        int number = cmd.ExecuteNonQuery();
                        result.messeage = "Update " + number + " obj";
                    }
                }

                return result;
            }

            public ICollection<Customer> GetAll()
            {
                ICollection<Customer> customers = null;

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(storedProcedure["getall"], connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlDataReader reader = cmd.ExecuteReader();

                    customers = new List<Customer>();
                    Customer customer;

                    if (reader.HasRows)
                    {
                        customer = new Customer();
                        while (reader.Read())
                        {
                            customer.Id = Convert.ToInt32(reader["id"]);
                            customer.Age = Convert.ToByte(reader["Age"]);
                            customer.FName = Convert.ToString(reader["FName"]);
                            customer.MName = Convert.ToString(reader["MName"]);
                            customer.LName = Convert.ToString(reader["LName"]);
                            customer.CustomerAddress = Convert.ToString(reader["Address"]);
                            customer.PhoneNumber = Convert.ToString(reader["PhoneNumber"]);
                            customer.Email = Convert.ToString(reader["Email"]);

                            customers.Add(customer);
                            customer = new Customer();
                        }
                    }

                    reader.Close();
                }

                return customers;
            }

            public ICollection<Customer> GetBy(string fieldName, object parametr)
            {
                throw new NotImplementedException();
            }
        }
    }
}