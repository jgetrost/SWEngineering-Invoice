using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace invoicr.Models
{
    public class InvoicrModel
    {
        public class Invoice
        {
            public int ID { get; set; }
            public string GUID { get; set; }
            public virtual ICollection<Order> Orders { get; set; }
        }

        public class Product
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Descrption { get; set; }
            public decimal Price { get; set; }
            public virtual ICollection<Order> Orders { get; set; }
        }

        public class Order
        {
            public int ID { get; set; }
            public virtual Product Product { get; set; }
            public virtual Invoice Invoice { get; set; }
            public int Quantity { get; set; }
        }
    }
}
