﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestService.ServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="BAS.ServiceModel", ConfigurationName="ServiceReference.CustomersService")]
    public interface CustomersService {
        
        [System.ServiceModel.OperationContractAttribute(Action="BAS.ServiceModel/BaseService/Create", ReplyAction="BAS.ServiceModel/BaseService/CreateResponse")]
        System.ValueTuple<bool, string> Create(System.Collections.Generic.List<BAS.DataModelLibrary.Customer> parametr);
        
        [System.ServiceModel.OperationContractAttribute(Action="BAS.ServiceModel/BaseService/Create", ReplyAction="BAS.ServiceModel/BaseService/CreateResponse")]
        System.Threading.Tasks.Task<System.ValueTuple<bool, string>> CreateAsync(System.Collections.Generic.List<BAS.DataModelLibrary.Customer> parametr);
        
        [System.ServiceModel.OperationContractAttribute(Action="BAS.ServiceModel/BaseService/Update", ReplyAction="BAS.ServiceModel/BaseService/UpdateResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.ValueTuple<bool, string>))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.Collections.Generic.List<BAS.DataModelLibrary.Customer>))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(BAS.DataModelLibrary.Customer))]
        System.ValueTuple<bool, string> Update(object parametr);
        
        [System.ServiceModel.OperationContractAttribute(Action="BAS.ServiceModel/BaseService/Update", ReplyAction="BAS.ServiceModel/BaseService/UpdateResponse")]
        System.Threading.Tasks.Task<System.ValueTuple<bool, string>> UpdateAsync(object parametr);
        
        [System.ServiceModel.OperationContractAttribute(Action="BAS.ServiceModel/BaseService/Delete", ReplyAction="BAS.ServiceModel/BaseService/DeleteResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.ValueTuple<bool, string>))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.Collections.Generic.List<BAS.DataModelLibrary.Customer>))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(BAS.DataModelLibrary.Customer))]
        System.ValueTuple<bool, string> Delete(object parametr);
        
        [System.ServiceModel.OperationContractAttribute(Action="BAS.ServiceModel/BaseService/Delete", ReplyAction="BAS.ServiceModel/BaseService/DeleteResponse")]
        System.Threading.Tasks.Task<System.ValueTuple<bool, string>> DeleteAsync(object parametr);
        
        [System.ServiceModel.OperationContractAttribute(Action="BAS.ServiceModel/BaseService/GetAll", ReplyAction="BAS.ServiceModel/BaseService/GetAllResponse")]
        System.Collections.Generic.List<BAS.DataModelLibrary.Customer> GetAll();
        
        [System.ServiceModel.OperationContractAttribute(Action="BAS.ServiceModel/BaseService/GetAll", ReplyAction="BAS.ServiceModel/BaseService/GetAllResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<BAS.DataModelLibrary.Customer>> GetAllAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="BAS.ServiceModel/BaseService/GetBy", ReplyAction="BAS.ServiceModel/BaseService/GetByResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.ValueTuple<bool, string>))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.Collections.Generic.List<BAS.DataModelLibrary.Customer>))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(BAS.DataModelLibrary.Customer))]
        System.Collections.Generic.List<BAS.DataModelLibrary.Customer> GetBy(string fieldName, object parametr);
        
        [System.ServiceModel.OperationContractAttribute(Action="BAS.ServiceModel/BaseService/GetBy", ReplyAction="BAS.ServiceModel/BaseService/GetByResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<BAS.DataModelLibrary.Customer>> GetByAsync(string fieldName, object parametr);
        
        [System.ServiceModel.OperationContractAttribute(Action="BAS.ServiceModel/CustomersService/UpdateAdress", ReplyAction="BAS.ServiceModel/CustomersService/UpdateAdressResponse")]
        System.ValueTuple<bool, string> UpdateAdress(int customerId, string address);
        
        [System.ServiceModel.OperationContractAttribute(Action="BAS.ServiceModel/CustomersService/UpdateAdress", ReplyAction="BAS.ServiceModel/CustomersService/UpdateAdressResponse")]
        System.Threading.Tasks.Task<System.ValueTuple<bool, string>> UpdateAdressAsync(int customerId, string address);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface CustomersServiceChannel : TestService.ServiceReference.CustomersService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CustomersServiceClient : System.ServiceModel.ClientBase<TestService.ServiceReference.CustomersService>, TestService.ServiceReference.CustomersService {
        
        public CustomersServiceClient() {
        }
        
        public CustomersServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CustomersServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CustomersServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CustomersServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.ValueTuple<bool, string> Create(System.Collections.Generic.List<BAS.DataModelLibrary.Customer> parametr) {
            return base.Channel.Create(parametr);
        }
        
        public System.Threading.Tasks.Task<System.ValueTuple<bool, string>> CreateAsync(System.Collections.Generic.List<BAS.DataModelLibrary.Customer> parametr) {
            return base.Channel.CreateAsync(parametr);
        }
        
        public System.ValueTuple<bool, string> Update(object parametr) {
            return base.Channel.Update(parametr);
        }
        
        public System.Threading.Tasks.Task<System.ValueTuple<bool, string>> UpdateAsync(object parametr) {
            return base.Channel.UpdateAsync(parametr);
        }
        
        public System.ValueTuple<bool, string> Delete(object parametr) {
            return base.Channel.Delete(parametr);
        }
        
        public System.Threading.Tasks.Task<System.ValueTuple<bool, string>> DeleteAsync(object parametr) {
            return base.Channel.DeleteAsync(parametr);
        }
        
        public System.Collections.Generic.List<BAS.DataModelLibrary.Customer> GetAll() {
            return base.Channel.GetAll();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<BAS.DataModelLibrary.Customer>> GetAllAsync() {
            return base.Channel.GetAllAsync();
        }
        
        public System.Collections.Generic.List<BAS.DataModelLibrary.Customer> GetBy(string fieldName, object parametr) {
            return base.Channel.GetBy(fieldName, parametr);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<BAS.DataModelLibrary.Customer>> GetByAsync(string fieldName, object parametr) {
            return base.Channel.GetByAsync(fieldName, parametr);
        }
        
        public System.ValueTuple<bool, string> UpdateAdress(int customerId, string address) {
            return base.Channel.UpdateAdress(customerId, address);
        }
        
        public System.Threading.Tasks.Task<System.ValueTuple<bool, string>> UpdateAdressAsync(int customerId, string address) {
            return base.Channel.UpdateAdressAsync(customerId, address);
        }
    }
}
