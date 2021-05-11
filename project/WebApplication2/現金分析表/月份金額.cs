using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.現金分析表
{
    public class 月份金額
    {
        public class monthMoney
        {
            public int? monthInOut { get; set; }
        }
        public class monthBalance
        {
            public int? balance { get; set; }
        }
        public class subMoneyIn
        {
            public string subType { get; set; }
            public int? Jan { get; set; }
            public int? Feb { get; set; }
            public int? Mar { get; set; }
            public int? Apr { get; set; }
            public int? May { get; set; }
            public int? Jun { get; set; }
            public int? Jul { get; set; }
            public int? Aug { get; set; }
            public int? Sep { get; set; }
            public int? Oct { get; set; }
            public int? Nov { get; set; }
            public int? Dec { get; set; }
        }


    }
}