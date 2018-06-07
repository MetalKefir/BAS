using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace UWPDataModelLibrary
{
    [DataContract]
    public class Status
    {
        [DataMember] public int Id { get; set; }
        [DataMember] public string StatusName { get; set; }

        public Status(string statusName) =>
            StatusName = statusName ?? throw new ArgumentNullException(nameof(statusName));

        public Status() { }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}