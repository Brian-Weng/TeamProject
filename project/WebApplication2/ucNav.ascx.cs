using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class ucNav : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginHelper.HasLogined())
            {
                this.LoginPlc.Visible = true;
            }
            else
            {
                this.LogoutPlc.Visible = true;
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            LoginHelper.Logout();
            Response.Redirect("~/Login.aspx");
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }
    }
}