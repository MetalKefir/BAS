using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using TestService.ServiceReference;
using BAS.DataModelLibrary;

namespace BAS
{
    namespace TestService
    {
        class Program
        {
            static void Main(string[] args)
            {
                //string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                //using (SqlConnection connection = new SqlConnection(connectionString))
                //{
                //    connection.Open();
                //    Console.WriteLine("Подключение открыто");
                //}
                //Console.WriteLine("Подключение закрыто...");
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
                    //List<Customer> cus = serviceClient.GetAll().ToList<Customer>();
                }

                Console.Read();
            }
        }
    }
}