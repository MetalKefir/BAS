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
            [ServiceKnownType(typeof(IReadOnlyCollection<DataModelLibrary.Customer>))]
            (bool IsSuccessful, string messeage) Create(ICollection<T> parametr);

            [OperationContract(Name = "Update")]
            (bool IsSuccessful, string messeage) Update(IReadOnlyCollection<T> parametr);

            [OperationContract(Name = "Delete")]
            (bool IsSuccessful, string messeage) Delete(IReadOnlyCollection<T> parametr);

            [OperationContract(Name = "GetAll")]
            ICollection<T> GetAll();

            [OperationContract(Name = "GetBy")]
            ICollection<T> GetBy(string fieldName, object parametr);
        }

    }
}