﻿using System;
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
        public class ProductsService : IProducts
        {
            public IEnumerable<Product> Create(IEnumerable<Product> products)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<Product> Delete(IEnumerable<Product> products)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<Product> GetAll()
            {
                throw new NotImplementedException();
            }

            public IEnumerable<Product> GetByColor(IEnumerable<string> colors)
            {
                throw new NotImplementedException();
            }

            public Product GetByID(int ID)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<Product> GetByManufacturer(IEnumerable<string> manufacturers)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<Product> GetByName(string name)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<Product> GetByPrice(int minprice, int? maxprice = null)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<Product> GetByType(IEnumerable<string> types)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<Product> Update(IEnumerable<Product> products)
            {
                throw new NotImplementedException();
            }
        }
    }
}