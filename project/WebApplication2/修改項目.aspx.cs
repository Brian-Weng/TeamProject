using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_init(object sender, EventArgs e)
        {
            string GetId = this.Request.QueryString["ID"];
            DataTable dt = DB.searchDB(GetId);
            string date = dt.Rows[0].Field<string>("Date");
            string sub = dt.Rows[0].Field<string>("Sub");
            string usefor = dt.Rows[0].Field<string>("Usefor");
            string money = dt.Rows[0].Field<int?>("Money").ToString();
            string OutIn = dt.Rows[0].Field<string>("IncomeAndExpenditure");
            int? total = dt.Rows[0].Field<int?>("total");

            this.date.Text = date;
            this.sub.Text = sub;
            this.usefor.Text = usefor;
            this.money.Text = money;
            this.DropDownList1.SelectedValue = OutIn;


        }
        protected void Page_Load(object sender, EventArgs e)
        {
           

        }

        protected void checkBtn_Click(object sender, EventArgs e)
        {
            if (this.date.Text!="" && this.sub.Text != "" && this.usefor.Text != "" && this.money.Text != ""){ 
            string GetId = this.Request.QueryString["ID"];
            string newdate = this.date.Text;
            string newsub = this.sub.Text;
            string newusefor = this.usefor.Text;
            int newmoney = Convert.ToInt32(this.money.Text);
            string newOutIn = this.DropDownList1.SelectedValue;
            
            DataTable dt = DB.searchDB(GetId);
            string date = dt.Rows[0].Field<string>("Date");
            string sub = dt.Rows[0].Field<string>("Sub");
            string usefor = dt.Rows[0].Field<string>("Usefor");
            int money = dt.Rows[0].Field<int>("Money");
            string OutIn = dt.Rows[0].Field<string>("IncomeAndExpenditure");
            int total = dt.Rows[0].Field<int>("total");

            int newtotal1 = newTotal.UpdateTotal(OutIn, newOutIn, money, newmoney, total);

                if (!DB.UpdateDB(GetId,newdate, newsub, newusefor, newmoney, newOutIn, newtotal1) || !DB.NewBalance(total, newtotal1, GetId))
                {
                    Response.Write("<Script language='JavaScript'>alert('餘額不可為負數');</Script>");
                    return;
                }
               
               Response.Write("<Script language='JavaScript'>alert('修改完成');</Script>");

               Response.Write("<Script language='JavaScript'>window.location.href = '現金日記帳.aspx';</Script>");

            }
            else
            {
                Response.Write("<Script language='JavaScript'>alert('欄位不可空白');</Script>");
            }
          
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            

            Response.Redirect("~/現金日記帳.aspx");
        }
    }
}