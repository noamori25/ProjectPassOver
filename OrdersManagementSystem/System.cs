using OrdersManagementSystem.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagementSystem
{
    class System : IOrdersManagmentSystem
    {
        Customer customer;
        CustomerBL customerBl;
        SupplierBL supplierBl;
        Supplier supplier;
        ProductBL product;

        public System()
        {
            customerBl = new CustomerBL();
            supplierBl = new SupplierBL();
            customer = new Customer();
            supplier = new Supplier();
            product = new ProductBL();
        }

        public void BackToMainMenu()
        {
            OpenSystem();
        }

        public void CloseSystem()
        {
           Console.WriteLine("Thank you!");
        }


        public void LoginExistingCustomer()
        {
            string userName;
            string password;

            Console.WriteLine("Please enter your user name");
            userName = Console.ReadLine();
            Console.WriteLine("Please enter your password");
            password = Console.ReadLine();

            customer = customerBl.ExsitingCustomer(userName, password);
            if (customer.Id > 0)
            {
                customerBl.MenuForExistingCustomer(customer);
            }
            else
            {
                Console.WriteLine("This user does not exist in the system");
                OpenSystem();
            }

        }
        public void LoginExistingSupplier()
        {
            string userName;
            string password;

            Console.WriteLine("Please enter your user name");
            userName = Console.ReadLine();
            Console.WriteLine("Please enter your password");
            password = Console.ReadLine();

            supplier = supplierBl.ExsitingSupplier(userName, password);
            if (supplier.Id > 0)
            {
                supplierBl.MenuForExistingSupplier(supplier);
            }
            else
            {
                Console.WriteLine("This user does not exist in the system");
                OpenSystem();
            }

        }

        public void OpenSystem()
        {
            Console.WriteLine("Welcome! Please choose:");
            Console.WriteLine("1. Existing customer");
            Console.WriteLine("2. A new customer");
            Console.WriteLine("3. Existing supplier");
            Console.WriteLine("4. A new supplier");
            int x = Convert.ToInt32(Console.ReadLine());
            switch (x)
            {
                case 1:
                    {
                        LoginExistingCustomer();
                        break;
                    }
                case 2:
                    {
                        customerBl.AddNewCustomer();
                        Console.WriteLine("Thank you for your registration");
                        break;
                    }
                case 3:
                    {
                        LoginExistingSupplier();
                        break;

                    }
                case 4:
                    {
                        supplierBl.AddNewSuypplier();
                        Console.WriteLine("Thank you for your registration");
                        break;

                    }

            }
        }
    }
}
