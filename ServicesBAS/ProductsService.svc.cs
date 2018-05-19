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
        public sealed class ProductsService : BaseService<Product>, IProductsServiceContract
        {
            protected override Func<SqlDataReader, Product> DataReaderConverter { get; set; }

            public ProductsService() : base(typeof(Product))
            {
                DataReaderConverter = (SqlDataReader reader) =>
                {
                    Product product = new Product
                    {
                        Articulus = Convert.IsDBNull(reader["id"]) ? null : (int?)Convert.ToUInt32(reader["id"]),
                        Name = Convert.ToString(reader["Name"]),
                        Color = Convert.ToString(reader["Color"]),
                        Manufacturer = Convert.ToString(reader["Manufacturer"]),
                        Price = Convert.ToDecimal(reader["Price"]),
                        Sale = Convert.IsDBNull(reader["Sale"]) ? null : (ushort?)Convert.ToUInt16(reader["Sale"]),
                        Quantity = Convert.ToUInt32(reader["Quantity"]),
                        Description = Convert.ToString(reader["Description"])
                    };

                    return product;
                };
            }

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

                SqlCommand command = new SqlCommand(storedProcedure["create"])
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
                    command.Parameters.AddRange(sqlParam);

                command.Parameters.Add("@Id", SqlDbType.Int);
                command.Parameters["@Id"].Direction = ParameterDirection.Output;

                RequestHelper.CUDQuery(connectionString, command);
                result.messeage = RequestHelper.CommandsResult;

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

                SqlCommand command = new SqlCommand(storedProcedure["delete"])
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("@ids", SqlDbType.Structured);
                command.Parameters["@ids"].TypeName = "intTable";
                command.Parameters["@ids"].Value = listOfDelete;

                RequestHelper.CUDQuery(connectionString, command);
                result.messeage = "Delete " + RequestHelper.CommandsResult + " obj";

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

                List<SqlParameter[]> listSqlParameters = new List<SqlParameter[]>();
                List<SqlCommand> listSqlCommands = new List<SqlCommand>();

                foreach (var product in products)
                {
                    var sqlPrametrs = GetProcParameters(product);
                    sqlPrametrs.Add(new SqlParameter() { ParameterName = "@prodid", Value = product.Articulus, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

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

                RequestHelper.CUDQuery(connectionString, listSqlCommands);
                result.messeage = "Update " + RequestHelper.CommandsResult + " obj";

                return result;
            }

            public ICollection<Product> GetAll()
            {
                ICollection<Product> products = new List<Product>();

                SqlCommand command = new SqlCommand(storedProcedure["getall"])
                {
                    CommandType = CommandType.StoredProcedure
                };

                foreach (var product in RequestHelper.ReadQuery(connectionString, command, DataReaderConverter))
                {
                    products.Add(product);
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