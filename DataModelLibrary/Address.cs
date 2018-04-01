using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace BAS
{
    namespace DataModelLibrary
    {
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
    }
}