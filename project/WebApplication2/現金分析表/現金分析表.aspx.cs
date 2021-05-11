using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2.現金分析表
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            int year = Convert.ToInt32(this.yearSelect.SelectedValue);
            this.monthIncome.DataSource = DB現金分析表.monthTotalOutIn(year,"收入");//列出每月收入
            this.monthIncome.DataBind();

            this.monthExpenditure.DataSource = DB現金分析表.monthTotalOutIn(year, "支出");//列出每月支出
            this.monthExpenditure.DataBind();

            this.monthBalance.DataSource = DB現金分析表.monthBalance(year);//每月餘額
            this.monthBalance.DataBind();

            this.title.Text = this.yearSelect.SelectedValue;            //隨年份更改標題

            this.MonthSubIncome.DataSource = DB現金分析表.subMoneyIn(year,"收入");
            this.DataBind();

            this.MonthSubEx.DataSource = DB現金分析表.subMoneyIn(year, "支出");
            this.DataBind();

            //this.Repeater1.DataSource = DB現金分析表.showSub();
            //this.Repeater1.DataBind();
        }

        protected void yearSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            int year = Convert.ToInt32(this.yearSelect.SelectedValue);
            
        }

       
    }
}