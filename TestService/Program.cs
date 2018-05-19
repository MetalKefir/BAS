using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using TestService.ServiceReference;
using TestService.ServiceReference1;
using TestService.ProductService;
using BAS.DataModelLibrary;

namespace BAS
{
    namespace TestService
    {
        class Program
        {
            static void Main(string[] args)
            {
                Console.WriteLine("Press any key");
                Console.Read();

                Console.WriteLine("Test Customers Service: ");
                TestCustomersService();
                Console.WriteLine("\nTest Customers Successful");

                Console.WriteLine("\nTest Products Service: ");
                TestProductsService();
                Console.WriteLine("\nTest Products Successful");

                Console.WriteLine("\nTest Orders Service: ");
                TestOrdersService();
                Console.WriteLine("\nTest Orders Successful");

                Console.Read();
                Console.Read();
            }

            private static void TestCustomersService()
            {
                List<Customer> customers = new List<Customer> {

                    new Customer()
                    {
                        Id = null,
                        Age = 15,
                        FName = "Иван",
                        LName = "Иванов",
                        MName = "Иванович",
                        CustomerAddress = "улица Пушкина, длм Колотушкина",
                        Email = "228@mail.ru",
                        PhoneNumber = "8(111) 111-11-12"
                    }
                };

                using (CustomersServiceClient serviceClient = new CustomersServiceClient())
                {
                    //Insert
                    Console.WriteLine("1. Insert data:");
                    var crez = serviceClient.Create(customers[0]);
                    customers[0].Id = (int)crez.Item2;
                    Console.WriteLine("Insert data\n");

                    //GetAll
                    Console.WriteLine("2. Viewing data:");
                    var gres = serviceClient.GetAll();
                    foreach (var cus in gres)
                    {
                        Console.WriteLine(cus.Id);
                        Console.WriteLine(cus.FName + " " + cus.LName + " " + cus.MName);
                        Console.WriteLine(cus.Age);
                        Console.WriteLine(cus.CustomerAddress);
                        Console.WriteLine(cus.PhoneNumber);
                        Console.WriteLine(cus.Email);
                    }

                    //Update
                    Console.WriteLine("\n3. Update data:");
                    customers[0].FName = "Петр";
                    customers[0].Age = 40;
                    var ures = serviceClient.Update(customers);
                    Console.WriteLine(ures.Item2 + "\n");

                    //GetAll
                    Console.WriteLine("4. Viewing data:");
                    var gres2 = serviceClient.GetAll();
                    foreach (var cus in gres2)
                    {
                        Console.WriteLine(cus.Id);
                        Console.WriteLine(cus.FName + " " + cus.LName + " " + cus.MName);
                        Console.WriteLine(cus.Age);
                        Console.WriteLine(cus.CustomerAddress);
                        Console.WriteLine(cus.PhoneNumber);
                        Console.WriteLine(cus.Email);
                    }

                    //Delete
                    Console.WriteLine("\n5. Deleting data:");
                    var dres = serviceClient.Delete(customers);
                    Console.WriteLine(dres.Item2 + "\n");

                    //GetAll
                    Console.WriteLine("6. Viewing data:");
                    var gres3 = serviceClient.GetAll();
                    foreach (var cus in gres3)
                    {
                        Console.WriteLine(cus.Id);
                        Console.WriteLine(cus.FName + " " + cus.LName + " " + cus.MName);
                        Console.WriteLine(cus.Age);
                        Console.WriteLine(cus.CustomerAddress);
                        Console.WriteLine(cus.PhoneNumber);
                        Console.WriteLine(cus.Email);
                    }
                }
            }

