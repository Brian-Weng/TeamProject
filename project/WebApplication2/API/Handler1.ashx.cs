using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.API
{
    /// <summary>
    /// Handler1 的摘要描述
    /// </summary>
    public class Handler1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            var tempList = DB.ShowDataTable("receipt"); /*(from item in this._list where item.UserName.Contains(pName) select item).ToList();*/

            string result = Newtonsoft.Json.JsonConvert.SerializeObject(tempList);

            context.Response.ContentType = "application/json";
            context.Response.Charset = "utf-8";
            context.Response.Write(result);

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}