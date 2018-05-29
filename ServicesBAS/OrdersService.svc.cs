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
        public sealed class OrdersService : BaseService<Order>, IOrdersServiceContract
        {
            protected override Func<SqlDataReader, Order> DataReaderConverter { get; set; }

            public OrdersService() : base(typeof(Order))
            {
                DataReaderConverter = (SqlDataReader reader) =>
                {
                    Order order = new Order
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        OrderCustomer = new Customer() { Id = Convert.ToInt32(reader["CustomerID"]) },
                        DateOrder = Convert.ToDateTime(reader["Date"]),
                        DeliveryService = new DeliveriService(Convert.ToString(reader["DeliveryService"])),
                        TotalSum = Convert.ToInt64(reader["TotalSum"]),
                        Comment = Convert.ToString(reader["comment"])
                    };

                    return order;
                };
            }

            private static List<SqlParameter> GetProcParameters(Order order) => new List<SqlParameter>
            {
                new SqlParameter() { ParameterName = "@customerId", Value = order.OrderCustomer.Id, SqlDbType = SqlDbType.Int},
                new SqlParameter() { ParameterName = "@deliveryService", Value = order.DeliveryService.ServiceName},
                new SqlParameter() { ParameterName = "@date", Value = order.DateOrder, SqlDbType = SqlDbType.DateTime},
                new SqlParameter() { ParameterName = "@totalsum", Value = order.TotalSum, SqlDbType = SqlDbType.Money},
                new SqlParameter() { ParameterName = "@comment", Value = order.Comment}
            };

            public (bool IsSuccessful, object messeage) Create(Order order)
            {
                (bool IsSuccessful, object messeage) result = (IsSuccessful: true, messeage: "");

                if (order == null)
                {
                    result.IsSuccessful = false;
                    result.messeage = "NullRef";

                    return result;
                }

                SqlCommand command = new SqlCommand(storedProcedure["create"])
                {
                    CommandType = CommandType.StoredProcedure
                };

                var sqlParam = GetProcParameters(order)?.ToArray();
                if (sqlParam == null)
                {
                    result.IsSuccessful = false;
                    result.messeage = "Failure";
                    return result;
                }
                else
                    command.Parameters.AddRange(sqlParam);

                DataTable orderedProducts = new DataTable();
                orderedProducts.Columns.Add("Id", typeof(int));
                orderedProducts.Columns.Add("Quantity", typeof(int));

                foreach (var orderedProduct in order.OrderList)
                {
                    orderedProducts.Rows.Add(orderedProduct.Product.Articulus, orderedProduct.Quantity);
                }

                command.Parameters.Add("@Id", SqlDbType.Int);
                command.Parameters["@Id"].Direction = ParameterDirection.Output;

                command.Parameters.Add("@products", SqlDbType.Structured);
                command.Parameters["@products"].TypeName = "OrderedProduct";
                command.Parameters["@products"].Value = orderedProducts;

                RequestHelper.CUDQuery(command);
                result.messeage = RequestHelper.CommandsResult;

                return result;
            }

            public (bool IsSuccessful, string messeage) Delete(ICollection<Order> oreders)
            {
                (bool IsSuccessful, string messeage) result = (IsSuccessful: true, messeage: "");

                if (oreders == null || oreders.Count == 0)
                {
                    result.IsSuccessful = false;
                    result.messeage = "List of Parameters empty";

                    return result;
                }

                DataTable listOfDelete = new DataTable();
                listOfDelete.Columns.Add("id", typeof(int));

                foreach (var order in oreders)
                {
                    listOfDelete.Rows.Add(order.Id);
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

            public (bool IsSuccessful, string messeage) Update(ICollection<Order> orders)
            {
                (bool IsSuccessful, string messeage) result = (IsSuccessful: true, messeage: "");

                if (orders == null || orders.Count == 0)
                {
                    result.IsSuccessful = false;
                    result.messeage = "List of Parameters empty";

                    return result;
                }

                DataTable orderedProducts = new DataTable();
                orderedProducts.Columns.Add("Id", typeof(int));
                orderedProducts.Columns.Add("Quantity", typeof(int));

                DataTable orderedStatuses = new DataTable();
                orderedStatuses.Columns.Add("Status", typeof(string));
                orderedStatuses.Columns.Add("Date", typeof(DateTime));

                List<SqlParameter[]> listSqlParameters = new List<SqlParameter[]>();
                List<SqlCommand> listSqlCommands = new List<SqlCommand>();

                foreach (var order in orders)
                {
                    foreach (var orderedProduct in order.OrderList)
                    {
                        orderedProducts.Rows.Add(orderedProduct.Product.Articulus, orderedProduct.Quantity);
                    }

                    foreach (var orderStatus in order.OrderStatuses)
                    {
                        orderedStatuses.Rows.Add(orderStatus.Status.StatusName, orderStatus.DateChange);
                    }

                    var sqlPrametrs = GetProcParameters(order);
                    sqlPrametrs.AddRange
                    (
                        new List<SqlParameter>
                        { 
                            new SqlParameter()
                            {
                                ParameterName = "@orderId",
                                Value = order.Id,
                                SqlDbType = SqlDbType.Int
                            },
                            new SqlParameter()
                            {
                                ParameterName = "@products",
                                Value = orderedProducts,
                                SqlDbType = SqlDbType.Structured
                            },
                            new SqlParameter()
                            {
                                ParameterName = "@statusesList",
                                Value = orderedStatuses,
                                SqlDbType = SqlDbType.Structured
                            }
                        }
                    );

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

            public ICollection<Order> GetAll()
            {
                ICollection<Order> orders = new List<Order>();

                SqlCommand command = new SqlCommand(storedProcedure["getall"])
                {
                    CommandType = CommandType.StoredProcedure
                };

                foreach (var oreder in RequestHelper.ReadQuery(command, DataReaderConverter))
                {
                    orders.Add(oreder);
                }

                return orders;
            }

            public ICollection<Order> GetBy(string fieldName, object value)
            {
                throw new NotImplementedException();
            }

            public ICollection<OrderStatus> GetOrderStatuses(int orderId)
            {
                throw new NotImplementedException();
            }
        }
    }
}