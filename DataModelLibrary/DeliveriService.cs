using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace BAS
{
    namespace DataModelLibrary
    {
        [DataContract]
        public class DeliveriService
        {
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
}