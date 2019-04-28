using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagementSystem
{
    public class OrderProductView
    {
       public int OrderId { get; set; }
        public int ProductID { get; set; }
        public int CustomerID { get; set; }
        public string ProductName { get; set; }
        public int QuantityOfOrders { get; set; }
        public int TotalPrice { get; set; }

        public override string ToString()
        {
            return $"Order Id: {OrderId} Product Id: {ProductID} Customer Id {CustomerID} Product Name {ProductName} Quantity {QuantityOfOrders} Total Price {TotalPrice}";
        }


    }
}
