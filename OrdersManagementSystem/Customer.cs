using OrdersManagementSystem.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagementSystem
{
    public class Customer
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Int64 CreditCard { get; set; }

        public override string ToString()
        {
            return $"Id {Id} user name {UserName} password {Password} first name {FirstName} last name {LastName} credit card {CreditCard}";
        }
    }
}
