using System;

namespace WebApplication2.Models
{
    public class ReceiptModel
    {
        public string ReceiptNumber { get; set; }
        public DateTime Date { get; set; }
        public string Company { get; set; }
        public Decimal Amount { get; set; }
        public Revenue_Expense Revenue_Expense { get; set; }
    }
  
}