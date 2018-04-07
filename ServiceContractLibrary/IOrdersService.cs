using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAS.DataModelLibrary;
using System.ServiceModel;
namespace BAS
{
    namespace ServiceContractLibrary
    {
        [ServiceContract(Namespace = "BAS.ServiceModel", Name = "OrdersService")]
        public interface IOrdersService : IBaseService<Order>
        {
            [OperationContract(Name = "GetByCust")]
            IEnumerable<Order> GetByCustomer(int customerId);

            [OperationContract(Name = "GetByProduct")]
            IEnumerable<Order> GetByProduct(IEnumerable<Product> products);

            [OperationContract(Name = "GetByStatus")]
            IEnumerable<Order> GetByStatus(Status products);
        }
    }
}