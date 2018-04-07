using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BAS.DataModelLibrary;
using BAS.ServiceContractLibrary;

namespace BAS
{
    namespace ServicesBAS
    {
        // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "OrdersService" в коде, SVC-файле и файле конфигурации.
        // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы OrdersService.svc или OrdersService.svc.cs в обозревателе решений и начните отладку.
        public class OrdersService : IOrdersService
        {
            public (bool IsSuccessful, string messeage) Create(IEnumerable<Order> parametrs)
            {
                throw new NotImplementedException();
            }

            public (bool IsSuccessful, string messeage) Delete(IEnumerable<Order> parametrs)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<Order> GetAll()
            {
                throw new NotImplementedException();
            }

            public IEnumerable<Order> GetByCustomer(int customerId)
            {
                throw new NotImplementedException();
            }

            public Order GetByID(int ID)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<Order> GetByProduct(IEnumerable<Product> products)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<Order> GetByStatus(Status products)
            {
                throw new NotImplementedException();
            }

            public (bool IsSuccessful, string messeage) Update(IEnumerable<Order> parametrs)
            {
                throw new NotImplementedException();
            }
        }
    }
}