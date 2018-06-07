using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace UWPDataModelLibrary
{
    [DataContract]
    public class DeliveriService
    {
        [DataMember] public int Id { get; set; }
        [DataMember] public string ServiceName { get; set; }

        public DeliveriService(string serviceName) =>
            ServiceName = serviceName ?? throw new ArgumentNullException(nameof(serviceName));

        public DeliveriService() { }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}