            private static void TestProductsService()
            {
                List<Product> products = new List<Product>
                {
                    new Product()
                    {
                        Articulus = null,
                        Name = "M-16",
                        Color = "Black",
                        Manufacturer = "USANavi",
                        Price = 123.56m,
                        Quantity = 45,
                        Sale = null,
                        Type = "rifle",
                        Description = null
                    }
                };

                using (ProductsServiceClient productsclient = new ProductsServiceClient())
                {
                    //Insert
                    Console.WriteLine("1. Insert data:");
                    var cres = productsclient.Create(products[0]);
                    products[0].Articulus = (int)cres.Item2;
                    Console.WriteLine("Insert data\n");

                    //GetAll
                    Console.WriteLine("2. Viewing data:");
                    var gres = productsclient.GetAll();
                    foreach (var product in gres)
                        Console.WriteLine(product.Articulus + " " + product.Name + " " + product.Color);

                    //Update
                    Console.WriteLine("\n3. Update data:");
                    products[0].Sale = 80;
                    products[0].Quantity = 12;
                    products[0].Color = "Brown";
                    var ures = productsclient.Update(products);
                    Console.WriteLine(ures.Item2+"\n");

                    //GetAll
                    Console.WriteLine("4. Viewing updated data:");
                    var gres2 = productsclient.GetAll();
                    foreach (var product in gres2)
                        Console.WriteLine(product.Articulus + " " + product.Name + " " + product.Color);

                    //Delete
                    Console.WriteLine("\n5. Deleting data:");
                    var dres = productsclient.Delete(products);
                    Console.WriteLine(dres.Item2 + "\n");

                    //GetAll
                    Console.WriteLine("6. Viewing data after delete:");
                    var gres3 = productsclient.GetAll();
                    foreach (var product in gres3)
                        Console.WriteLine(product.Articulus + " " + product.Name + " " + product.Color);
                }
            }

            private static void TestOrdersService()
            {
                List<Order> orders = new List<Order>
                {
                    new Order()
                    {
                        Id = null,
                        DateOrder = DateTime.Now,
                        DeliveryService = new DeliveriService("Self"),
                        OrderCustomer = new Customer {Id=1},
                        Comment = "",
                        TotalSum = 340,
                        OrderStatuses = new List<OrderStatus>
                        {
                            new OrderStatus
                            {
                                Status = new Status("moderation"),
                                DateChange = DateTime.Now
                            }
                        },
                        OrderList = new List<OrderedProduct>
                        {
                            new OrderedProduct
                            {
                                Product = new Product { Articulus = 1},
                                Quantity = 5
                            },
                            new OrderedProduct
                            {
                                Product = new Product { Articulus = 2},
                                Quantity = 2
                            }
                        }
                    }
                };

                using (OrdersServiceClient ordersclient = new OrdersServiceClient())
                {
                    //Insert
                    Console.WriteLine("1. Insert data:");
                    var cres = ordersclient.Create(orders[0]);
                    orders[0].Id = (int)cres.Item2;
                    Console.WriteLine("Insert data\n");

                    //GetAll
                    Console.WriteLine("2. Viewing data:");
                    var gres = ordersclient.GetAll();
                    foreach (var order in gres)
                        Console.WriteLine(order.Id + " " + order.DateOrder + " " + order.OrderCustomer.Id);

                    //Update
                    Console.WriteLine("\n3. Update data:");
                    orders[0].TotalSum = 80;
                    orders[0].Comment = "12212121";
                    orders[0].DeliveryService = new DeliveriService("RusPochta");
                    orders[0].OrderList[0].Quantity = 8;
                    orders[0].OrderList[0].Quantity = 9;
                    orders[0].OrderList.Add(new OrderedProduct
                    {
                        Product = new Product { Articulus = 3 },
                        Quantity = 4
                    });
                    var ures = ordersclient.Update(orders);
                    Console.WriteLine(ures.Item2 + "\n");

                    //GetAll
                    Console.WriteLine("4. Viewing data:");
                    var gres2 = ordersclient.GetAll();
                    foreach (var order in gres2)
                        Console.WriteLine(order.Id + " " + order.DateOrder + " " + order.OrderCustomer.Id);

                    //Delete
                    Console.WriteLine("\n5. Deleting data:");
                    var dres = ordersclient.Delete(orders);
                    Console.WriteLine(dres.Item2 + "\n");

                    //GetAll
                    Console.WriteLine("6. Viewing data:");
                    var gres3 = ordersclient.GetAll();
                    foreach (var order in gres3)
                        Console.WriteLine(order.Id + " " + order.DateOrder + " " + order.OrderCustomer.Id);
                }
            }
        }
    }
}