using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebApplication2
{
    public partial class 現金日記帳 : System.Web.UI.Page
    {
        public static string cmdArgu;
        //public string modalID { get; set; }
        protected void Page_init(object sender, EventArgs e)
        {
            
            //this.showMoneyList.DataSource = DB.showTotalDB();
            //this.showMoneyList.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            

        }

        protected void Button3_Click(object sender, EventArgs e)
        {

        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            
            //this.showMoneyList.DataSource = DB.showTotalDB();
            //this.showMoneyList.DataBind();
           
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
        //    string cmdName = e.CommandName; 

        //    cmdArgu = e.CommandArgument.ToString();

        //    if ("deleteItem" == cmdName)
        //    {
        //        DB.BeforeDelete(cmdArgu);

        //        DB.deleteDB(cmdArgu);
        //    }
        //    if ("EditItem" == cmdName)
        //    {
        //        string targetUrl =
        //            "~/修改項目.aspx?ID=" + cmdArgu;

        //        Response.Redirect(targetUrl);
        //    }
        }
        
        

    }
}