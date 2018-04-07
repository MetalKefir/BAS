using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace BAS
{
    namespace DataModelLibrary
    {
        [DataContract]
        public class Customer
        {
            [DataMember] public int Id { get; set; }
            [DataMember] public string FName { get; set; }
            [DataMember] public string LName { get; set; }
            [DataMember] public string MName { get; set; }
            [DataMember] public ushort Age { get; set; }
            [DataMember] public string PhoneNumber { get; set; }
            [DataMember] public string Email { get; set; }
            [DataMember] public Address CustomerAddress { get; set; }

            public Customer(int id, string fName, string lName, string mName,
                ushort age, string phoneNumber, string email, Address customerAddress)
            {
                Id = id;
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
    }
}