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

            private static List<SqlParameter> GetCreateParameters(Customer customer)
            {
                return new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@age", Value = customer.Age, SqlDbType = SqlDbType.TinyInt},
                    new SqlParameter() { ParameterName = "@fName", Value = customer.FName},
                    new SqlParameter() { ParameterName = "@lName", Value = customer.LName},
                    new SqlParameter() { ParameterName = "@mName", Value = customer.MName},
                    new SqlParameter() { ParameterName = "@address", Value = customer.CustomerAddress},
                    new SqlParameter() { ParameterName = "@phoneNumber", Value = customer.PhoneNumber},
                    new SqlParameter() { ParameterName = "@email", Value = customer.Email},
                    new SqlParameter() { ParameterName = "@cusid", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output}
                };

            }

            public (bool IsSuccessful, string messeage) Create(ICollection<Customer> customers)
            {
                (bool IsSuccessful, string messeage) result = (IsSuccessful : true, messeage : "");

              
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    foreach (var customer in customers)
                    {
                        SqlCommand cmd = new SqlCommand(storedProcedure["create"], conn)
                        {
                            CommandType = CommandType.StoredProcedure
                        };

                        var sqlParam = GetCreateParameters(customer)?.ToArray();

                        if (sqlParam != null)
                            cmd.Parameters.AddRange(sqlParam);
                        else
                        {
                            result.IsSuccessful = false;
                            result.messeage = "Failure";
                            return result;
                        }


                        cmd.ExecuteNonQuery();
                        result.messeage = cmd.Parameters["@cusid"].Value.ToString();
                        
                    }
                }
                
                
                return result;

                throw new NotImplementedException();
            }

            public (bool IsSuccessful, string messeage) Delete(IReadOnlyCollection<Customer> parametr)
            {
                throw new NotImplementedException();
            }

            public ICollection<Customer> GetAll()
            {
                throw new NotImplementedException();
            }

            public ICollection<Customer> GetBy(string fieldName, object parametr)
            {
                throw new NotImplementedException();
            }

            public (bool IsSuccessful, string messeage) Update(IReadOnlyCollection<Customer> parametr)
            {
                throw new NotImplementedException();
            }

            public (bool IsSuccessful, string messeage) UpdateAddress(int customerId, string address)
            {
                throw new NotImplementedException();
            }
        }
    }
}