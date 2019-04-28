using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagementSystem.BL
{
    class SupplierBL
    {
        private ProductBL productBl;

       public SupplierBL()
        {
            productBl = new ProductBL();
        }

        public Supplier ExsitingSupplier(string userName, string password)
        {
            Supplier s = new Supplier();
            using (SqlConnection conn = new SqlConnection(@"Data Source=.;Initial Catalog=OrdersManagmentSystem;Integrated Security=True"))
            {
                SqlCommand cmd = new SqlCommand("SELECT_EXISTING_SUPPLIER", conn);
                cmd.Parameters.Add(new SqlParameter("@USERNAME", userName));
                cmd.Parameters.Add(new SqlParameter("@PASSWORD", password));
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                while (reader.Read())
                {
                    s.Id = (int)reader["Id"];
                    s.UserName = (string)reader["UserName"];
                    s.Password = (string)reader["Password"];
                    s.FirstName = (string)reader["FirstName"];
                    s.LastName = (string)reader["LastName"];
                }
                cmd.Connection.Close();

                return s;
            }
        }

        public void MenuForExistingSupplier(Supplier s)
        {
            Console.WriteLine("1.Add a product to stock");
            Console.WriteLine("2.View all my products");
            int x = Convert.ToInt32(Console.ReadLine());
            switch (x)
            {
                case 1:
                    {
                        Console.WriteLine("Enter product name");
                        string proName = Console.ReadLine();
                        Product p = productBl.GetProductByName(proName);
                        if (p.Id > 0)
                        {
                            if (productBl.IsProductExistsByNameAndIdSupllier(proName,s.Id))
                            {
                                Console.WriteLine("This product already exist");
                                Console.WriteLine("Please enter quantity");
                                int quantity = Convert.ToInt32(Console.ReadLine());
                                if (quantity > 0)
                                {
                                    productBl.UpdateSupplierProduct(p.Id, quantity);
                                }
                            }
                            else
                            {
                                Console.WriteLine("This product already exist by other supplier");
                                
                            }
                        }
                        else
                        {
                            Console.WriteLine("Please Enter Product Name");
                            Product p1 = new Product();
                            p1.Name = Console.ReadLine();
                            Console.WriteLine("Please Enter Price");
                            p1.Price = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Please Enter Quantity");
                            p1.InStock = Convert.ToInt32(Console.ReadLine());

                            productBl.InsertNewProduct(p1);
                        }

                        break;
                    }
                   
                case 2:
                    {
                        List<SupplierProductView> a = new List<SupplierProductView>();
                        a = productBl.ViewShoppingListBySupplierId(s.Id);
                        foreach (SupplierProductView b in a)
                        {
                            Console.WriteLine(b);
                        }
                    }

                    break;

            }
        }
        public void AddNewSuypplier()
        {
            Console.WriteLine("Choose User Name");
            string UserName = Console.ReadLine();
            Console.WriteLine("Choose Password");
            string Password = Console.ReadLine();
            Console.WriteLine("Enter First Name");
            string FirstName = Console.ReadLine();
            Console.WriteLine("Enter Last Name");
            string LastName = Console.ReadLine();
            Console.WriteLine("Enter Name Of Company");
            string CompanyName = Console.ReadLine();

            using (SqlConnection conn = new SqlConnection(@"Data Source=.;Initial Catalog=OrdersManagmentSystem;Integrated Security=True"))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT_NEW_SUPPLIER", conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@UserName", UserName));
                    cmd.Parameters.Add(new SqlParameter("@Password", Password));
                    cmd.Parameters.Add(new SqlParameter("@FirstName", FirstName));
                    cmd.Parameters.Add(new SqlParameter("@LastName", LastName));
                    cmd.Parameters.Add(new SqlParameter("@NameOfCompany", CompanyName));
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

            }

        }
    }
}
