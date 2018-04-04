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
        public interface IProducts
        {
            [OperationContract(Name = "Delete")]
            IEnumerable<Product> Delete(IEnumerable<Product> products);

            [OperationContract(Name = "Create")]
            IEnumerable<Product> Create(IEnumerable<Product> products);

            [OperationContract(Name = "Update")]
            IEnumerable<Product> Update(IEnumerable<Product> products);

            [OperationContract(Name = "GetAll")]
            IEnumerable<Product> GetAll();

            [OperationContract(Name = "GetByID")]
            Product GetByID(int ID);

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