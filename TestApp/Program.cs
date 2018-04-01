using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Xml;
using BAS.DataModelLibrary;

namespace BAS
{
    namespace TestApp
    {
        class Program
        {
            static void Main(string[] args)
            {
                Customer customer = new Customer { FName = "Denis", LName = "Morozov", MName = null,
                    Age = 15, PhoneNumber = "89563214587", Email = null, CustomerAddress = null};

                var ds = new DataContractSerializer(typeof(Customer));

                XmlWriterSettings settings = new XmlWriterSettings() { Indent = true };
                using (var writer = XmlWriter.Create("customer.xml", settings))
                    ds.WriteObject(writer, customer);
                

                Customer customer2;
                using (Stream s = File.OpenRead("customer.xml"))
                    customer2 = (Customer)ds.ReadObject(s);
                
                Console.Write(customer2.FName + " " + customer2.Age);
                Console.Read();
            }
        }
    }
}