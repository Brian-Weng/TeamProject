using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

using WebApplication2.Managers;

namespace WebApplication2.Helpers
{
    public class ReceiptDetailHelper
    {
        public static string checkReceiptNumber(string receiptNumber)
        {
            string label;
            var manager = new ReceiptManager();

            if (receiptNumber == null || receiptNumber.Length == 0)
            {
                label = "發票編號不能為空";
                return label;
            }
            else if (receiptNumber.Length != 11 || !Regex.IsMatch(receiptNumber, @"^[A-Z]{2}[-]{1}[0-9]{8}$"))
            {
                label = "發票格式不正確";
                return label;
            }
            else if (manager.GetReceipt(receiptNumber) != null)
            {
                label = "此發票號碼重複";
                return label;
            }
            else
            {
                label = string.Empty;
                return label;
            }
        }

        public static bool isUpdateMode()
        {   
            string qsID = HttpContext.Current.Request.QueryString["ID"];
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

    }
}