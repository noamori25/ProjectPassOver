using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPassOver
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator c = new Calculator();
           // c.DeleteResults();
           // c.DeleteYAndX();
           // c.InsertXAndY();
            // c.CrossJoinIntoResultsTable();
            c.UpdateResultsColumnInResultsTable();
        }
    }
}
