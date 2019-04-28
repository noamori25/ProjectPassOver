using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagementSystem.BL
{
    public class CustomerBL
    {
        private ProductBL productBL;
        private OrderBL orderBl;

        public CustomerBL()
        {
            productBL = new ProductBL();
            orderBl = new OrderBL();
        }

        public Customer ExsitingCustomer(string userName, string password)
        {
            Customer c = new Customer();
            using (SqlConnection conn = new SqlConnection(@"Data Source=.;Initial Catalog=OrdersManagmentSystem;Integrated Security=True"))
            {
                SqlCommand cmd = new SqlCommand("SELECT_EXISTING_CUSTOMER", conn);
                cmd.Parameters.Add(new SqlParameter("@USERNAME", userName));
                cmd.Parameters.Add(new SqlParameter("@PASSWORD", password));
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                while (reader.Read())
                {
                    c.Id = (int)reader["Id"];
                    c.UserName = (string)reader["UserName"];
                    c.Password = (string)reader["Password"];
                    c.FirstName = (string)reader["FirstName"];
                    c.LastName = (string)reader["LastName"];
                    c.CreditCard = (Int64)reader["CreditCard"];
                }
                cmd.Connection.Close();

                return c;
            }
        }

        public void MenuForExistingCustomer(Customer c)
        {
            Console.WriteLine("1.View all my shopping list");
            Console.WriteLine("2.View All Products");
            Console.WriteLine("3.Order Product");
            int x = Convert.ToInt32(Console.ReadLine());
            switch (x)
            {
                case 1:
                    List<OrderProductView> l = new List<OrderProductView>();
                    l = orderBl.ViewShoppingListByCustomerId(c.Id);
                    if (l.Count > 0)
                    {
                        foreach (OrderProductView b in l)
                        {
                            Console.WriteLine(b);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No products found for this user");
                    }
                    break;
                case 2:
                    List<Product> a = new List<Product>();
                    a = productBL.ViewAllProducts();
                    foreach (Product b in a)
                    {
                        Console.WriteLine(b);
                    }
                    break;
                case 3:
                    Console.WriteLine("Which product would you like to order?");
                    string s = Console.ReadLine();
                    Product selectedP = productBL.GetProductByName(s);
                    if (selectedP.Id > 0)
                    {
                        Console.WriteLine("How much would you like to order?");
                        int quantity = Convert.ToInt32(Console.ReadLine());
                        if (quantity <= selectedP.InStock && quantity > 0)
                        {
                            productBL.OrderAProduct(c.Id, selectedP.Id, quantity, selectedP.Price * quantity);
                        }
                        else
                        {
                            while (quantity <= 0 || quantity > selectedP.InStock)
                            {
                                Console.WriteLine($"You can order up to {selectedP.InStock}, please try again");
                                quantity = Convert.ToInt32(Console.ReadLine());
                            }
                            productBL.OrderAProduct(c.Id, selectedP.Id, quantity, selectedP.Price * quantity);
                        }
                    }
                    else
                    {
                        Console.WriteLine("This product does not exist in stock");
                    }
                    break;
            }

        }

        public void AddNewCustomer()
        {
            Console.WriteLine("Choose User Name");
            string UserName = Console.ReadLine();
            Console.WriteLine("Choose Password");
            string Password = Console.ReadLine();
            Console.WriteLine("Enter First Name");
            string FirstName = Console.ReadLine();
            Console.WriteLine("Enter Last Name");
            string LastName = Console.ReadLine();
            Console.WriteLine("Enter Credit Card Number");
            int CreditCard = Convert.ToInt32(Console.ReadLine());

            using (SqlConnection conn = new SqlConnection(@"Data Source=.;Initial Catalog=OrdersManagmentSystem;Integrated Security=True"))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT_NEW_CUSTOMER", conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@UserName", UserName));
                    cmd.Parameters.Add(new SqlParameter("@Password", Password));
                    cmd.Parameters.Add(new SqlParameter("@FirstName", FirstName));
                    cmd.Parameters.Add(new SqlParameter("@LastName", LastName));
                    cmd.Parameters.Add(new SqlParameter("@CreditCard", CreditCard));
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

            }
        }
        
    }
}
