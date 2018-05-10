using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using BAS.DataModelLibrary;
using BAS.ServiceContractLibrary;

namespace BAS
{
    namespace ServicesBAS
    {
        // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
        // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.
        public sealed class ProductsService : BaseService, IProductsServiceContract
        {

            public ProductsService() : base(typeof(Product)) { }

            public (bool IsSuccessful, object messeage) Create(Product parametr)
            {
                throw new NotImplementedException();
            }

            public (bool IsSuccessful, string messeage) Delete(ICollection<Product> parametrs)
            {
                throw new NotImplementedException();
            }

            public (bool IsSuccessful, string messeage) Update(ICollection<Product> parametrs)
            {
                throw new NotImplementedException();
            }

            public ICollection<Product> GetAll()
            {
                throw new NotImplementedException();
            }

            public ICollection<Product> GetBy(string fieldName, object parametr)
            {
                throw new NotImplementedException();
            }

            public ICollection<Product> GetByPrice(int minprice, int? maxprice = null)
            {
                throw new NotImplementedException();
            }

        }
    }
}