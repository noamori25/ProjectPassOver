using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPassOver
{
    class Calculator
    {
        int x = 1;
        int y;

        public void InsertXAndY()
        {
            while (x > 0)
            {
                Console.WriteLine("please enter a number");
                x = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("please enter a number");
                y = Convert.ToInt32(Console.ReadLine());

                if (x > 0)
                {
                    using (SqlConnection conn = new SqlConnection(@"Data Source=.;Initial Catalog=Calculator;Integrated Security=True"))
                    {
                        SqlCommand cmd = new SqlCommand("INSERT_X", conn);
                        cmd.Parameters.Add(new SqlParameter("@x", x));
                        cmd.Connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                        cmd.Connection.Close();

                        SqlCommand cmd1 = new SqlCommand("INSERT_Y", conn);
                        cmd1.Parameters.Add(new SqlParameter("@y", y));
                        cmd1.Connection.Open();
                        cmd1.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader1 = cmd1.ExecuteReader(CommandBehavior.Default);
                        cmd1.Connection.Close();
                    }
                }
              
            }
        }
        public void DeleteYAndX ()
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=.;Initial Catalog=Calculator;Integrated Security=True"))
            {
                SqlCommand cmd = new SqlCommand("DELETE_X_AND_Y", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                cmd.Connection.Close();
            }
        }
        public void DeleteResults ()
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=.;Initial Catalog=Calculator;Integrated Security=True"))
            {
                SqlCommand cmd = new SqlCommand("DELETE_RESULTS_TABLE", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                cmd.Connection.Close();
            }
        }
        public void CrossJoinIntoResultsTable ()
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=.;Initial Catalog=Calculator;Integrated Security=True"))
            {
                SqlCommand cmd = new SqlCommand("CROSS_JOIN_X_OPERATION_Y", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                cmd.Connection.Close();
            }
        }
        public void UpdateResultsColumnInResultsTable()
        {
            int id;
            int x;
            int y;
            string operation;
            int result = 0;

            using (SqlConnection conn = new SqlConnection(@"Data Source=.;Initial Catalog=Calculator;Integrated Security=True"))
            {
                SqlCommand cmd = new SqlCommand("SELECT_ALL_RESULTS", conn);

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    id = (int)dt.Rows[i]["Id"];
                    x = (int)dt.Rows[i]["X"];
                    y = (int)dt.Rows[i]["Y"];
                    operation = (string)dt.Rows[i]["Operation"];

                    switch (operation)
                    {
                        case "+":
                            result = x + y;
                            break;
                        case "-":
                            result = x - y;
                            break;
                        case "/":
                            result = x / y;
                            break;
                        case "*":
                            result = x * y;
                            break;
                    }

                    cmd = new SqlCommand("UPDATE_RESULT_COLUMN", conn);
                    cmd.Parameters.Add(new SqlParameter("@RESULT", result));
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

                cmd.Connection.Close();
            }
        }
    }
}
