using System.Collections.Generic;
using System.ServiceModel;

namespace ServiceContractLibrary
{
    [ServiceContract(Namespace = "ServiceModel", Name = "BaseService")] 
    public interface IBaseServiceContract<T>
    {
        [OperationContract(Name = "Create")]
        (bool IsSuccessful, object messeage) Create(T parameter);

        [OperationContract(Name = "Update")]
        (bool IsSuccessful, string messeage) Update(ICollection<T> parameters);

        [OperationContract(Name = "Delete")]
        (bool IsSuccessful, string messeage) Delete(ICollection<T> parameters);

        [OperationContract(Name = "GetAll")]
        ICollection<T> GetAll();

        [OperationContract(Name = "GetFromTo")]
        ICollection<T> GetFromTo(uint from, uint to);

        [OperationContract(Name = "GetBy")]
        ICollection<T> GetBy(string fieldName, object value);
    }
}