using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using DataModelLibrary;

namespace ServiceContractLibrary
{
    [ServiceContract(Namespace = "ServiceModel", Name = "CustomersService")]
    public interface ICustomersServiceContract : IBaseServiceContract<Customer> { }
}