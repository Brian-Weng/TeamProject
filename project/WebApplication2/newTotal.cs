using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2
{
    public class newTotal
    {
        public static int UpdateTotal(string outIn, string newOutIn, int money,int newMoney, int total)
        {
            if(outIn == "收入") //如果修改前的行為為收入，餘額減掉收入，讓他回到前一筆的金額
            {
                total -= money;
            }
            else
            {
                total += money; //支出
            }

            if(newOutIn == "收入") //更新行為以及餘額
            {
                total += newMoney;
            }
            else
            {
                total -= newMoney;
            }

            return total;
        }
    }
}