using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagementSystem
{
    public class Order
    {
        public int Id { get; set; }
        public int IdCustomer { get; set; }
        public int IdProduct { get; set; }
        public int QuantityOfOrders { get; set; }
        public int TotalPrice { get; set; }

        public override string ToString()
        {
            return $"Id: {Id} IdCustomer: {IdCustomer} IdProduct: {IdProduct}  Quantity Of Orders: {QuantityOfOrders} TotalPrice: {TotalPrice}";
        }
    }
}
