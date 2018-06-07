using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace DataModelLibrary
{
    [DataContract]
    public class OrderedProduct
    {
        [DataMember] public Product Product { get; set; }
        [DataMember] public int Quantity { get; set; }

        public OrderedProduct(Product product, int quantity)
        {
            this.Product = product;
            this.Quantity = quantity;
        }

        public OrderedProduct() { }
    }
}
