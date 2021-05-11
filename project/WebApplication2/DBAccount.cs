using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication2
{
    public class DBAccount
    {
        public static DataTable AccountCheck(string account)
        {
            string connectionstring = "Data Source=localhost\\SQLExpress;Initial Catalog=財經系統;Integrated Security=true";
            string querystring = @"SELECT  * from [user] where account = @account;";

            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand command = new SqlCommand(querystring, con);
                command.Parameters.AddWithValue("@account", account);

                try
                {
                    con.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    DataTable dt = new DataTable();

                    dt.Load(reader);

                    reader.Close();

                    return dt;
                }
                catch (Exception ex)
                {
                    HttpContext.Current.Response.Write(ex);

                    return null;
                }
            }

        }
    }
}