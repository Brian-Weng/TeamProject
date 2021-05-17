using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace WebApplication2.Managers
{
    public class ReceiptManager
    {
        public void CreateReceipt(ReceiptModel model)
        {
            string connectionString = "Data Source=localhost\\SQLExpress;Initial Catalog=Financials; Integrated Security=true";

            string queryString =
                $@" INSERT INTO Receipt ( ReceiptNumber, Date, Company, Amount, Revenue_Expense, CreateDate, Creator) 
                    VALUES ( @ReceiptNumber, @Date, @Company, @Amount, @Revenue_Expense, @CreateDate, @Creator)";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@ReceiptNumber", model.ReceiptNumber);
                command.Parameters.AddWithValue("@Date", model.Date);
                command.Parameters.AddWithValue("@Company", model.Company);
                command.Parameters.AddWithValue("@Amount", model.Amount);
                //將Enum值轉成相對應的int
                command.Parameters.AddWithValue("@Revenue_Expense", (int)model.Revenue_Expense);
                command.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                command.Parameters.AddWithValue("@Creator", "Brian");

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public void UpdateReceipt(ReceiptModel model)
        {
            string connectionString = "Data Source=localhost\\SQLExpress;Initial Catalog=Financials; Integrated Security=true";

            string queryString =
                $@" UPDATE Receipt
                        SET ReceiptNumber = @ReceiptNumber, 
                            Date = @Date, 
                            Company = @Company, 
                            Amount = @Amount, 
                            Revenue_Expense = @Revenue_Expense
                    WHERE ReceiptNumber = @ReceiptNumber;
                ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@ReceiptNumber", model.ReceiptNumber);
                command.Parameters.AddWithValue("@Date", model.Date);
                command.Parameters.AddWithValue("@Company", model.Company);
                command.Parameters.AddWithValue("@Amount", model.Amount);
                //將Enum值轉成相對應的int
                command.Parameters.AddWithValue("@Revenue_Expense", (int)model.Revenue_Expense);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public void DeleteReceipt(string ReceiptNumber)
        {
            string connectionString = "Data Source=localhost\\SQLExpress;Initial Catalog=Financials; Integrated Security=true";

            string queryString =
                $@" UPDATE Receipt
                        SET  DeleteDate = @DeleteDate,
                             Deleter = @Deleter
                    WHERE ReceiptNumber = @ReceiptNumber;
                ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@ReceiptNumber", ReceiptNumber);
                command.Parameters.AddWithValue("@DeleteDate", DateTime.Now);
                command.Parameters.AddWithValue("@Deleter", "Brian");

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public List<ReceiptModel> GetReceipts(string company, decimal? minPrice, decimal? maxPrice, int? R_E , out int totalSize, int currentPage = 1, int pageSize = 10)
        {
            string connectionString = "Data Source=localhost\\SQLExpress;Initial Catalog=Financials; Integrated Security=true";

            List<string> conditions = new List<string>();

            if (!string.IsNullOrEmpty(company))
                conditions.Add(" Company LIKE '%' + @company + '%'");
            
            if(minPrice.HasValue && maxPrice.HasValue)
            {
                if(minPrice.Value > maxPrice.Value)
                {
                    decimal temp = minPrice.Value;
                    minPrice = maxPrice.Value;
                    maxPrice = temp;
                }

                conditions.Add(" Amount BETWEEN @minPrice AND @maxPrice");
            }
            else if(minPrice.HasValue)
                conditions.Add(" Amount >= @minPrice");
            else if(maxPrice.HasValue)
                conditions.Add(" Amount <= @maxPrice");

            if (R_E.HasValue)
                conditions.Add(" Revenue_Expense = @r_e");

            string filterConditions =
                (conditions.Count > 0)
                    ? (" WHERE " + string.Join(" AND ", conditions))
                    : string.Empty;

            string queryString =
                $@" 
                    SELECT TOP {10} * FROM
                    (
                        SELECT
                            ROW_NUMBER() OVER(ORDER BY Date DESC) AS RowNumber,
                            ReceiptNumber,
                            Date,
                            Company,
                            Amount,
                            Revenue_Expense
                        FROM
                            (
							SELECT *
							FROM Receipt
							WHERE Deleter IS NULL
						    ) AS TempD
                        {filterConditions}
                    ) AS TempT
                    WHERE RowNumber > {pageSize * (currentPage - 1)}
                    ORDER BY TempT.Date DESC
                ";

            string countQuery =
                $@" SELECT COUNT(ReceiptNumber)
                    FROM Receipt
                    {filterConditions}
                ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                if (!string.IsNullOrEmpty(company))
                    command.Parameters.AddWithValue("@company", company);
                if (minPrice.HasValue && maxPrice.HasValue)
                {
                    if (minPrice.Value > maxPrice.Value)
                    {
                        decimal temp = minPrice.Value;
                        minPrice = maxPrice.Value;
                        maxPrice = temp;
                    }

                    command.Parameters.AddWithValue("@minPrice", minPrice.Value);
                    command.Parameters.AddWithValue("@maxPrice", maxPrice.Value);
                }
                else if (minPrice.HasValue)
                    command.Parameters.AddWithValue("@minPrice", minPrice.Value);
                else if (maxPrice.HasValue)
                    command.Parameters.AddWithValue("@maxPrice", maxPrice.Value);

                if (R_E.HasValue)
                    command.Parameters.AddWithValue("@r_e", R_E.Value);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();//從最上方1行1行下來將SQL Sever的資料流入reader
                    List<ReceiptModel> list = new List<ReceiptModel>();

                    while (reader.Read())
                    {
                        ReceiptModel model = new ReceiptModel();
                        model.ReceiptNumber = (string)reader["ReceiptNumber"];
                        model.Date = (DateTime)reader["Date"];
                        model.Company = (string)reader["Company"];
                        model.Amount = (Decimal)reader["Amount"];
                        model.Revenue_Expense = (Revenue_Expense)reader["Revenue_Expense"];

                        list.Add(model);
                    }
                    reader.Close();

                    command.CommandText = countQuery;
                    int? totalSize2 = command.ExecuteScalar() as int?;
                    totalSize = (totalSize2.HasValue) ? totalSize2.Value : 0;

                    return list;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public ReceiptModel GetReceipt(string ReceiptNumber)
        {
            string connectionString = "Data Source=localhost\\SQLExpress;Initial Catalog=Financials; Integrated Security=true";

            string queryString =
                $@" SELECT * FROM Receipt
                    WHERE ReceiptNumber = @ReceiptNumber
                ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@ReceiptNumber", ReceiptNumber);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();//從最上方1行1行下來將SQL Sever的資料流入reader

                    ReceiptModel model = null;
                    
                    while (reader.Read())
                    {   //假設有reader有讀到資料,才設定model的初始值
                        model = new ReceiptModel();
                        model.ReceiptNumber = (string)reader["ReceiptNumber"];
                        model.Date = (DateTime)reader["Date"];
                        model.Company = (string)reader["Company"];
                        model.Amount = (Decimal)reader["Amount"];
                        model.Revenue_Expense = (Revenue_Expense)reader["Revenue_Expense"];
                    }
                    reader.Close();

                    return model;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}