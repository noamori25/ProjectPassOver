using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagementSystem
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Idsupplier { get; set; }
        public int Price { get; set; }
        public int InStock { get; set; }

        public override string ToString()
        {
            return $"Id: {Id} Name:{Name} IdSupplier {Idsupplier} Price: {Price} In Stock: {InStock}";
        }
    }
}
