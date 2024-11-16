using System;
using System.Data;
using System.Data.SqlClient;

namespace ShoeStore.Models
{
    class Database
    {
        private string svrName = "MY-PC";
        private string dbName = "ShoeStore";
        private bool intergratedMode = true; // Set to true for Windows Authentication

        private SqlConnection sqlconn;

        public Database()
        {
            string connStr = intergratedMode
                ? $"server={svrName}; database={dbName}; Integrated Security=True"
                : $"server={svrName}; database={dbName};";

            sqlconn = new SqlConnection(connStr);
        }

        public DataTable Execute(string strQuery)
        {
            DataTable resultTable = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(strQuery, sqlconn);
                da.Fill(resultTable);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error executing query: " + ex.Message);
            }
            return resultTable;
        }

        public bool ExecuteNonQuery(string strQuery)
        {
            try
            {
                sqlconn.Open();
                using (SqlCommand sqlcom = new SqlCommand(strQuery, sqlconn))
                {
                    sqlcom.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error executing non-query: " + ex.Message);
                return false;
            }
            finally
            {
                sqlconn.Close();
            }
        }
    }
}
