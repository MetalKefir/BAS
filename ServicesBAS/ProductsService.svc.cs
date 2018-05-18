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
        // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
        // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.
        public sealed class ProductsService : BaseService, IProductsServiceContract
        {

            public ProductsService() : base(typeof(Product)) { }

            private static List<SqlParameter> GetProcParameters(Product product) => new List<SqlParameter>
            {
                new SqlParameter() { ParameterName = "@name", Value = product.Name},
                new SqlParameter() { ParameterName = "@manufacturer", Value = product.Manufacturer},
                new SqlParameter() { ParameterName = "@type", Value = product.Type},
                new SqlParameter() { ParameterName = "@color", Value = product.Color},
                new SqlParameter() { ParameterName = "@quantity", Value = product.Quantity, SqlDbType = SqlDbType.Int},
                new SqlParameter() { ParameterName = "@price", Value = product.Price, SqlDbType = SqlDbType.Money},
                new SqlParameter() { ParameterName = "@sale", Value = product.Sale,  SqlDbType = SqlDbType.TinyInt},
                new SqlParameter() { ParameterName = "@description", Value = product.Description}
            };

            public (bool IsSuccessful, object messeage) Create(Product product)
            {
                (bool IsSuccessful, object messeage) result = (IsSuccessful: true, messeage: "");

                if (product == null)
                {
                    result.IsSuccessful = false;
                    result.messeage = "NullRef";

                    return result;
                }

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(storedProcedure["create"], connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    var sqlParam = GetProcParameters(product)?.ToArray();
                    if (sqlParam == null)
                    {
                        result.IsSuccessful = false;
                        result.messeage = "Failure";
                        return result;
                    }
                    else
                        cmd.Parameters.AddRange(sqlParam);

                    cmd.Parameters.Add("@prodid", SqlDbType.Int);
                    cmd.Parameters["@prodid"].Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    result.messeage = cmd.Parameters["@prodid"].Value;
                }

                return result;
            }

            public (bool IsSuccessful, string messeage) Delete(ICollection<Product> products)
            {
                (bool IsSuccessful, string messeage) result = (IsSuccessful: true, messeage: "");

                if (products == null || products.Count == 0)
                {
                    result.IsSuccessful = false;
                    result.messeage = "List of Parameters empty";

                    return result;
                }

                DataTable listOfDelete = new DataTable();
                listOfDelete.Columns.Add("id", typeof(int));

                foreach (var customer in products)
                {
                    listOfDelete.Rows.Add(customer.Articulus);
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

            public (bool IsSuccessful, string messeage) Update(ICollection<Product> products)
            {
                (bool IsSuccessful, string messeage) result = (IsSuccessful: true, messeage: "");

                if (products == null || products.Count == 0)
                {
                    result.IsSuccessful = false;
                    result.messeage = "List of Parameters empty";

                    return result;
                }

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(storedProcedure["update"], connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlParameter[] sqlParam = null;
                    int changeCounter = 0;

                    foreach (var product in products)
                    {
                        sqlParam = GetProcParameters(product)?.ToArray();
                        if (sqlParam == null)
                        {
                            result.IsSuccessful = false;
                            result.messeage = "Failure";
                            return result;
                        }
                        else
                            cmd.Parameters.AddRange(sqlParam);

                        cmd.Parameters.Add("@prodid", SqlDbType.Int);
                        cmd.Parameters["@prodid"].Direction = ParameterDirection.Input;
                        cmd.Parameters["@prodid"].Value = product.Articulus;

                        changeCounter += cmd.ExecuteNonQuery();
                    }
                    result.messeage = "Update " + changeCounter + " obj";
                }

                return result;
            }

            public ICollection<Product> GetAll()
            {
                ICollection<Product> products = null;

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(storedProcedure["getall"], connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlDataReader reader = cmd.ExecuteReader();

                    products = new List<Product>();
                    Product product;

                    if (reader.HasRows)
                    {
                        product = new Product();
                        while (reader.Read())
                        {
                            product.Articulus = Convert.IsDBNull(reader["id"]) ? null : (int?)Convert.ToUInt32(reader["id"]);
                            product.Name = Convert.ToString(reader["Name"]);
                            product.Color = Convert.ToString(reader["Color"]);
                            product.Manufacturer = Convert.ToString(reader["Manufacturer"]);
                            product.Price = Convert.ToDecimal(reader["Price"]);
                            product.Sale = Convert.IsDBNull(reader["Sale"]) ? null : (ushort?)Convert.ToUInt16(reader["Sale"]);
                            product.Quantity = Convert.ToUInt32(reader["Quantity"]);
                            product.Description = Convert.ToString(reader["Description"]);
                            
                            products.Add(product);
                            product = new Product();
                        }
                    }

                    reader.Close();
                }

                return products;
            }

            public ICollection<Product> GetBy(string fieldName, object parameter)
            {
                throw new NotImplementedException();
            }

            public ICollection<Product> GetByPrice(int minprice, int? maxprice = null)
            {
                throw new NotImplementedException();
            }

        }
    }
}