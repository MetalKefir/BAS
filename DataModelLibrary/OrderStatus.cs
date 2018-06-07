using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace DataModelLibrary
{
    [DataContract]
    public class OrderStatus
    {
        [DataMember] public string Status { get; set; }
        [DataMember] public DateTime DateChange { get; set; }

        public OrderStatus(string status, DateTime date)
        {
            this.Status = status;
            this.DateChange = date;
        }

        public OrderStatus() { }
    }
}