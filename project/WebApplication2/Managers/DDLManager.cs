using System;
using System.Data;
using System.Data.SqlClient;


namespace WebApplication2.Managers
{
    public class DDLManager
    {
        public static DataTable GetCompanyDDL()
        {
            string connectionString = "Data Source=localhost\\SQLExpress;Initial Catalog=Financials; Integrated Security=true";

            string queryString =
                $@"SELECT * FROM Company";

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    reader.Close();
                    return dataTable;
                }
                catch(Exception ex)
                {
                    throw;
                }
            }
        }
    }
}