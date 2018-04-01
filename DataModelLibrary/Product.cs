using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace BAS
{
    namespace DataModelLibrary
    {
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
    }
}