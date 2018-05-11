using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace BAS
{
    namespace DataModelLibrary
    {
        [DataContract]
        public class Order
        {
            [DataMember] public int? Id { get; private set; }
            [DataMember] public Customer OrderCustomer { get; set; }
            [DataMember] public DeliveriService DeliveriService { get; set; }
            [DataMember] public DateTime DateOrder { get; set; }
            [DataMember] public List<OrderedProduct> OrderList { get; set; }
            [DataMember] public List<OrderStatus> OrderStatuses { get; set; }
            [DataMember] public long TotalSum { get; set; }
            [DataMember] public string Comment { get; set; }

            public Order(int? id, Customer orderCustomer, DeliveriService deliveriService, DateTime dateOrder,
                List<OrderedProduct> orderList, List<OrderStatus> orderStatuses, long totalSum, string comment)
            {
                Id = id;
                OrderCustomer = orderCustomer ?? throw new ArgumentNullException(nameof(orderCustomer));
                DeliveriService = deliveriService ?? throw new ArgumentNullException(nameof(deliveriService));
                DateOrder = dateOrder;
                OrderList = orderList ?? throw new ArgumentNullException(nameof(orderList));
                OrderStatuses = orderStatuses ?? throw new ArgumentNullException(nameof(orderStatuses));
                TotalSum = totalSum;
                Comment = comment;
            }

            public Order() { }

            public override string ToString()
            {
                return base.ToString();
            }
        }
    }
}