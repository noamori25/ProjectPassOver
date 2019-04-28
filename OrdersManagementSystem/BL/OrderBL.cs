using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagementSystem.BL
{
    public class OrderBL
    {
        public void InsertNewOrder(Order order)
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=.;Initial Catalog=OrdersManagmentSystem;Integrated Security=True"))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT_NEW_ORDER", conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@IdCustomer", order.IdCustomer));
                    cmd.Parameters.Add(new SqlParameter("@IdProduct", order.IdProduct));
                    cmd.Parameters.Add(new SqlParameter("@Quantity", order.QuantityOfOrders));
                    cmd.Parameters.Add(new SqlParameter("@TotalPrice", order.TotalPrice));
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<OrderProductView> ViewShoppingListByCustomerId(int customerId)
        {
            List<OrderProductView> ShoppingList = new List<OrderProductView>();
            using (SqlConnection conn = new SqlConnection(@"Data Source=.;Initial Catalog=OrdersManagmentSystem;Integrated Security=True"))
            {
                SqlCommand cmd = new SqlCommand("SELECT_LIST_ORDERS_BY_CUSTOMER", conn);
                cmd.Parameters.Add(new SqlParameter("@ID", customerId));
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                while (reader.Read())
                {
                    OrderProductView op = new OrderProductView();
                    op.OrderId = (int)reader["Id"];
                    op.ProductID = (int)reader["Id_Product"];
                    op.CustomerID = (int)reader["Id_Customer"];
                    op.QuantityOfOrders = (int)reader["Quantity_Of_Orders"];
                    op.TotalPrice = (int)reader["Total_Price"];
                    op.ProductName = (string)reader["Name"];
                    ShoppingList.Add(op);
                }
                cmd.Connection.Close();

                return ShoppingList;


            }
        }
    }
}
