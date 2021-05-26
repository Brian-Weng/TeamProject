using System;
using System.Data;
using WebApplication2.Managers;
using WebApplication2.Models;
using WebApplication2.Helpers;

namespace WebApplication2
{
    public partial class ReceiptDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //如果第一次載入這個Page
            if (!IsPostBack)
            {
                //讀取DB的Company表來設定下拉選單內容
                DDLManager ddlManager = new DDLManager();
                DataTable ddlDataTable = ddlManager.GetCompanyDDL();
                this.dplCompany.DataSource = ddlDataTable;
                this.dplCompany.DataValueField = "Cid";
                this.dplCompany.DataTextField = "Name";
                this.dplCompany.DataBind();

                //宣告變數存取QueryString
                string RepNumber = Request.QueryString["RepNo"];

                //分為更新模式及新增模式
                if (ReceiptDetailHelper.isUpdateMode())
                {
                    this.h1Title.InnerText = "修改發票";

                    //讀取DB內的資料顯示在畫面
                    this.LoadReceipt(RepNumber);

                    //鎖定發票號碼
                    this.txtReceiptNumber.Enabled = false;
                    this.txtReceiptNumber.BackColor = System.Drawing.Color.LightGray;
                }
                else if (!string.IsNullOrEmpty(RepNumber))
                {
                    //Uri的QueryString被更改時、直接跳回發票總覽頁面
                    var manager = new ReceiptManager();
                    if (manager.GetReceipt(RepNumber) == null)
                    {
                        Response.Redirect("~/ReceiptList.aspx");
                    }
                }
                else
                {
                    this.h1Title.InnerText = "新增發票";
                }
            }

        }

        #region LoadReceipt
        //用發票號碼去DB讀取對應的發票資料
        private void LoadReceipt(string ReceiptNumber)
        {   
            //讀取資料並放入資料model
            var manager = new ReceiptManager();
            var model = manager.GetReceipt(ReceiptNumber);

            //如果讀取不到資料，回到發票總覽頁面
            if (model == null)
                Response.Redirect("~/ReceiptList.aspx");

            //讀取到的資料放入畫面中各個使用者輸入項目裡
            this.txtReceiptNumber.Text = model.ReceiptNumber;
            this.lbDate.Text = string.Format("{0:yyyy-MM-dd}", model.Date);
            this.dplCompany.SelectedValue = model.Company;
            this.txtAmount.Text = model.Amount.ToString();
            this.dplRE.SelectedValue = ((int)model.Revenue_Expense).ToString();
        }

        #endregion

        #region SetInputDate
        //日期輸入設定
        protected void cldrDate_SelectionChanged(object sender, EventArgs e)
        {
            //日曆上點選日期會在日期標籤顯示，標籤文字顏色設定為黑色
            lbDate.ForeColor = System.Drawing.Color.Black;
            lbDate.Text = string.Format("{0:yyyy-MM-dd}", cldrDate.SelectedDate);
        }

        #endregion

        #region checkReceiptNumber
        //檢查發票號碼
        protected void txtReceiptNumber_TextChanged(object sender, EventArgs e)
        {
            //檢查發票號碼輸入值，依照輸入內容在畫面顯示提示訊息
            string inputRepNumber = this.txtReceiptNumber.Text.Trim();
            this.lbReceiptNumber.Text = ReceiptDetailHelper.checkReceiptNumber(inputRepNumber);
        }
        #endregion

        #region checkAmount
        protected void txtAmount_TextChanged(object sender, EventArgs e)
        {
            //檢查金額輸入值，依照輸入內容在畫面顯示提示訊息
            string inputAmount = this.txtAmount.Text;
            this.lbAmount.Text = ReceiptDetailHelper.checkAmount(inputAmount);
        }
        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //建立資料model及ReceiptManager的物件
            var manager = new ReceiptManager();
            var model = new ReceiptModel();

            //建立5個變數來存取各個輸入值
            string inputRecNo = this.txtReceiptNumber.Text.Trim();
            string inputDate = this.lbDate.Text.Trim();
            string dplCompany = this.dplCompany.SelectedValue;
            string inputAmount = this.txtAmount.Text.Trim();
            string dplRE = this.dplRE.SelectedValue;

            //檢查輸入值(發票號碼、日期、金額)
            if (string.IsNullOrEmpty(inputRecNo) || string.IsNullOrEmpty(inputAmount) || string.Equals(inputDate, "請選擇日期"))
            {
                //日期沒輸入會變成紅色
                if (string.Equals(inputDate, "請選擇日期"))
                    this.lbDate.ForeColor = System.Drawing.Color.Red;

                this.lblMsg.Text = "請填入完整的發票資料";
                return;
            }
            else if (ReceiptDetailHelper.checkReceiptNumber(inputRecNo) != string.Empty)
            {
                this.lblMsg.Text = "請填入正確的發票格式";
                return;
            }
            else if (ReceiptDetailHelper.checkAmount(inputAmount) != string.Empty)
            {
                this.lblMsg.Text = "金額只能填入數字";
                return;
            }

            //上面檢查都通過後，將輸入值存入資料model
            model.ReceiptNumber = inputRecNo;
            model.Date = DateTime.Parse(inputDate);
            model.Company = dplCompany;
            model.Amount = decimal.Parse(inputAmount);
            model.Revenue_Expense = (Revenue_Expense)Enum.Parse(typeof(Revenue_Expense), dplRE);

            //分成更新模式及新增模式
            //更新模式下將資料model更新至資料庫
            //新增模式下將資料model存入資料庫
            if (ReceiptDetailHelper.isUpdateMode())
            {
                manager.UpdateReceipt(model);
                this.lblMsg.Text = "發票更新成功";
            }
            else
            {
                manager.CreateReceipt(model);
                this.lblMsg.Text = "發票新增成功";
            }

        }
        
    }
}