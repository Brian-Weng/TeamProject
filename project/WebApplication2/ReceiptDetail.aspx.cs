using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication2.Managers;
using WebApplication2.Models;
using System.Text.RegularExpressions;
using WebApplication2.Helpers;
using System.Data;

namespace WebApplication2
{
    public partial class ReceiptDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //假設不是PostBack
            //if (!IsPostBack)
            //{
                DataTable dt = DDLManager.GetCompanyDDL();
                this.dplCompany.DataSource = dt;
                this.dplCompany.DataTextField = "Name";
                this.dplCompany.DataValueField = "Cid";
                this.dplCompany.DataBind();
                //分為更新模式及新增模式，標題會依照當前模式動態更新。
                //更新模式下會讀取當前發票號碼的資料，並鎖定發票號碼不讓使用者修改
                if (ReceiptDetailHelper.isUpdateMode())
                {   
                    this.h1title.InnerText = "修改發票";
                    string RepNumber = Request.QueryString["RepNo"];

                    this.LoadReceipt(RepNumber);

                    this.txtReceiptNumber.Enabled = false;
                    this.txtReceiptNumber.BackColor = System.Drawing.Color.LightGray;
                }
                else
                {
                    this.h1title.InnerText = "新增發票";
                }
            //}
        }

        #region LoadReceipt
        //將發票號碼作為參數去資料庫讀取相對應的發票資料
        private void LoadReceipt(string ReceiptNumber)
        {   
            //讀取指定發票號碼的資料並放入資料模型model裡
            var manager = new ReceiptManager();
            var model = manager.GetReceipt(ReceiptNumber);

            //讀取不到資料的話，回到發票總覽頁面
            if (model == null)
                Response.Redirect("~/ReceiptList.aspx");

            //將讀取到的資料放入畫面中各個使用者輸入項目裡
            this.txtReceiptNumber.Text = model.ReceiptNumber;
            this.lbDate.Text = string.Format("{0:yyyy-MM-dd}", model.Date);
            
            this.dplCompany.SelectedValue = model.Company.Trim();
            this.txtAmount.Text = model.Amount.ToString();
            
            int R_E = (int)model.Revenue_Expense;
            this.dplRE.SelectedValue = R_E.ToString();
        }

        #endregion

        #region SetInputDate
        protected void cldrDate_SelectionChanged(object sender, EventArgs e)
        {
            //將使用者點選的日期存入日期標籤
            lbDate.ForeColor = System.Drawing.Color.Black;
            lbDate.Text = string.Format("{0:yyyy-MM-dd}", cldrDate.SelectedDate);
        }

        #endregion

        #region checkReceiptNumber
        protected void txtReceiptNumber_TextChanged(object sender, EventArgs e)
        {
            string inputRepNumber = this.txtReceiptNumber.Text;

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
            var manager = new ReceiptManager();
            ReceiptModel model = null;

            string inputRecNo = this.txtReceiptNumber.Text.Trim();
            string inputDate = this.lbDate.Text;
            string dplCompany = this.dplCompany.SelectedValue;
            string inputAmount = this.txtAmount.Text.Trim();
            string dplRE = this.dplRE.SelectedValue;
            //判斷使用者輸入的值,有值且格式正確,才可以存入資料庫。
            //空值或格式不正確,中斷方法
            if(ReceiptDetailHelper.isUpdateMode())
            {
                string RepNumber = Request.QueryString["RepNo"];

                model = manager.GetReceipt(RepNumber);
            }
            else
            {
                model = new ReceiptModel();
            }

            if(ReceiptDetailHelper.isUpdateMode())
            {   //編輯模式只檢查金額不可為空(因發票號碼鎖定,其他輸入控制項會帶入當前項目)
                if(string.IsNullOrEmpty(inputAmount))
                {
                    this.lblMsg.Text = "請填入發票金額";
                    return;
                }
            }
            else
            {   //新增模式驗證發票號碼,日期輸入及金額輸入不可為空值
                if (inputRecNo == null || string.Equals(inputDate, "請選擇日期") || inputAmount == null)
                {
                    this.lbDate.ForeColor = System.Drawing.Color.Red;
                    this.lblMsg.Text = "請填入完整的發票資料";
                    return;
                }
                else if (ReceiptDetailHelper.checkReceiptNumber(inputRecNo) != string.Empty)
                {
                    this.lblMsg.Text = "請填入正確的發票資料";
                    return;
                }
            }









            model.ReceiptNumber = inputRecNo;
            model.Date = DateTime.Parse(inputDate);
            model.Company = dplCompany;
            model.Amount = decimal.Parse(inputAmount);
            //將下拉選單的字串值,轉型成Enum
            model.Revenue_Expense = (Revenue_Expense)Enum.Parse(typeof(Revenue_Expense), dplRE);

            //寫入DB
            if (ReceiptDetailHelper.isUpdateMode())
            {
                manager.UpdateReceipt(model);
                this.lblMsg.Text = "發票更新成功";
            }
            else if(!ReceiptDetailHelper.isUpdateMode())
            {
                manager.CreateReceipt(model);
                this.lblMsg.Text = "發票新增成功";
            }
            else
            {
                this.lblMsg.Text = "存檔失敗，請聯繫開發人員";
            }

        }

        
    }
}