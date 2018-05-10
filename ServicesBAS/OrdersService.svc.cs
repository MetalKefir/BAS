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
        public sealed class OrdersService : BaseService, IOrdersServiceContract
        {

            public OrdersService() : base(typeof(Order)) { }

            public (bool IsSuccessful, object messeage) Create(Order parametr)
            {
                throw new NotImplementedException();
            }

            public (bool IsSuccessful, string messeage) Delete(ICollection<Order> parametrs)
            {
                throw new NotImplementedException();
            }

            public (bool IsSuccessful, string messeage) Update(ICollection<Order> parametrs)
            {
                throw new NotImplementedException();
            }

            public ICollection<Order> GetAll()
            {
                throw new NotImplementedException();
            }

            public ICollection<Order> GetBy(string fieldName, object parametr)
            {
                throw new NotImplementedException();
            }
        }
    }
}