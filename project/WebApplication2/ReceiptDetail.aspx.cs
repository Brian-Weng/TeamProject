using System;
using WebApplication2.Managers;
using WebApplication2.Models;
using WebApplication2.Helpers;
using System.Data;

namespace WebApplication2
{
    public partial class ReceiptDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //依照資料庫的Company資料表設定下拉選單預設值
                //宣告DataTable來存取Company資料並給下拉選單繫結
                DDLManager ddlManager = new DDLManager();
                DataTable ddlDataTable = ddlManager.GetCompanyDDL();
                this.dplCompany.DataSource = ddlDataTable;
                this.dplCompany.DataValueField = "Cid";
                this.dplCompany.DataTextField = "Name";
                this.dplCompany.DataBind();

                //分為更新模式及新增模式，標題會依照當前模式動態更新。
                //更新模式下會讀取當前發票號碼的資料，並鎖定發票號碼不讓使用者修改
                if (ReceiptDetailHelper.isUpdateMode())
                {
                    this.h1Title.InnerText = "修改發票";
                    //宣告變數RepNumber來存取QueryString["RepNo"]
                    string RepNumber = Request.QueryString["RepNo"];
                    this.LoadReceipt(RepNumber);

                    this.txtReceiptNumber.Enabled = false;
                    this.txtReceiptNumber.BackColor = System.Drawing.Color.LightGray;
                }
                else
                {
                    this.h1Title.InnerText = "新增發票";
                }
            }

        }

        #region LoadReceipt
        //將發票號碼作為參數去資料庫讀取相對應的發票資料
        private void LoadReceipt(string ReceiptNumber)
        {   
            //讀取指定發票號碼的資料並放入資料模型model
            var manager = new ReceiptManager();
            var model = manager.GetReceipt(ReceiptNumber);

            //讀取不到資料的話，回到發票總覽頁面
            if (model == null)
                Response.Redirect("~/ReceiptList.aspx");

            //將讀取到的資料放入畫面中各個使用者輸入項目裡
            this.txtReceiptNumber.Text = model.ReceiptNumber;
            this.lbDate.Text = string.Format("{0:yyyy-MM-dd}", model.Date);
            this.dplCompany.SelectedValue = model.Company;
            this.txtAmount.Text = model.Amount.ToString();
            this.dplRE.SelectedValue = ((int)model.Revenue_Expense).ToString();
        }

        #endregion

        #region SetInputDate
        protected void cldrDate_SelectionChanged(object sender, EventArgs e)
        {
            //將使用者在日曆上點選的日期存入日期標籤，標籤文字顏色設定為黑色
            lbDate.ForeColor = System.Drawing.Color.Black;
            lbDate.Text = string.Format("{0:yyyy-MM-dd}", cldrDate.SelectedDate);
        }

        #endregion

        #region checkReceiptNumber
        protected void txtReceiptNumber_TextChanged(object sender, EventArgs e)
        {
            //宣告變數存取使用者輸入的發票號碼
            string inputRepNumber = this.txtReceiptNumber.Text.Trim();
            //檢查使用者的輸入值，依照輸入內容在畫面顯示提示訊息
            this.lbReceiptNumber.Text = ReceiptDetailHelper.checkReceiptNumber(inputRepNumber);
        }
        #endregion

        #region checkAmount
        protected void txtAmount_TextChanged(object sender, EventArgs e)
        {
            string inputAmount = this.txtAmount.Text;

            this.lbAmount.Text = ReceiptDetailHelper.checkAmount(inputAmount);
        }
        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //建立ReceiptManager的物件實體
            //建立資料模型model變數設定為null
            var manager = new ReceiptManager();
            ReceiptModel model = null;

            //宣告5個變數來存取使用者的輸入值
            string inputRecNo = this.txtReceiptNumber.Text.Trim();
            string inputDate = this.lbDate.Text.Trim();
            string dplCompany = this.dplCompany.SelectedValue;
            string inputAmount = this.txtAmount.Text.Trim();
            string dplRE = this.dplRE.SelectedValue;

            //分成更新模式及新增模式
            //更新模式下將當前QueryString["RepNo"]作為參數讀取資料並放入資料模型model
            //新增模式下model變數設定成新的物件實體
            if (ReceiptDetailHelper.isUpdateMode())
            {
                string RepNumber = Request.QueryString["RepNo"];
                //model = manager.GetReceipt(RepNumber);
                model = new ReceiptModel();
            }
            else
            {
                model = new ReceiptModel();
            }

            //分成更新模式及新增模式
            //更新模式下鎖定了發票號碼的輸入值，所以只檢查金額的輸入值
            //新增模式下檢查發票號碼的輸入值、是否有點選日期及金額的輸入值
            //依照使用者的輸入給予提示訊息
            if (ReceiptDetailHelper.isUpdateMode())
            {   
                if(string.IsNullOrEmpty(inputAmount))
                {
                    this.lblMsg.Text = "請填入發票金額";
                    return;
                }
                else if (ReceiptDetailHelper.checkAmount(inputAmount) != string.Empty)
                {
                    this.lblMsg.Text = "金額只能填入數字";
                    return;
                }
            }
            else
            {   
                if (string.IsNullOrEmpty(inputRecNo) || string.Equals(inputDate, "請選擇日期") || string.IsNullOrEmpty(inputAmount))
                {
                    if(string.Equals(inputDate, "請選擇日期"))
                        this.lbDate.ForeColor = System.Drawing.Color.Red;

                    this.lblMsg.Text = "請填入完整的發票資料";
                    return;
                }
                else if (ReceiptDetailHelper.checkReceiptNumber(inputRecNo) != string.Empty)
                {
                    this.lblMsg.Text = "請填入正確的發票格式";
                    return;
                }
                else if(ReceiptDetailHelper.checkAmount(inputAmount) != string.Empty)
                {
                    this.lblMsg.Text = "金額只能填入數字";
                    return;
                }
            }

            //上面驗證都通過後，將資料模型model設定成使用者輸入值
            model.ReceiptNumber = inputRecNo;
            model.Date = DateTime.Parse(inputDate);
            model.Company = dplCompany;
            model.Amount = decimal.Parse(inputAmount);
            model.Revenue_Expense = (Revenue_Expense)Enum.Parse(typeof(Revenue_Expense), dplRE);

            //分成更新模式及新增模式
            //更新模式下將資料model更新至資料庫
            //新增模式下將新資料model存入資料庫
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