using Ant.UserView.Core;
using Ant.UserView.Util;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Topshelf.Logging;

namespace Ant.UserView
{
    [DisallowConcurrentExecution]    
    public class RequestJob : IJob
    {
        private LogWriter logger = HostLogger.Get<RequestJob>();

        public void Execute(IJobExecutionContext context)
        {
            //logger.Info("RequestJob start");

            try
            {
                var urlList = GetUrls();

                //SendHelper sendHelper = new SendHelper();

                foreach (var url in urlList)
                {
                    WebBrowserOpen(url);
                    //WebProxyItem item = new WebProxyItem() { Url = "http://191.102.125.74" };
                    //SendResult sendResult = sendHelper.Send(url, item);
                    //if (sendResult.Success == false)
                    //{
                    //    string key = url + "-sendTime";
                    //    string sendTime = JsonConfig.Get(key);
                    //    string timeInterval = JsonConfig.Get("timeInterval");

                    //    //if (string.IsNullOrEmpty(sendTime) || DateTime.Now > DateTime.Parse(sendTime).AddMinutes(int.Parse(timeInterval)))
                    //    //{
                    //    //    string toListStr = JsonConfig.Get("toList");
                    //    //    string[] toList = toListStr.Split(new char[] { ';' });
                    //    //    string subject = JsonConfig.Get("subject");
                    //    //    string content = string.Format("{0} <br/> {1} </br> {2} {3} ", DateTime.Now, url, sendResult.Code, sendResult.Msg);
                    //    //    JsonConfig.Add(key, DateTime.Now.ToString());
                    //    //}
                    //}
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ex.StackTrace);
            }

            logger.Info("end");
        }

        private string[] GetUrls()
        {
            string urls = JsonConfig.Get("monitorUrls");
            urls = "http://www.sqber.com";
            string[] urlList = urls.Split(new char[] { ';' });
            return urlList;
        }

        [STAThread]
        private void WebBrowserOpen(string url)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var from = new Form();
            var webBrowser = new WebBrowser();
            from.Controls.Add(webBrowser);

            Application.Run(from);
           
            new IEProxy("http://191.102.125.74:8080").RefreshIESettings();
            webBrowser.Navigate(url);
        }
    }

    //http://www.cnblogs.com/wangchuang/category/379245.html
    public class IEProxy
    {
        private const int INTERNET_OPTION_PROXY = 38;
        private const int INTERNET_OPEN_TYPE_PROXY = 3;
        private const int INTERNET_OPEN_TYPE_DIRECT = 1;
        private string ProxyStr;

        [DllImport("wininet.dll", SetLastError = true)]
        private static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int lpdwBufferLength);

        public struct Struct_INTERNET_PROXY_INFO
        {
            public int dwAccessType;
            public IntPtr proxy;
            public IntPtr proxyBypass;
        }

        private bool InternetSetOption(string strProxy)
        {
            int bufferLength;
            IntPtr intptrStruct;
            Struct_INTERNET_PROXY_INFO struct_IPI;
            if (string.IsNullOrEmpty(strProxy) || strProxy.Trim().Length == 0)
            {
                strProxy = string.Empty;
                struct_IPI.dwAccessType = INTERNET_OPEN_TYPE_DIRECT;
            }
            else
            {
                struct_IPI.dwAccessType = INTERNET_OPEN_TYPE_PROXY;
            }

            struct_IPI.proxy = Marshal.StringToHGlobalAnsi(strProxy);
            struct_IPI.proxyBypass = Marshal.StringToHGlobalAnsi("local");
            bufferLength = Marshal.SizeOf(struct_IPI);
            intptrStruct = Marshal.AllocCoTaskMem(bufferLength);
            Marshal.StructureToPtr(struct_IPI, intptrStruct, true);
            return InternetSetOption(IntPtr.Zero, INTERNET_OPTION_PROXY, intptrStruct, bufferLength);
        }

        public IEProxy(string strProxy)
        {
            this.ProxyStr = strProxy;
        }

        //设置代理
        public bool RefreshIESettings()
        {
            return InternetSetOption(this.ProxyStr);
        }

        //取消代理
        public bool DisableIEProxy()
        {
            return InternetSetOption(string.Empty);
        }

    }
}
