using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication2
{
    public class DB
    {
        const string connectionstring = "Data Source=localhost\\SQLExpress;Initial Catalog=財經系統;Integrated Security=true";
        public static Boolean confirmAcc(string account)
        {
            string querystr = $@"select Account from [User] where Account = @account;";

            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand command = new SqlCommand(querystr, con);

                command.Parameters.AddWithValue("@account", account);

                try
                {
                    con.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (!reader.Read())
                    {
                        reader.Close();
                        return true;
                    }
                    else
                    {
                        reader.Close();
                        return false;
                    }


                }
                catch (Exception e)
                {
                    HttpContext.Current.Response.Write(e);

                    return true;
                }
            }
        }
        public static Boolean confirmPw(string pw)
        {
            string querystr = $@"select password from [User] where password = @pw;";

            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand command = new SqlCommand(querystr, con);

                command.Parameters.AddWithValue("@pw", pw);

                try
                {
                    con.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (!reader.Read())
                    {
                        reader.Close();
                        return true;
                    }
                    else
                    {
                        reader.Close();
                        return false;
                    }


                }
                catch (Exception e)
                {
                    HttpContext.Current.Response.Write(e);

                    return true;
                }
            }
        }
        public static DataTable showTotalDB()
        {
            string querystring = @"SELECT  * from Assets where not [Isdelete] = 'true';";

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
                catch (Exception ex)
                {
                    HttpContext.Current.Response.Write(ex);

                    return null;
                }
            }

        }
        public static void insertDB(string date, string sub, string usefor, int? money, string outIn, int subID)
        {
            string queryTotalMoney = @"select top 1 [Total] from [Assets] where isdelete = 'false' order by [ID] desc;";

            string querystr = @"insert into Assets (Date,Sub,Usefor,Money,[IncomeAndExpenditure],Total,[IsDelete],SubID)
                        values (@date,@sub,@usefor,@money,@outIn,@total,'false',@subID);";
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand command = new SqlCommand(querystr, con);
                SqlCommand commandMoney = new SqlCommand(queryTotalMoney, con);

                try
                {
                    con.Open();
                    SqlDataReader reader = commandMoney.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    int? total;
                    if (dt.Rows.Count > 0) {
                        total = dt.Rows[0].Field<int?>("Total");
                    }
                    else
                    {
                        total = 0;
                    }

                    

                    if(total == null)
                    {
                        total = 0;
                    }

                    if (outIn == "收入")
                    {
                        total = total + money;
                    }
                    else
                    {
                        total = total - money;
                    }
                
                  
                    command.Parameters.AddWithValue("@date", date);
                    command.Parameters.AddWithValue("@sub",sub);
                    command.Parameters.AddWithValue("@usefor",usefor);
                    command.Parameters.AddWithValue("@money",money);
                    command.Parameters.AddWithValue("@outIn", outIn);
                    command.Parameters.AddWithValue("@total", total);
                    command.Parameters.AddWithValue("@subID", subID);
                    command.ExecuteNonQuery();
                }
                catch(Exception e)
                {
                    HttpContext.Current.Response.Write(e);
                }





            }
        }
        public static void deleteDB(string id)
        {
            string querystr = @"update assets set [IsDelete] = 'true' where ID = @id;
                                select * from assets where ID=@id;";

            string newTotal;
           //string querystr = @"delete Assets where ID=@ID;";

            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand command = new SqlCommand(querystr, con);
                command.Parameters.AddWithValue("@ID", id);
                
                
                
                try
                {
                    con.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    DataTable dt = new DataTable();

                    dt.Load(reader);

                    string outIn = dt.Rows[0].Field<string>("IncomeAndExpenditure");

                    int money = dt.Rows[0].Field<int>("Money");

                    command.ExecuteNonQuery();

                    con.Close();
                }catch(Exception e)
                {
                    HttpContext.Current.Response.Write(e);
                }
            }
        }
        public static DataTable searchDB(string id)
        {
            string querystring = @"SELECT  * from Assets where ID=@ID;";
            
            using(SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand command = new SqlCommand(querystring, con);

                command.Parameters.AddWithValue("@ID", id);

                try
                {
                    con.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    DataTable dt = new DataTable();

                    dt.Load(reader);

                    reader.Close();

                    return dt;
                }catch(Exception e)
                {
                    HttpContext.Current.Response.Write(e);
                    return null;
                }
            }
        }
        public static bool UpdateDB(string id,string date, string sub, string usefor, int money, string outIn, int total)
        {
            string querystr = @"update [Assets] set Date = @date, Sub = @sub, Usefor = @usefor, 
                                Money = @money, IncomeAndExpenditure = @outIn, Total = @total where ID = @id and isdelete = 'false';"; //20210413 修改
            using(SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand command = new SqlCommand(querystr, con);
                command.Parameters.AddWithValue("@date", date);
                command.Parameters.AddWithValue("@sub", sub);
                command.Parameters.AddWithValue("@usefor", usefor);
                command.Parameters.AddWithValue("@money", money);
                command.Parameters.AddWithValue("@outIn", outIn);
                command.Parameters.AddWithValue("@total", total);
                command.Parameters.AddWithValue("@id", id);

                try
                {
                    con.Open();

                    if (total > 0)
                    {
                        command.ExecuteNonQuery();
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                    

                }catch(Exception e)
                {
                    HttpContext.Current.Response.Write(e);
                    return false;
                }
            }
        }
        public static void BeforeDelete(string id)
        {
            string querystring = @"SELECT  * from Assets where ID=@ID;";

             string NewQuery;

            using(SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand command = new SqlCommand(querystring, con);

                command.Parameters.AddWithValue("@ID", id);

                try
                {
                    con.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    DataTable dt = new DataTable();

                    dt.Load(reader);

                    string outIn = dt.Rows[0].Field<string>("IncomeAndExpenditure");

                    int money = dt.Rows[0].Field<int>("Money");

                    if(outIn == "收入")
                    {
                        NewQuery = "update Assets set total = total - @money where ID > @ID and not [IsDelete] = 'true';";

                        SqlCommand Newcommand = new SqlCommand(NewQuery, con);

                        Newcommand.Parameters.AddWithValue("@ID", id);

                        Newcommand.Parameters.AddWithValue("@money", money);

                        Newcommand.ExecuteNonQuery();
                    }
                    else
                    {
                        NewQuery = "update Assets set total = total + @money where ID > @ID and not [IsDelete] = 'true';";

                        SqlCommand Newcommand = new SqlCommand(NewQuery, con);

                        Newcommand.Parameters.AddWithValue("@ID", id);

                        Newcommand.Parameters.AddWithValue("@money", money);

                        Newcommand.ExecuteNonQuery();
                    }
                    command.ExecuteNonQuery();
                }catch(Exception e)
                {
                    HttpContext.Current.Response.Write(e);
                }
            }
        }
        public static bool BalanceCheck(int money)
        {
            string querystring = @"select top(1) total from Assets order by ID desc;";

            using(SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand command = new SqlCommand(querystring, con);

                try
                {
                    con.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    DataTable dt = new DataTable();

                    dt.Load(reader);

                    int total = dt.Rows[0].Field<int>("total");

                    if (total < money)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }catch(Exception e)
                {
                    //HttpContext.Current.Response.Write(e);
                    return true;
                }
            }
        }
        public static bool NewBalance(int total, int newTotal, string id)
        {
            string querystring = @"update [Assets] set total = total + @newMoney where ID > @id;"; //

            int newMoney = newTotal - total;

            using(SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand command = new SqlCommand(querystring, con);

                command.Parameters.AddWithValue("@newMoney", newMoney);
                command.Parameters.AddWithValue("@id", id);

                try
                {
                    con.Open();

                    string checkBalance = @"select total from [Assets] where total <0;";

                    SqlCommand checkCommand = new SqlCommand(checkBalance, con);

                    SqlDataReader reader = checkCommand.ExecuteReader();

                    DataTable dt = new DataTable();

                    dt.Load(reader);

                    if(dt.Rows.Count > 0) {
                        return false;
                    }
                    else
                    {
                        command.ExecuteNonQuery();
                        return true;
                    }

                    
                }catch(Exception e)
                {
                    HttpContext.Current.Response.Write(e);
                    return false;
                }
            }
        }
        public static DataTable ShowDataTable(string DBName)
        {
            string querystring = "select * from " + DBName + ";";

            using(SqlConnection con = new SqlConnection(connectionstring))
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
                }catch(Exception e)
                {
                    HttpContext.Current.Response.Write(e);

                    return null;
                }
            }
        }
        public static bool AddSubList(string sub)
        {
            string querystring = @"insert into SubList (Sublist) values (@sub);";

            string check = @"select SubList from SubList where SubList = @sub";

            using(SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand command = new SqlCommand(querystring, con);
                SqlCommand commandCheck = new SqlCommand(check, con);
                command.Parameters.AddWithValue("@sub", sub);
                commandCheck.Parameters.AddWithValue("@sub", sub);
                try
                {
                    con.Open();

                    SqlDataReader reader = commandCheck.ExecuteReader();

                    DataTable dt = new DataTable();

                    dt.Load(reader);

                    reader.Close();

                    if (dt.Rows.Count > 0)
                    {
                        return false;
                    }
                    else
                    {
                        command.ExecuteNonQuery();
                        return true;
                    }

                    
                }catch(Exception e)
                {
                    HttpContext.Current.Response.Write(e);

                    return false;
                }
            }
        }
        
        
    }
}