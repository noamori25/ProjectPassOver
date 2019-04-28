using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagementSystem.BL
{
    public class ProductBL
    {
        private OrderBL orderBl;

        public ProductBL()
        {
            orderBl = new OrderBL();
        }

        public void UpdateInStockByProductId(int ProductId, int Quantity)
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=.;Initial Catalog=OrdersManagmentSystem;Integrated Security=True"))
            {
                SqlCommand cmd = new SqlCommand("UPDATE_INSTOCK_PRODUCTS_BY_ID", conn);
                cmd.Parameters.Add(new SqlParameter("@Id", ProductId));
                cmd.Parameters.Add(new SqlParameter("@Quantity", Quantity));
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
            }
        }
        public List<SupplierProductView> ViewShoppingListBySupplierId(int supplierId)
        {
            List<SupplierProductView> ShoppingList = new List<SupplierProductView>();
            using (SqlConnection conn = new SqlConnection(@"Data Source=.;Initial Catalog=OrdersManagmentSystem;Integrated Security=True"))
            {
                SqlCommand cmd = new SqlCommand("SELECT_LIST_PRODUCTS_BY_SUPPLIER", conn);
                cmd.Parameters.Add(new SqlParameter("@id", supplierId));
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                while (reader.Read())
                {
                    SupplierProductView sp = new SupplierProductView();
                    sp.FirstNameSup = (string)reader["FirstName"];
                    sp.Instock = (int)reader["InStock"];
                    sp.ProducPrice = (int)reader["Price"];
                    sp.ProductName = (string)reader["Name"];
                    sp.ProductID = (int)reader["Id"];
                    ShoppingList.Add(sp);
                }
                cmd.Connection.Close();

                return ShoppingList;
            }

        }
        public List<Product> ViewAllProducts()
        {
            List<Product> AllProducts = new List<Product>();
            using (SqlConnection conn = new SqlConnection(@"Data Source=.;Initial Catalog=OrdersManagmentSystem;Integrated Security=True"))
            {
                SqlCommand cmd = new SqlCommand("SELECT_ALL_PRODUCTS", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                while (reader.Read())
                {
                    Product p = new Product();
                    p.Id = (int)reader["Id"];
                    p.Idsupplier = (int)reader["Id_Supplier"];
                    p.Price = (int)reader["Price"];
                    p.Name = (string)reader["Name"];
                    p.InStock = (int)reader["InStock"];

                    AllProducts.Add(p);
                }
            }

            return AllProducts;
        }
        public void InsertNewProduct (Product p)
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=.;Initial Catalog=OrdersManagmentSystem;Integrated Security=True"))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT_NEW_PRODUCT", conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@NAME", p.Name));
                    cmd.Parameters.Add(new SqlParameter("@IDSUP", p.Idsupplier));
                    cmd.Parameters.Add(new SqlParameter("@PRICE", p.Price));
                    cmd.Parameters.Add(new SqlParameter("@INTSTOCK", p.InStock));
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

            }
        }
        public bool IsProductExists(string productName)
        {
            int count = 0;
            using (SqlConnection conn = new SqlConnection(@"Data Source=.;Initial Catalog=OrdersManagmentSystem;Integrated Security=True"))
            {
                SqlCommand cmd = new SqlCommand("SELECT_PRODUCT_BY_NAME", conn);
                cmd.Parameters.Add(new SqlParameter("@NAME", productName));
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                count = cmd.ExecuteNonQuery();
            }

            return count > 0;
        }
        public bool IsProductExistsByNameAndIdSupllier(string productName, int idSupplier)
        {
            int count = 0;
            using (SqlConnection conn = new SqlConnection(@"Data Source=.;Initial Catalog=OrdersManagmentSystem;Integrated Security=True"))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT_PRODUCT_BY_NAME_AND_IDSUP", conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@NAME", productName));
                    cmd.Parameters.Add(new SqlParameter("@IDSUPPLIER", idSupplier));
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    count = cmd.ExecuteNonQuery();
                }
                
            }

            return count > 0;
        }
        public void UpdateSupplierProduct (int id, int quantity)
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=.;Initial Catalog=OrdersManagmentSystem;Integrated Security=True"))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE_SUPPLIER_PRODUCT", conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@ID", id));
                    cmd.Parameters.Add(new SqlParameter("@QUANTITY", quantity));
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

            }
        }
        public Product GetProductByName(string productName)
        {
            Product p = new Product();
            using (SqlConnection conn = new SqlConnection(@"Data Source=.;Initial Catalog=OrdersManagmentSystem;Integrated Security=True"))
            {
                SqlCommand cmd = new SqlCommand("SELECT_PRODUCT_BY_NAME", conn);
                cmd.Parameters.Add(new SqlParameter("@NAME", productName));
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        p.Id = (int)reader["Id"];
                        p.Idsupplier = (int)reader["Id_Supplier"];
                        p.InStock = (int)reader["InStock"];
                        p.Name = (string)reader["Name"];
                        p.Price = (int)reader["Price"];
                    }
                }
            }
            return p;
        }

        public void OrderAProduct(int cId, int pId, int quantity, int totalPrice)
        {
            Order o = new Order()
            {
                IdCustomer = cId,
                IdProduct = pId,
                QuantityOfOrders = quantity,
                TotalPrice = totalPrice
            };

            orderBl.InsertNewOrder(o);
            UpdateInStockByProductId(pId, quantity);
            Console.WriteLine("Order approved");

        }
    }
}
