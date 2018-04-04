using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BAS.ServiceContractLibrary;
using BAS.DataModelLibrary;
namespace BAS
{
    namespace ServicesBAS
    {
        // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "CustomersService" в коде, SVC-файле и файле конфигурации.
        // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы CustomersService.svc или CustomersService.svc.cs в обозревателе решений и начните отладку.
        public class CustomersService : ICustomersService
        {
            public IEnumerable<Customer> GetCustomers(int? ID = null)
            {
                throw new NotImplementedException();
            }
        }
    }
}