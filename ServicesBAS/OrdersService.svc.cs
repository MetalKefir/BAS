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
        // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "OrdersService" в коде, SVC-файле и файле конфигурации.
        // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы OrdersService.svc или OrdersService.svc.cs в обозревателе решений и начните отладку.
        public sealed class OrdersService : BaseService, IOrdersServiceContract
        {

            public OrdersService() : base(typeof(Order)) { }

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

                SqlCommand cmd = new SqlCommand(storedProcedure["create"])
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
                    cmd.Parameters.AddRange(sqlParam);

                DataTable orderedProducts = new DataTable();
                orderedProducts.Columns.Add("Id", typeof(int));
                orderedProducts.Columns.Add("Quantity", typeof(int));

                foreach (var orderedProduct in order.OrderList)
                {
                    orderedProducts.Rows.Add(orderedProduct.Product.Articulus, orderedProduct.Quantity);
                }

                cmd.Parameters.Add("@orderId", SqlDbType.Int);
                cmd.Parameters["@orderId"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add("@products", SqlDbType.Structured);
                cmd.Parameters["@products"].TypeName = "OrderedProduct";
                cmd.Parameters["@products"].Value = orderedProducts;

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    cmd.Connection = connection;

                    cmd.ExecuteNonQuery();
                    result.messeage = cmd.Parameters["@orderId"].Value;
                }

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

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(storedProcedure["update"], connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    SqlParameter[] sqlParam = null;
                    int changeCounter = 0;

                    foreach (var order in orders)
                    {
                        sqlParam = GetProcParameters(order)?.ToArray();
                        if (sqlParam == null)
                        {
                            result.IsSuccessful = false;
                            result.messeage = "Failure";
                            return result;
                        }
                        else
                            cmd.Parameters.AddRange(sqlParam);

                        foreach (var orderedProduct in order.OrderList)
                        {
                            orderedProducts.Rows.Add(orderedProduct.Product.Articulus, orderedProduct.Quantity);
                        }

                        foreach (var orderStatus in order.OrderStatuses)
                        {
                            orderedStatuses.Rows.Add(orderStatus.Status.StatusName, orderStatus.DateChange);
                        }

                        cmd.Parameters.Add("@products", SqlDbType.Structured);
                        cmd.Parameters["@products"].TypeName = "OrderedProduct";
                        cmd.Parameters["@products"].Value = orderedProducts;

                        cmd.Parameters.Add("@statusesList", SqlDbType.Structured);
                        cmd.Parameters["@statusesList"].TypeName = "StatusesOrder";
                        cmd.Parameters["@statusesList"].Value = orderedStatuses;

                        cmd.Parameters.Add("@orderId", SqlDbType.Int);
                        cmd.Parameters["@orderId"].Direction = ParameterDirection.Input;
                        cmd.Parameters["@orderId"].Value = order.Id;

                        changeCounter += cmd.ExecuteNonQuery();

                        orderedProducts.Clear();
                        orderedStatuses.Clear();
                    }

                    result.messeage = "Update " + changeCounter + " obj";
                }
                /*
    @products OrderedProduct READONLY,
    @statusesList StatusesOrder READONLY,
 */
                return result;
            }

            public ICollection<Order> GetAll()
            {
                ICollection<Order> orders = null;

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand("GetAllOrders", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlDataReader reader = cmd.ExecuteReader();

                    orders = new List<Order>();
                    Order order;

                    if (reader.HasRows)
                    {
                        order = new Order();
                        while (reader.Read())
                        {
                            order.Id = Convert.ToInt32(reader["id"]);
                            order.OrderCustomer = new Customer() { Id = Convert.ToInt32(reader["CustomerID"]) };
                            order.DateOrder = Convert.ToDateTime(reader["Date"]);
                            order.DeliveryService = new DeliveriService(Convert.ToString(reader["DeliveryService"]));
                            order.TotalSum = Convert.ToInt64(reader["TotalSum"]);
                            order.Comment = Convert.ToString(reader["comment"]);

                            orders.Add(order);
                            order = new Order();
                        }
                    }

                    reader.Close();
                }

                return orders;
            }

            public ICollection<Order> GetBy(string fieldName, object parameter)
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