using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication2.Helpers
{
    public class DBBase
    {   
        //取得DataTable
        public DataTable GetDataTable(string dbCommand, List<SqlParameter> parameters)
        {
            string connectionString = this.GetConnectionString();

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(dbCommand, connection);
                command.Parameters.AddRange(parameters.ToArray());

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    reader.Close();

                    return dt;
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
        }

        //取得第一筆資料
        public object GetScale(string dbCommand, List<SqlParameter> parameters)
        {
            string connectionString = this.GetConnectionString();

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(dbCommand, connection);

                List<SqlParameter> parameters2 = new List<SqlParameter>();
                foreach(var item in parameters)
                {
                    parameters2.Add(new SqlParameter(item.ParameterName, item.Value));
                }

                command.Parameters.AddRange(parameters2.ToArray());

                try
                {
                    connection.Open();
                    return command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //取得受影響列數
        public int ExecuteNonQuery(string dbCommand, List<SqlParameter> parameters)
        {
            string connectionString = this.GetConnectionString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(dbCommand, connection);

                List<SqlParameter> parameters2 = new List<SqlParameter>();
                foreach(var item in parameters)
                {
                    parameters2.Add(new SqlParameter(item.ParameterName, item.Value));
                }

                command.Parameters.AddRange(parameters2.ToArray());

                try
                {
                    connection.Open();
                    int totalChange = command.ExecuteNonQuery();
                    return totalChange;
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
        }

        //取得連線字串
        private string GetConnectionString()
        {
            var manage = System.Configuration.ConfigurationManager.ConnectionStrings["FinancialsDataBase"];

            if (manage == null)
                return string.Empty;
            else
                return manage.ConnectionString;
        }
    }
}