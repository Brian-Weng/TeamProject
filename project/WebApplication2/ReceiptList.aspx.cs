using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication2.Helpers;
using WebApplication2.Managers;
using WebApplication2.Models;

namespace WebApplication2
{
    public partial class Invoice : System.Web.UI.Page
    {
        const int _pageSize = 10;
        internal class PagingLink
        {
            public string Name { get; set; }
            public string Link { get; set; }
            public string Title { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.LoadRepeater();
                this.RestoreParameters();
            }
        }

        protected void repInvoice_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string cmdName = e.CommandName;
            string arg = e.CommandArgument.ToString();
            var manager = new ReceiptManager();

            if(cmdName == "DeleteItem")
            {
                manager.DeleteReceipt(arg);
                this.LoadRepeater();
            }

            if(cmdName == "UpdateItem")
            {
                string targetUrl = "~/ReceiptDetail.aspx?ID=" + arg;
                Response.Redirect(targetUrl);
            }
        }

        private void RestoreParameters()
        {
            string company = Request.QueryString["company"];
            string minPriceText = Request.QueryString["minPrice"];
            string maxPriceText = Request.QueryString["maxPrice"];
            string R_E = Request.QueryString["R_E"];

            if (!string.IsNullOrEmpty(company))
                this.ddlCompany.SelectedValue = company;

            if (!string.IsNullOrEmpty(minPriceText))
                this.txtMinAmount.Text = minPriceText;

            if (!string.IsNullOrEmpty(maxPriceText))
                this.txtMaxAmount.Text = maxPriceText;

            if (!string.IsNullOrEmpty(R_E))
                this.ddlR_E.SelectedValue = R_E;
        }

        private string GetQueryString(int? pageIndex)
        {
            //string page = Request.QueryString["Page"];
            string company = Request.QueryString["company"];
            string minPriceText = Request.QueryString["minPrice"];
            string maxPriceText = Request.QueryString["maxPrice"];
            string R_E = Request.QueryString["R_E"];

            List<string> conditions = new List<string>();

            //if (!string.IsNullOrEmpty(page))
            //    conditions.Add("Page=" + page);

            if (!string.IsNullOrEmpty(company))
                conditions.Add("company=" + company);

            if(!string.IsNullOrEmpty(minPriceText))
                conditions.Add("minPrice=" + minPriceText);

            if(!string.IsNullOrEmpty(maxPriceText))
                conditions.Add("maxPrice=" + maxPriceText);

            if (!string.IsNullOrEmpty(R_E))
                conditions.Add("R_E=" + R_E);

            if (pageIndex.HasValue)
                conditions.Add("Page=" + pageIndex.Value);

            string retText =
                (conditions.Count > 0)
                    ? "?" + string.Join("&", conditions)
                    : string.Empty;

            return retText;

        }

        private void LoadRepeater()
        {
            string page = Request.QueryString["Page"];
            int pIndex = 0;
            if(string.IsNullOrEmpty(page))
            {
                pIndex = 1;
            }
            else
            {
                int.TryParse(page, out pIndex);

                if (pIndex <= 0)
                    pIndex = 1;
            }

            string company = Request.QueryString["company"];
            string minPriceText = Request.QueryString["minPrice"];
            string maxPriceText = Request.QueryString["maxPrice"];
            string R_EText = Request.QueryString["R_E"];

            decimal? minPrice = null;
            decimal? maxPrice = null;
            if(!string.IsNullOrEmpty(minPriceText))
            {
                int temp;
                if (int.TryParse(minPriceText, out temp))
                    minPrice = temp;
            }

            if(!string.IsNullOrEmpty(maxPriceText))
            {
                int temp;
                if (int.TryParse(maxPriceText, out temp))
                    maxPrice = temp;
            }

            int? R_E = null;
            if(!string.IsNullOrEmpty(R_EText))
            {
                int temp;
                if (int.TryParse(R_EText, out temp))
                    R_E = temp;
            }


            int totalSize = 0;

            var manager = new ReceiptManager();
            var list = manager.GetReceipts(company, minPrice, maxPrice, R_E, out totalSize, pIndex, _pageSize);


            int pages = PagingHelper.CalculatePages(totalSize, _pageSize);

            List<PagingLink> pagingList = new List<PagingLink>();
            for (var i = 1; i <= pages; i++)
            {
                pagingList.Add(new PagingLink()
                {
                    Link = $"ReceiptList.aspx{this.GetQueryString(i)}",
                    Name = $"{i}",
                    Title = $"前往第 {i} 頁"
                });
            }
            this.firstPage.HRef = $"ReceiptList.aspx{this.GetQueryString(1)}";
            this.lastPage.HRef = $"ReceiptList.aspx{this.GetQueryString(pages)}";

            this.repPaging.DataSource = pagingList;
            this.repPaging.DataBind();

            this.repInvoice.DataSource = list;
            this.repInvoice.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string company = this.ddlCompany.Text;
            string minPrice = this.txtMinAmount.Text;
            string maxPrice = this.txtMaxAmount.Text;
            string R_E = this.ddlR_E.Text;

            string template = "?Page=1";

            if (!string.IsNullOrEmpty(company))
                template += "&company=" + company;

            if (!string.IsNullOrEmpty(minPrice))
                template += "&minPrice=" + minPrice;

            if (!string.IsNullOrEmpty(maxPrice))
                template += "&maxPrice=" + maxPrice;

            if (!string.IsNullOrEmpty(R_E))
                template += "&productType=" + R_E;

            Response.Redirect("ReceiptList.aspx" + template);
        }
    }
}