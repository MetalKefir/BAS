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
        public interface IProductsServiceContract : IBaseServiceContract<Product>
        {

            [OperationContract(Name = "GetByPrice")]
            ICollection<Product> GetByPrice(int minprice, int? maxprice = null);

        }

    }
}