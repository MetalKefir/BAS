using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using BAS.DataModelLibrary;

namespace BAS
{
    namespace ServiceContractLibrary
    {
        [ServiceContract(Namespace = "BAS.ServiceModel", Name = "CustomersService")]
        public interface ICustomersService : IBaseService<Customer>
        {
            [OperationContract(Name = "UpdateAdress")]
            (bool IsSuccessful, string messeage) UpdateAdress(int customerId, Address address);
        }
    }
}