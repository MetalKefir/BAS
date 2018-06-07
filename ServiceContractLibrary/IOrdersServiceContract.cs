using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModelLibrary;
using System.ServiceModel;

namespace ServiceContractLibrary
{
    [ServiceContract(Namespace = "ServiceModel", Name = "OrdersService")]
    public interface IOrdersServiceContract : IBaseServiceContract<Order>
    {
        ICollection<OrderStatus> GetOrderStatuses(int orderId);
    }
}