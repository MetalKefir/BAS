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
        [ServiceContract(Namespace = "BAS.ServiceModel", Name = "ProductsService")]
        public interface IProductsService : IBaseService<Product>
        {
            [OperationContract(Name = "GetByType")]
            IEnumerable<Product> GetByType(IEnumerable<string> types);

            [OperationContract(Name = "GetByColor")]
            IEnumerable<Product> GetByColor(IEnumerable<string> colors);

            [OperationContract(Name = "GetByManufacturer")]
            IEnumerable<Product> GetByManufacturer(IEnumerable<string> manufacturers);

            [OperationContract(Name = "GetByPrice")]
            IEnumerable<Product> GetByPrice(int minprice, int? maxprice = null);

            [OperationContract(Name = "GetByName")]
            IEnumerable<Product> GetByName(string name);
        }
    }
}