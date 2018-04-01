using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BAS
{
    namespace DataModelLibrary
    {
        [DataContract]
        public class OrderedProduct
        {
            [DataMember] Product Product { get; set; }
            [DataMember] int Quantity { get; set; }

            public OrderedProduct(Product product, int quantity)
            {
                this.Product = product;
                this.Quantity = quantity;
            }
        }

        [DataContract]
        public class OrderStatus
        {
            [DataMember] public Status Status { get; set; }
            [DataMember] public DateTime DateChange { get; set; }

            public OrderStatus(Status status, DateTime date)
            {
                this.Status = status;
                this.DateChange = date;
            }
        }

        [DataContract]
        public class Product
        {
            [DataMember] public int Articulus { get; set; }
            [DataMember] public string Name { get; set; }
            [DataMember] public string Manufacturer { get; set; }
            [DataMember] public string Type { get; set; }
            [DataMember] public string Color { get; set; }
            [DataMember] public int Quantity { get; set; }
            [DataMember] public int Price { get; set; }
            [DataMember] public ushort? Sale { get; set; }
            [DataMember] public string Description { get; set; }

            public Product(int articulus, string name, string manufacturer, string type, 
                string color, int quantity, int price, ushort? sale, string description)
            {
                Articulus = articulus;
                Name = name ?? throw new ArgumentNullException(nameof(name));
                Manufacturer = manufacturer ?? throw new ArgumentNullException(nameof(manufacturer));
                Type = type ?? throw new ArgumentNullException(nameof(type));
                Color = color ?? throw new ArgumentNullException(nameof(color));
                Quantity = quantity;
                Price = price;
                Sale = sale;
                Description = description;
            }

            public override string ToString()
            {
                return base.ToString();
            }
        }

        [DataContract]
        public class Order
        {
            [DataMember] public Customer OrderCustomer { get; set; }
            [DataMember] public DeliveriService DeliveriService { get; set; }
            [DataMember] public DateTime DateOrder { get; set; }
            [DataMember] public List<OrderedProduct> OrderList { get; set; }
            [DataMember] public List<OrderStatus> OrderStatuses { get; set; }
            [DataMember] public long TotalSum { get; set; }
            [DataMember] public string Comment { get; set; }

            public Order(Customer orderCustomer, DeliveriService deliveriService, DateTime dateOrder, 
                List<OrderedProduct> orderList, List<OrderStatus> orderStatuses, long totalSum, string comment)
            {
                OrderCustomer = orderCustomer ?? throw new ArgumentNullException(nameof(orderCustomer));
                DeliveriService = deliveriService ?? throw new ArgumentNullException(nameof(deliveriService));
                DateOrder = dateOrder;
                OrderList = orderList ?? throw new ArgumentNullException(nameof(orderList));
                OrderStatuses = orderStatuses ?? throw new ArgumentNullException(nameof(orderStatuses));
                TotalSum = totalSum;
                Comment = comment;
            }

            public override string ToString()
            {
                return base.ToString();
            }
        }

        [DataContract]
        public class Customer
        {
            [DataMember] public string FName { get; set; }
            [DataMember] public string LName { get; set; }
            [DataMember] public string MName { get; set; }
            [DataMember] public ushort Age { get; set; }
            [DataMember] public string PhoneNumber { get; set; }
            [DataMember] public string Email { get; set; }
            [DataMember] public Address CustomerAddress { get; set; }

            public Customer(string fName, string lName, string mName,
                ushort age, string phoneNumber, string email, Address customerAddress)
            {
                FName = fName ?? throw new ArgumentNullException(nameof(fName));
                LName = lName ?? throw new ArgumentNullException(nameof(lName));
                MName = mName;
                Age = age;
                PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
                Email = email ?? throw new ArgumentNullException(nameof(email));
                CustomerAddress = customerAddress ?? throw new ArgumentNullException(nameof(customerAddress));
            }

            public Customer()
            {
                
            }

            public override string ToString()
            {
                return base.ToString();
            }
        }

        [DataContract]
        public class Address
        {
            [DataMember] public string Country { get; set; }
            [DataMember] public string City { get; set; }
            [DataMember] public string Street { get; set; }
            [DataMember] public int House { get; set; }
            [DataMember] public int? Housing { get; set; }
            [DataMember] public int Apartment { get; set; }

            public Address(string country, string city, string street, int house, int? housing, int apartment)
            {
                Country = country ?? throw new ArgumentNullException(nameof(country));
                City = city ?? throw new ArgumentNullException(nameof(city));
                Street = street ?? throw new ArgumentNullException(nameof(street));
                House = house;
                Housing = housing;
                Apartment = apartment;
            }

            public override string ToString()
            {
                return base.ToString();
            }
        }

        [DataContract]
        public class DeliveriService
        {
            [DataMember] public string ServiceName { get; set; }

            public DeliveriService(string serviceName)=>
                ServiceName = serviceName ?? throw new ArgumentNullException(nameof(serviceName));
            
            public override string ToString()
            {
                return base.ToString();
            }
        }

        [DataContract]
        public class Status
        {
            [DataMember] public string StatusName { get; set; }

            public Status(string statusName) =>
                StatusName = statusName ?? throw new ArgumentNullException(nameof(statusName));
            
            public override string ToString()
            {
                return base.ToString();
            }
        }
    }
}