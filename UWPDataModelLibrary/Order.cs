using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace UWPDataModelLibrary
{
    [DataContract]
    public class Order
    {
        [DataMember] public int? Id { get; set; }
        [DataMember] public Customer OrderCustomer { get; set; }
        [DataMember] public DeliveriService DeliveryService { get; set; }
        [DataMember] public DateTime DateOrder { get; set; }
        [DataMember] public List<OrderedProduct> OrderList { get; set; }
        [DataMember] public List<OrderStatus> OrderStatuses { get; set; }
        [DataMember] public long TotalSum { get; set; }
        [DataMember] public string Comment { get; set; }

        public Order(int? id, Customer orderCustomer, DeliveriService deliveryService, DateTime dateOrder,
            List<OrderedProduct> orderList, List<OrderStatus> orderStatuses, long totalSum, string comment)
        {
            Id = id;
            OrderCustomer = orderCustomer ?? throw new ArgumentNullException(nameof(orderCustomer));
            DeliveryService = deliveryService ?? throw new ArgumentNullException(nameof(deliveryService));
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