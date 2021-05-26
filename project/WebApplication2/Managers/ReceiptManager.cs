using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WebApplication2.Helpers;
using WebApplication2.Models;

namespace WebApplication2.Managers
{
    public class ReceiptManager : DBBase
    {
        //新增發票
        public void CreateReceipt(ReceiptModel model)
        {
            string queryString =
                $@" INSERT INTO Receipt ( ReceiptNumber, Date, Company, Amount, Revenue_Expense, CreateDate, Creator) 
                    VALUES ( @ReceiptNumber, @Date, @Company, @Amount, @Revenue_Expense, @CreateDate, @Creator)";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@ReceiptNumber", model.ReceiptNumber),
                new SqlParameter("@Date", model.Date),
                new SqlParameter("@Company", model.Company),
                new SqlParameter("@Amount", model.Amount),
                new SqlParameter("@Revenue_Expense", (int)model.Revenue_Expense),
                new SqlParameter("@CreateDate", DateTime.Now),
                new SqlParameter("@Creator", "Brian"),
            };

            this.ExecuteNonQuery(queryString, parameters);

        }

        //修改發票
        public void UpdateReceipt(ReceiptModel model)
        {
            string queryString =
                $@" UPDATE Receipt
                        SET ReceiptNumber = @ReceiptNumber, 
                            Date = @Date, 
                            Company = @Company, 
                            Amount = @Amount, 
                            Revenue_Expense = @Revenue_Expense,
                            ModifyDate = @ModifyDate,
                            Modifier = @Modifier
                    WHERE ReceiptNumber = @ReceiptNumber;
                ";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@ReceiptNumber", model.ReceiptNumber),
                new SqlParameter("@Date", model.Date),
                new SqlParameter("@Company", model.Company),
                new SqlParameter("@Amount", model.Amount),
                new SqlParameter("@Revenue_Expense", (int)model.Revenue_Expense),
                new SqlParameter("@ModifyDate", DateTime.Now),
                new SqlParameter("@Modifier", "Brian"),
            };

            this.ExecuteNonQuery(queryString, parameters);

        }

        //刪除發票
        public void DeleteReceipt(string ReceiptNumber)
        {
            string queryString =
                $@" UPDATE Receipt
                        SET  DeleteDate = @DeleteDate,
                             Deleter = @Deleter
                    WHERE ReceiptNumber = @ReceiptNumber;
                ";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@ReceiptNumber", ReceiptNumber),
                new SqlParameter("@DeleteDate", DateTime.Now),
                new SqlParameter("@Deleter", "Brian"),
            };

            this.ExecuteNonQuery(queryString, parameters);

        }

        //查詢發票清單
        public List<ReceiptModel> GetReceipts(string company, decimal? minPrice, decimal? maxPrice, int? R_E , out int totalSize, int currentPage = 1, int pageSize = 10)
        {
            List<SqlParameter> dbParameters = new List<SqlParameter>();

            #region ProcessConditions
            List<string> conditions = new List<string>();

            if (!string.IsNullOrEmpty(company))
            {
                conditions.Add(" Company LIKE '%' + @company + '%'");
                dbParameters.Add(new SqlParameter("@company", company));
            }

            if (minPrice.HasValue && maxPrice.HasValue)
            {
                if (minPrice.Value > maxPrice.Value)
                {
                    decimal temp = minPrice.Value;
                    minPrice = maxPrice.Value;
                    maxPrice = temp;
                }

                conditions.Add(" Amount BETWEEN @minPrice AND @maxPrice");
                dbParameters.Add(new SqlParameter("@minPrice", minPrice.Value));
                dbParameters.Add(new SqlParameter("@maxPrice", maxPrice.Value));
            }
            else if (minPrice.HasValue)
            {
                conditions.Add(" Amount >= @minPrice");
                dbParameters.Add(new SqlParameter("@minPrice", minPrice.Value));
            }
            else if (maxPrice.HasValue)
            {
                conditions.Add(" Amount <= @maxPrice");
                dbParameters.Add(new SqlParameter("@maxPrice", maxPrice.Value));
            }

            if (R_E.HasValue)
            {
                conditions.Add(" Revenue_Expense = @r_e");
                dbParameters.Add(new SqlParameter("@r_e", R_E.Value));
            }
            string filterConditions =
                (conditions.Count > 0)
                    ? (" WHERE " + string.Join(" AND ", conditions))
                    : string.Empty;
            #endregion


            string queryString =
                $@" 
                    SELECT TOP {10} * FROM
                    (
                        SELECT
                            ROW_NUMBER() OVER(ORDER BY Date DESC) AS RowNumber,
                            ReceiptNumber,
                            Date,
                            Company.Name AS Company,
                            Amount,
                            Revenue_Expense
                        FROM
                            (
							SELECT *
							FROM Receipt
							WHERE Deleter IS NULL
						    ) AS TempR
                        JOIN Company
                        ON TempR.Company = Company.Cid
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

            var dt = this.GetDataTable(queryString, dbParameters);

            List<ReceiptModel> list = new List<ReceiptModel>();

            foreach(DataRow dr in dt.Rows)
            {
                ReceiptModel model = new ReceiptModel();
                model.ReceiptNumber = (string)dr["ReceiptNumber"];
                model.Date = (DateTime)dr["Date"];
                model.Company = (string)dr["Company"];
                model.Amount = (Decimal)dr["Amount"];
                model.Revenue_Expense = (Revenue_Expense)dr["Revenue_Expense"];

                list.Add(model);
            }

            int? totalSize2 = this.GetScale(countQuery, dbParameters) as int?;
            totalSize = (totalSize2.HasValue) ? totalSize2.Value : 0;

            return list;
        }

        //查詢單一發票
        public ReceiptModel GetReceipt(string ReceiptNumber)
        {
            string queryString =
                $@" SELECT * FROM Receipt
                    WHERE ReceiptNumber = @ReceiptNumber
                ";

            List<SqlParameter> dbParameters = new List<SqlParameter>()
            {
                new SqlParameter("@ReceiptNumber", ReceiptNumber),
            };

            ReceiptModel model = null;
            var dt = this.GetDataTable(queryString, dbParameters);
            if (dt.Rows.Count != 0)
            {
                model = new ReceiptModel();
                model.ReceiptNumber = (string)dt.Rows[0]["ReceiptNumber"];
                model.Date = (DateTime)dt.Rows[0]["Date"];
                model.Company = dt.Rows[0]["Company"].ToString();
                model.Amount = (Decimal)dt.Rows[0]["Amount"];
                model.Revenue_Expense = (Revenue_Expense)dt.Rows[0]["Revenue_Expense"];
            }

            return model;

        }
    }
}