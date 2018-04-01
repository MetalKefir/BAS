using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace BAS
{
    namespace DataModelLibrary
    {
        [DataContract]
        public class OrderedProduct
        {
            [DataMember] Product Product { get; set; }
            [DataMember] int Quantity { get; set; }

            public OrderedProduct(Product product, int quantity)
            {
                this.Product = product;
                this.Quantity = quantity;
            }
        }
    }
}
