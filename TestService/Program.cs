using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using TestService.ServiceReference;
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
                //TestCustomersService();
                //TestProductsService();
                //TestOrdersService();
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
                    var crez = serviceClient.Create(customers[0]);
                    customers[0].Id = (int)crez.Item2;

                    var gres = serviceClient.GetAll();
                    foreach (var cus in gres)
                    {
                        Console.WriteLine(cus.Id);
                        Console.WriteLine(cus.FName + " " + cus.LName + " " + cus.MName);
                        Console.WriteLine(cus.Age);
                        Console.WriteLine(cus.CustomerAddress);
                        Console.WriteLine(cus.PhoneNumber);
                        Console.WriteLine(cus.Email);
                        Console.WriteLine("\n");
                    }

                    customers[0].FName = "Петр";
                    customers[0].Age = 40;
                    var ures = serviceClient.Update(customers);

                    var gres2 = serviceClient.GetAll();
                    foreach (var cus in gres2)
                    {
                        Console.WriteLine(cus.Id);
                        Console.WriteLine(cus.FName + " " + cus.LName + " " + cus.MName);
                        Console.WriteLine(cus.Age);
                        Console.WriteLine(cus.CustomerAddress);
                        Console.WriteLine(cus.PhoneNumber);
                        Console.WriteLine(cus.Email);
                        Console.WriteLine("\n");
                    }

                    var dres = serviceClient.Delete(customers);

                    Console.WriteLine(dres.Item1 + " " + dres.Item2);
                    Console.Read();

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

                using (ProductsServiceClient client = new ProductsServiceClient())
                {
                    //Insert
                    Console.WriteLine("1. Insert data:");
                    var cres = client.Create(products[0]);
                    products[0].Articulus = (int)cres.Item2;
                    Console.WriteLine("Insert data\n");

                    //GetAll
                    Console.WriteLine("2. Viewing data:");
                    var gres = client.GetAll();
                    foreach (var product in gres)
                        Console.WriteLine(product.Articulus + " " + product.Name + " " + product.Color);

                    //Update
                    Console.WriteLine("\n3. Update data:");
                    products[0].Sale = 80;
                    products[0].Quantity = 12;
                    products[0].Color = "Brown";
                    var ures = client.Update(products);
                    Console.WriteLine(ures.Item2+"\n");

                    //GetAll
                    Console.WriteLine("4. Viewing updated data:");
                    var gres2 = client.GetAll();
                    foreach (var product in gres2)
                        Console.WriteLine(product.Articulus + " " + product.Name + " " + product.Color);

                    //Delete
                    Console.WriteLine("\n5. Deleting data:");
                    var dres = client.Delete(products);
                    Console.WriteLine(dres.Item2 + "\n");

                    //GetAll
                    Console.WriteLine("6. Viewing data after delete:");
                    var gres3 = client.GetAll();
                    foreach (var product in gres3)
                        Console.WriteLine(product.Articulus + " " + product.Name + " " + product.Color);
                    Console.Read();
                }
            }

            private static void TestOrdersService() { }
        }
    }
}