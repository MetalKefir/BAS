using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace BAS
{
    namespace DataModelLibrary
    {
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
    }
}