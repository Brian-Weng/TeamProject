using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WebApplication2.Helpers;

namespace WebApplication2.Managers
{
    public class DDLManager : DBBase
    {
        public DataTable GetCompanyDDL()
        {
            //string connectionString = "Data Source=localhost\\SQLExpress;Initial Catalog=Financials; Integrated Security=true";

            string queryString =
                $@"SELECT * FROM Company";

            List<SqlParameter> dbParameters = new List<SqlParameter>();
   
            var dt = this.GetDataTable(queryString, dbParameters);

            return dt;
            //using(SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    SqlCommand command = new SqlCommand(queryString, connection);

            //    try
            //    {
            //        connection.Open();
            //        SqlDataReader reader = command.ExecuteReader();

            //        DataTable dataTable = new DataTable();
            //        dataTable.Load(reader);
            //        reader.Close();
            //        return dataTable;
            //    }
            //    catch(Exception ex)
            //    {
            //        throw;
            //    }
            //}
        }
    }
}