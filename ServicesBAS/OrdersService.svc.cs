using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ServiceContractLibrary;
using DataModelLibrary;
using System.Data.SqlClient;
using System.Data;

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
                    Comment = Convert.ToString(reader["comment"]),
                    OrderList = new List<OrderedProduct>(),
                    OrderStatuses = new List<OrderStatus>()
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
                    orderedStatuses.Rows.Add(orderStatus.Status, orderStatus.DateChange);
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

            SqlCommand statusesCommand = new SqlCommand("GetOrderStatuses")
            {
                CommandType = CommandType.StoredProcedure
            };
            statusesCommand.Parameters.Add(new SqlParameter() { ParameterName = "@orderid", SqlDbType = SqlDbType.Int });

            Func<SqlDataReader, OrderStatus> statusesConvertor = (SqlDataReader reader) =>
            {
                OrderStatus orderStatus = new OrderStatus()
                {
                    Status = Convert.ToString(reader["Status"]),
                    DateChange = Convert.ToDateTime(reader["Date"])
                };

                return orderStatus;
            };

            SqlRequestHelper<OrderStatus> statusesRequestHelper = new SqlRequestHelper<OrderStatus>();


            SqlCommand productIdCommand = new SqlCommand("GetOrderedProduct")
            {
                CommandType = CommandType.StoredProcedure
            };
            productIdCommand.Parameters.Add(new SqlParameter() { ParameterName = "@orderid", SqlDbType = SqlDbType.Int });

            Func<SqlDataReader, OrderedProduct> productIdConvertor = (SqlDataReader reader) =>
            {
                OrderedProduct orderedProduct = new OrderedProduct()
                {
                    Product = new Product() { Articulus = Convert.ToInt32(reader["ProductId"]) },
                    Quantity = Convert.ToInt32(reader["Quantity"])
                };

                return orderedProduct;
            };

            SqlRequestHelper<OrderedProduct> productIdRequestHelper = new SqlRequestHelper<OrderedProduct>();

            List<int> listProdId = new List<int>();

            foreach (var order in RequestHelper.ReadQuery(command, DataReaderConverter))
            {
                statusesCommand.Parameters["@orderid"].Value = order.Id;
                productIdCommand.Parameters["@orderid"].Value = order.Id;

                foreach (var orderStatus in statusesRequestHelper.ReadQuery(statusesCommand, statusesConvertor))
                    order.OrderStatuses.Add(orderStatus);

                foreach (var orderedProduct in productIdRequestHelper.ReadQuery(productIdCommand, productIdConvertor))
                    order.OrderList.Add(orderedProduct);


                orders.Add(order);
            }

            return orders;
        }

        public ICollection<Order> GetBy(string fieldName, object value)
        {
            throw new NotImplementedException();
        }

        public ICollection<Order> GetFromTo(uint from, uint to)
        {
            ICollection<Order> orders = new List<Order>();

            SqlCommand command = new SqlCommand(storedProcedure["getfromto"])
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add(new SqlParameter() { ParameterName = "@from", Value = from, SqlDbType = SqlDbType.Int });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@to", Value = to, SqlDbType = SqlDbType.Int });

            SqlCommand statusesCommand = new SqlCommand("GetOrderStatuses")
            {
                CommandType = CommandType.StoredProcedure
            };
            statusesCommand.Parameters.Add(new SqlParameter() { ParameterName = "@orderid", SqlDbType = SqlDbType.Int });

            Func<SqlDataReader, OrderStatus> statusesConvertor = (SqlDataReader reader) =>
            {
                OrderStatus orderStatus = new OrderStatus()
                {
                    Status = Convert.ToString(reader["Status"]),
                    DateChange = Convert.ToDateTime(reader["Date"])
                };

                return orderStatus;
            };

            SqlRequestHelper<OrderStatus> statusesRequestHelper = new SqlRequestHelper<OrderStatus>();


            SqlCommand productIdCommand = new SqlCommand("GetOrderedProduct")
            {
                CommandType = CommandType.StoredProcedure
            };
            productIdCommand.Parameters.Add(new SqlParameter() { ParameterName = "@orderid", SqlDbType = SqlDbType.Int });

            Func<SqlDataReader, OrderedProduct> productIdConvertor = (SqlDataReader reader) =>
            {
                OrderedProduct orderedProduct = new OrderedProduct()
                {
                    Product = new Product() { Articulus = Convert.ToInt32(reader["ProductId"]) },
                    Quantity = Convert.ToInt32(reader["Quantity"])
                };

                return orderedProduct;
            };

            SqlRequestHelper<OrderedProduct> productIdRequestHelper = new SqlRequestHelper<OrderedProduct>();

            List<int> listProdId = new List<int>();

            ProductsService productsService = new ProductsService();

            foreach (var order in RequestHelper.ReadQuery(command, DataReaderConverter))
            {
                statusesCommand.Parameters["@orderid"].Value = order.Id;
                productIdCommand.Parameters["@orderid"].Value = order.Id;

                foreach (var orderStatus in statusesRequestHelper.ReadQuery(statusesCommand, statusesConvertor))
                    order.OrderStatuses.Add(orderStatus);
   
                foreach (var orderedProductId in productIdRequestHelper.ReadQuery(productIdCommand, productIdConvertor))
                {
                    List<int> param = new List<int>
                    {
                        orderedProductId.Product.Articulus.Value
                    };

                    OrderedProduct orderedProduct = new OrderedProduct()
                    {
                        Product = productsService.GetById(param).ToList()[0],
                        Quantity = orderedProductId.Quantity
                    };

                    order.OrderList.Add(orderedProduct);
                }

                CustomersService customersService = new CustomersService();
                order.OrderCustomer = customersService.GetFromTo((uint)order.OrderCustomer.Id, (uint)order.OrderCustomer.Id).FirstOrDefault();

                orders.Add(order);
            }

            return orders;
        }

        public ICollection<OrderStatus> GetOrderStatuses(int orderId)
        {
            throw new NotImplementedException();
        }
    }
}