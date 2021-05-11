using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace WebApplication2
{
    public partial class UC確認頁面 : System.Web.UI.UserControl
    {

        
        public string Text1 { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            string DBName = "SubList";

            DataTable dt = DB.ShowDataTable(DBName);

            for (int i = 0; i<dt.Rows.Count; i++)
            {
                if (!IsPostBack) { 
                string Sublist = dt.Rows[i].Field<string>("SubList");
                string SubID = dt.Rows[i].Field<int>("ID").ToString();
                    //this.DropDownList2.Items.Add(Sublist);
                    this.DropDownListSub.Items.Add(new ListItem(Sublist,SubID));
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // 如果日期為空白跳出警告
            if (this.TextDate.Text == "")
            {
                this.TextDate.Text = "";  
                
                this.TextSummary.Text = "";
                this.TextMoney.Text = "";
                this.DropDownList1OutIn.SelectedValue = "收入";
                Response.Write("<Script language='JavaScript'>alert('欄位不可空白');</Script>");
                return;
            }
            // 如果欄位為空白跳出警告
            if ( this.DropDownListSub.Text != "" || this.TextSummary.Text != "" || this.TextMoney.Text != "" )
            {
                string date = this.TextDate.Text;
                string sub = this.DropDownListSub.SelectedItem.Text;
                string usefor = this.TextSummary.Text;
                int money = Convert.ToInt32(this.TextMoney.Text);
                string OutIn = this.DropDownList1OutIn.SelectedItem.Value;
                int subID = Convert.ToInt32(this.DropDownListSub.SelectedValue);
                
                this.TextDate.Text="";
                this.TextSummary.Text="";
                this.TextMoney.Text="";
                this.DropDownList1OutIn.SelectedValue = "收入";
                //確認支出是否高於餘額
                if (DB.BalanceCheck(money)&& OutIn == "支出")
                {
                    Response.Write("<Script language='JavaScript'>alert('支出不可高於餘額');</Script>");
                    return;
                }

                DB.insertDB(date, sub, usefor, money, OutIn, subID);
                Response.Write("<Script language='JavaScript'>alert('新增成功');</Script>");
            }
            else
            {
                Response.Write("<Script language='JavaScript'>alert('欄位不可空白');</Script>");
            }
            
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if(this.NewSub.Text != "")
            {
                

                if (DB.AddSubList(this.NewSub.Text))
                {
                    this.DropDownListSub.Items.Add(this.NewSub.Text);
                    Response.Write("<Script language='JavaScript'>alert('新增成功');</Script>");
                }
                else
                {
                    Response.Write("<Script language='JavaScript'>alert('已有相同項目');</Script>");
                }

            }
            else
            {
                Response.Write("<Script language='JavaScript'>alert('欄位不可空白');</Script>");
            }

           
        }

        
    }
}