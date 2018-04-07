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
            public (bool IsSuccessful, string messeage) Create(IEnumerable<Customer> parametrs)
            {
                throw new NotImplementedException();
            }

            public (bool IsSuccessful, string messeage) Delete(IEnumerable<Customer> parametrs)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<Customer> GetAll()
            {
                throw new NotImplementedException();
            }

            public Customer GetByID(int ID)
            {
                throw new NotImplementedException();
            }

            public (bool IsSuccessful, string messeage) Update(IEnumerable<Customer> parametrs)
            {
                throw new NotImplementedException();
            }

            public (bool IsSuccessful, string messeage) UpdateAdress(int customerId, Address address)
            {
                throw new NotImplementedException();
            }
        }
    }
}