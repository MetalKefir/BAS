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
        public interface IOrdersServiceContract : IBaseServiceContract<Order> { }

    }
}