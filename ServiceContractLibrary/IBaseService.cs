using System.Collections.Generic;
using System.ServiceModel;

namespace BAS
{
    namespace ServiceContractLibrary
    {
        [ServiceContract(Namespace = "BAS.ServiceModel", Name = "BaseService")]
        public interface IBaseService<T>
        {
            [OperationContract(Name = "Create")]
            (bool IsSuccessful, string messeage) Create(IEnumerable<T> parametrs);

            [OperationContract(Name = "Update")]
            (bool IsSuccessful, string messeage) Update(IEnumerable<T> parametrs);

            [OperationContract(Name = "Delete")]
            (bool IsSuccessful, string messeage) Delete(IEnumerable<T> parametrs);

            [OperationContract(Name = "GetAll")]
            IEnumerable<T> GetAll();

            [OperationContract(Name = "GetByID")]
            T GetByID(int ID);
        }
    }
}