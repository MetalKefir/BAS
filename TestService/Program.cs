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
                    var rez = serviceClient.Create(customers);

                    Console.Read();
                    //List<Customer> cus = serviceClient.GetAll().ToList<Customer>();
                }

                Console.Read();
            }
        }
    }
}