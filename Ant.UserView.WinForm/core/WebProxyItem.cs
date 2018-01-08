using System;
using System.Net;

namespace Ant.UserView.WinForm.core
{
    public class WebProxyItem
    {
        public string Url { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public WebProxy ToWebProxy()
        {
            WebProxy proxy = new WebProxy();                                      //定義一個網關對象
            proxy.Address = new Uri(this.Url);              //網關服務器:端口

            if (!string.IsNullOrEmpty(UserName))
            {
                proxy.Credentials = new NetworkCredential(this.UserName, this.Password);      //用戶名,密碼
                proxy.UseDefaultCredentials = true;
            }


            return proxy;
        }
    }
}