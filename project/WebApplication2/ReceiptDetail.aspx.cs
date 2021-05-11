﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication2.Managers;
using WebApplication2.Models;
using System.Text.RegularExpressions;
using WebApplication2.Helpers;

namespace WebApplication2
{
    public partial class InvoiceDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            { 
                if(this.isUpdateMode())
                {
                    this.lbltitle.InnerText = "修改發票";
                    string qsid = Request.QueryString["ID"];

                    this.txtReceiptNumber.Enabled = false;
                    this.txtReceiptNumber.BackColor = System.Drawing.Color.LightGray;
                    this.LoadReceipt(qsid);
                }
                else
                {
                    this.lbltitle.InnerText = "新增發票";
                }
            }
        }
        protected void cldrDate_SelectionChanged(object sender, EventArgs e)
        {
            //將使用者點選的日期存入日期標籤
            lbDate.Text = string.Format("{0:yyyy-MM-dd}", cldrDate.SelectedDate);
        }
        #region checkReceiptNumber
        protected void txtReceiptNumber_TextChanged(object sender, EventArgs e)
        {
            string input = this.txtReceiptNumber.Text;

            this.lbReceiptNumber.Text = ReceiptNumberHelper.checkReceiptNumber(input);
        }
        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var manager = new ReceiptManager();
            ReceiptModel model = null;

            string inputRecNo = this.txtReceiptNumber.Text.Trim();
            string inputDate = this.lbDate.Text;
            string dpdCompany = this.dpdCompany.SelectedItem.Text;
            string inputAmount = this.txtAmount.Text.Trim();
            string dpdValue = this.dpdRE.SelectedValue.ToString();
            //判斷使用者輸入的值,有值且格式正確,才可以存入資料庫。
            //空值或格式不正確,中斷方法
            if(this.isUpdateMode())
            {
                string qsID = Request.QueryString["ID"];

                model = manager.GetReceipt(qsID);
            }
            else
            {
                model = new ReceiptModel();
            }

            if(this.isUpdateMode())
            {   //編輯模式只檢查金額不可為空(因發票號碼鎖定,其他輸入控制項會帶入當前項目)
                if(inputAmount == null)
                {
                    this.lblMsg.Text = "請填入發票金額";
                    return;
                }
            }
            else
            {   //新增模式驗證發票號碼,日期輸入及金額輸入不可為空值
                if (inputRecNo == null || string.Equals(inputDate, "請選擇日期") || inputAmount == null)
                {
                    this.lblMsg.Text = "請填入完整的發票資料";
                    return;
                }
                else if (ReceiptNumberHelper.checkReceiptNumber(inputRecNo) != string.Empty)
                {
                    this.lblMsg.Text = "請填入正確的發票資料";
                    return;
                }
            }


            model.ReceiptNumber = this.txtReceiptNumber.Text.Trim();
            model.Date = DateTime.Parse(this.lbDate.Text);
            model.Company = this.dpdCompany.SelectedItem.Text;
            model.Amount = decimal.Parse(this.txtAmount.Text.Trim());
            //將下拉選單的值轉成字串,再轉成相對應的Enum值
            model.Revenue_Expense = (Revenue_Expense)Enum.Parse(typeof(Revenue_Expense), dpdValue);

            //寫入DB
            if (this.isUpdateMode())
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
        
        private bool isUpdateMode()
        {   
            string qsID = Request.QueryString["ID"];
            if (string.IsNullOrEmpty(qsID))
            {
                return false;
            }
            if (Regex.IsMatch(qsID, @"^[A-Z]{2}[-]{1}[0-9]{8}$"))
            {
                return true;
            }

            return false;
        }

        private void LoadReceipt(string ReceiptNumber)
        {
            var manager = new ReceiptManager();
            var model = manager.GetReceipt(ReceiptNumber);//out Guid temp

            if (model == null)
                Response.Redirect("~/ReceiptList.aspx");
            
            this.txtReceiptNumber.Text = model.ReceiptNumber;
            //將日期轉成yyyy-MM-dd格式
            this.lbDate.Text = string.Format("{0:yyyy-MM-dd}", model.Date);

            this.dpdCompany.SelectedValue = model.Company.Trim();
            this.txtAmount.Text = model.Amount.ToString();

            int R_E = (int)model.Revenue_Expense;
            this.dpdRE.SelectedValue = R_E.ToString();
        }
    }
}