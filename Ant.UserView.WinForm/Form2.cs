using Ant.UserView.WinForm.core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ant.UserView.WinForm
{
    public partial class Form2 : Form
    {
        private Timer _timer = new Timer();
        private string[] _proxyList;
        private int index = Convert.ToInt32(ConfigurationManager.AppSettings["index"]);
        string proxyUrlFile = "ProxyUrl";


        public Form2()
        {
            InitializeComponent();

            _proxyList = GetProxyUrl();
            _timer.Interval = 5 * 1000;//5s
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

            ConfigurationManager.AppSettings.Set("index", index.ToString());

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
}
