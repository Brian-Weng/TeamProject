using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginHelper.HasLogined()) //判斷是否已經登入
            {
                string targetUrl =
                    "~/homepage.aspx?User=" + LoginHelper.GetUserName();

                Response.Redirect(targetUrl);  //如果已登入跳轉至首頁
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            string account = this.TextBox1.Text;

            string passowrd = this.TextBox2.Text;

            bool logined = LoginHelper.tryLogin(account, passowrd); 

            if (logined) //如果登入成功跳轉至首頁
            {
                string targetUrl =
                    "~/homepage.aspx?User=" + LoginHelper.GetUserName();

                Response.Redirect(targetUrl);
            }
            else //失敗跳出錯誤訊息
            {
                this.Label1.Visible = true;
            }
        }
    }
}