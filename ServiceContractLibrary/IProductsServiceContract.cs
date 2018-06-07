using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using DataModelLibrary;

namespace ServiceContractLibrary
{
    [ServiceContract(Namespace = "ServiceModel", Name = "ProductsService")]
    public interface IProductsServiceContract : IBaseServiceContract<Product>
    {
        [OperationContract(Name = "GetByPrice")]
        ICollection<Product> GetByPrice(int minprice, int? maxprice = null);

        [OperationContract(Name = "GetById")]
        ICollection<Product> GetById(ICollection<int> ids);

        [OperationContract(Name = "GetAllTypes")]
        ICollection<string> GetAllType();

        [OperationContract(Name = "GetAllManufacturers")]
        ICollection<string> GetAllManufacturer();

        [OperationContract(Name = "GetAllColors")]
        ICollection<string> GetAllColor();
    }
}