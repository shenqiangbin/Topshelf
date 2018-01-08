using Ant.UserView.WinForm.core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ant.UserView.WinForm
{
    public partial class Form1 : Form
    {
        private Timer _timer = new Timer();
        private string[] _proxyList;
        private int index = 1;
        string proxyUrlFile = "ProxyUrl";

        public Form1()
        {
            InitializeComponent();
            _proxyList = GetProxyUrl();
            _timer.Interval = 10 * 1000;//5s
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            if (_proxyList.Count() == 0)
                return;

            if (index == _proxyList.Count())
                index = 1;

            lblInt.Text = $"当前索引：{index}";
            lblIp.Text = $"当前代理服务器：{_proxyList[index]}";

            ConfigurationManager.AppSettings["index"] = index.ToString();

            new IEProxy(_proxyList[index]).RefreshIESettings();
            this.webBrowser1.Navigate("http://www.sqber.com");
            //this.webBrowser2.Navigate("http://www.sqber.com");
            //this.webBrowser3.Navigate("http://www.sqber.com");
            //this.webBrowser4.Navigate("http://www.sqber.com");
            //this.webBrowser5.Navigate("http://www.sqber.com");
            //this.webBrowser6.Navigate("http://www.sqber.com");
            //this.webBrowser7.Navigate("http://www.sqber.com");
            //this.webBrowser8.Navigate("http://www.sqber.com");
            //this.webBrowser9.Navigate("http://www.sqber.com");
            //this.webBrowser10.Navigate("http://www.sqber.com");


            index++;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            //this.Hide();
        }

        private string[] GetProxyUrl()
        {
            if (!File.Exists(proxyUrlFile))
            {
                FileStream stream = File.Create(proxyUrlFile);
                stream.Dispose();
            }

            string content = File.ReadAllText(proxyUrlFile);
            if (string.IsNullOrEmpty(content) || IsNeedRefresh())
            {
                List<string> list = new WebProxyFactory().Get();
                content = string.Join(",", list.ToArray());
                //content = "http://27.38.154.143:9999,http://218.250.205.57:9064,http://118.161.133.2:9064";
                File.WriteAllText(proxyUrlFile, content);
            }

            return content.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        }

        private bool IsNeedRefresh()
        {
            TimeSpan span = DateTime.Now - new FileInfo(proxyUrlFile).LastWriteTime;
            if (span.TotalDays > 7)
                return true;
            else
                return false;
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
