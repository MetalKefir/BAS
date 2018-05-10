using System.Collections.Generic;
using System.ServiceModel;

namespace BAS
{
    namespace ServiceContractLibrary
    {

        [ServiceContract(Namespace = "BAS.ServiceModel", Name = "BaseService")] 
        public interface IBaseServiceContract<T>
        {
            [OperationContract(Name = "Create")]
            (bool IsSuccessful, object messeage) Create(T parametr);

            [OperationContract(Name = "Update")]
            (bool IsSuccessful, string messeage) Update(ICollection<T> parametrs);

            [OperationContract(Name = "Delete")]
            (bool IsSuccessful, string messeage) Delete(ICollection<T> parametrs);

            [OperationContract(Name = "GetAll")]
            ICollection<T> GetAll();

            [OperationContract(Name = "GetBy")]
            ICollection<T> GetBy(string fieldName, object parametr);
        }

    }
}