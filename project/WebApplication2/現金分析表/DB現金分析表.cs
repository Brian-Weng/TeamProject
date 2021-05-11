using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static WebApplication2.現金分析表.月份金額;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication2.現金分析表
{
    public class DB現金分析表
    {
        const string connectionstring = "Data Source=localhost\\SQLExpress;Initial Catalog=財經系統;Integrated Security=true";
        const int month = 12;
        public static List<monthMoney> monthTotalOutIn(int year, string outIn) //一個儲存每月收入/支出的方法
        {

            using (SqlConnection con = new SqlConnection(connectionstring))
            {

                try
                {
                    con.Open();

                    List<monthMoney> monthMoney = new List<monthMoney>();

                    for (var i = 1; i <= month; i++)
                    {
                        string mon;
                        if (i < 10)//如果月份小於10在數字前面加一個"-0"，在最後加入"%"代表SQL的模糊查詢語法  (資料庫存入的格式為yyyy-MM-dd)
                        {
                            mon = "-0" + i.ToString() + "%";
                        }
                        else
                        {
                            mon = "-" + i.ToString() + "%"; //大於零在數字前面加一個"-"，在最後加入"%"代表SQL的模糊查詢語法
                        }
                        string querystring = @"SELECT  SUM(Money) FROM Assets where [IsDelete] = 'false' and IncomeAndExpenditure =@outIn and [date] like @date;";
                        SqlCommand command = new SqlCommand(querystring, con);
                        command.Parameters.AddWithValue("@date", year + mon);
                        command.Parameters.AddWithValue("@outIn", outIn);
                        int? total = 0;

                        total = command.ExecuteScalar() as int?; //ExecuteScalar()輸出一筆資料


                        if (total != null)
                        { //不等於null就存入數字
                            monthMoney.Add(new monthMoney()
                            {
                                monthInOut = total
                            });
                        }
                        else
                        {
                            monthMoney.Add(new monthMoney()//等於null就存入0
                            {
                                monthInOut = 0
                            });
                        }
                    }
                    return monthMoney;
                }
                catch (Exception e)
                {
                    HttpContext.Current.Response.Write(e);
                    return null;
                }
            }
        }
        public static List<monthBalance> monthBalance(int year)
        {
            

            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                try
                {
                    con.Open();
                    List<monthBalance> monthBalances = new List<monthBalance>();

                    for (int i = 1; i <= month; i++)
                    {
                        string mon;
                        if (i < 10)//如果月份小於10在數字前面加一個"-0"，在最後加入"%"代表SQL的模糊查詢語法  (資料庫存入的格式為yyyy-MM-dd)
                        {
                            mon = "-0" + i.ToString() + "%";
                        }
                        else
                        {
                            mon = "-" + i.ToString() + "%"; //大於零在數字前面加一個"-"，在最後加入"%"代表SQL的模糊查詢語法
                        }
                        string querystring = @"SELECT TOP (1) [total] FROM [Assets] where [IsDelete] = 'false' and [date] like @date ORDER BY ID DESC;";

                        string date = year + mon;

                        SqlCommand command = new SqlCommand(querystring, con);

                        command.Parameters.AddWithValue("@date", date);

                        int? balance = 0;

                        balance = command.ExecuteScalar() as int?;

                        if (balance != null)
                        {
                            monthBalances.Add(new monthBalance()
                            {
                                balance = balance
                            });
                        }
                        else
                        {
                            monthBalances.Add(new monthBalance()
                            {
                                balance = 0
                            });
                        }

                    }
                    return monthBalances;
                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }
        public static DataTable showSub()
        {
            string querystring = @"select [SubList] from [SubList];";

            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand command = new SqlCommand(querystring, con);

                try
                {
                    con.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    DataTable dt = new DataTable();

                    dt.Load(reader);

                    reader.Close();

                    return dt;
                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }
        public static List<subMoneyIn> subMoneyIn(int year, string outIn)
        {
            // string querystringTotal = @"select Sum(Money) as money,subid from assets where [date] like @date and not IsDelete = 'true' and IncomeAndExpenditure ='收入' group by SubID;";
            string querystringSubList = @"select count(SubList) from SubList;";

            string showSubLIst = @"select [SubList] from [SubList];";

            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand cmdSubCount = new SqlCommand(querystringSubList, con);
                SqlCommand cmdShow = new SqlCommand(showSubLIst, con);
                try
                {
                    con.Open();

                    int SubCount = (int)cmdSubCount.ExecuteScalar();

                    List<subMoneyIn> subMoneyInceome = new List<subMoneyIn>();

                    int?[] SubArr = new int?[month];

                    for (int i = 0; i < SubCount; i++)
                    {
                        SqlDataReader SubReader = cmdShow.ExecuteReader();
                        DataTable SubDt = new DataTable();
                        SubDt.Load(SubReader);
                        SubReader.Close();

                        string sub = SubDt.Rows[i].Field<string>("SubList");

                        if (sub != null)
                        {

                            for (int y = 1; y <= month; y++)
                            {
                                string querystringTotal = @"select Sum(Money) as money from assets where date like @date and sub = @sub and not IsDelete = 'true' and IncomeAndExpenditure = @outIn;";
                                SqlCommand cmdTotal = new SqlCommand(querystringTotal, con);
                                cmdTotal.Parameters.AddWithValue("@outIn", outIn);
                                cmdTotal.Parameters.AddWithValue("@sub", sub);

                                string mon;
                                if (y < 10)//如果月份小於10在數字前面加一個"-0"，在最後加入"%"代表SQL的模糊查詢語法  (資料庫存入的格式為yyyy-MM-dd)
                                {
                                    mon = "-0" + y.ToString() + "%";
                                }
                                else
                                {
                                    mon = "-" + y.ToString() + "%"; //大於零在數字前面加一個"-"，在最後加入"%"代表SQL的模糊查詢語法
                                }

                                string date = year + mon;
                                cmdTotal.Parameters.AddWithValue("@date", date);

                                int? SubMoneyTotal = cmdTotal.ExecuteScalar() as int?;

                                if(SubMoneyTotal == null) { SubMoneyTotal = 0; }

                                SubArr[y-1] = SubMoneyTotal;
                            }

                            if(SubArr[0]!=0 || SubArr[1] != 0 || SubArr[2] != 0 || SubArr[3] != 0 || SubArr[4] != 0 ||
                               SubArr[5] != 0 || SubArr[6] != 0 || SubArr[7] != 0 || SubArr[8] != 0 || SubArr[9] != 0 ||
                               SubArr[10] != 0 || SubArr[11] != 0) { 

                            subMoneyInceome.Add(new subMoneyIn
                            {
                                subType = sub,
                                Jan = SubArr[0],
                                Feb = SubArr[1],
                                Mar = SubArr[2],
                                Apr = SubArr[3],
                                May = SubArr[4],
                                Jun = SubArr[5],
                                Jul = SubArr[6],
                                Aug = SubArr[7],
                                Sep = SubArr[8],
                                Oct = SubArr[9],
                                Nov = SubArr[10],
                                Dec = SubArr[11]
                            });

                            }
                        }
                        else
                        {
                            continue;
                        }

                    }



                    return subMoneyInceome;

                }
                catch (Exception e)
                {
                    HttpContext.Current.Response.Write(e);
                    return null;
                }
            }
        }
    }
}