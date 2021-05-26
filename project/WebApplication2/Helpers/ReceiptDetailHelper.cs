using System.Web;
using System.Text.RegularExpressions;
using WebApplication2.Managers;

namespace WebApplication2.Helpers
{
    public class ReceiptDetailHelper
    {
        //檢查發票號碼
        public static string checkReceiptNumber(string receiptNumber)
        {
            
            string label;
            var manager = new ReceiptManager();
            
            if (receiptNumber == null || receiptNumber.Length == 0)
                label = "發票編號不能為空";
            else if (receiptNumber.Length != 11 || !Regex.IsMatch(receiptNumber, @"^[A-Z]{2}[-]{1}[0-9]{8}$"))
                label = "發票格式不正確";
            else if (HttpContext.Current.Request.QueryString["RepNo"] == null && manager.GetReceipt(receiptNumber) != null)
                label = "此發票號碼重複";
            else
                label = string.Empty;
            return label;
        }

        //檢查金額
        public static string checkAmount(string Amount)
        {   
            string label;
            if(string.IsNullOrEmpty(Amount))
                label = "金額不能為空";
            else if(!Regex.IsMatch(Amount, @"^[1-9][0-9]{1,7}$"))
                label = "輸入範圍在 1 ~ 99999999";
            else if (!decimal.TryParse(Amount, out decimal Amt))
                label = "必須輸入數字";
            else
                label = string.Empty;
            return label;
        }
        
        //是否為更新模式
        public static bool isUpdateMode()
        {   
            string RepNumber = HttpContext.Current.Request.QueryString["RepNo"];
            if (string.IsNullOrEmpty(RepNumber))
                return false;
            else if (Regex.IsMatch(RepNumber, @"^[A-Z]{2}[-]{1}[0-9]{8}$"))
                return true;
            else
                return false;     
        }
    }
}