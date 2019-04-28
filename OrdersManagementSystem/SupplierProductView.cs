using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagementSystem
{
  public  class SupplierProductView
    {
        public string FirstNameSup { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int ProducPrice { get; set; }
        public int Instock { get; set; }

        public override string ToString()
        {
            return $"Supplier First Name {FirstNameSup} Product Id {ProductID} Product Name {ProductName} Product Price {ProducPrice} Quantity {Instock}";
        }
    }
}
