using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagementSystem
{
    public class Supplier
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NameOfCompany { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public override string ToString()
        {
            return $"Id {Id} First Name {FirstName} Last Name {LastName} Name {UserName} Password {Password} Name Of Company {NameOfCompany}";
        }

    }
}
