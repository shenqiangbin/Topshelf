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
        private WebProxyFactory webProxyFactory = new WebProxyFactory();
        private List<string> urls = null;

        string proxyUrlFile = "ProxyUrl";
        private int enableNumber = 0;


        public Form2()
        {
            InitializeComponent();
            this.webBrowser1.Navigate("http://www.baidu.com");
            this.webBrowser1.GoHome();
            this.webBrowser1.Navigated += WebBrowser1_Navigated;

            webProxyFactory.GetFrom_Goubanjia(1);
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            if (!chkEnableProxy.Checked)
            {
                this.webBrowser1.Navigate("http://www.baidu.com");
            }
            else
            {
                if (urls == null || urls.Count == 0)
                {
                    int i = Convert.ToInt32(ConfigurationManager.AppSettings["index"]);
                    //int totalPage = webProxyFactory.GetTotalPage();
                    int totalPage = 1121;

                    if (i == totalPage)
                        i = 1;

                    ShowStatus("获取代理服务器列表...");
                    urls = webProxyFactory.Get(i);
                    ShowStatus("代理服务器列表完成...");

                    lblInt.Text = $"当前页：{i}";

                    i++;

                    AppConfigHelper.saveValue("index", i.ToString());
                }

                lblProxyUrls.Text = string.Join("\r\n", urls.ToArray());

                var url = urls.First();
                urls.RemoveAt(0);

                ShowStatus($"{url}代理可用性验证中...");
                SendResult result = new SendHelper().Send("http://www.baidu.com", new WebProxyItem { Url = url });
                if (result.Success && result.Msg.Contains("domain:\"http://www.baidu.com\""))
                {
                    ShowStatus($"{url} 代理可用");
                    enableNumber++;
                    lblEnableNum.Text = enableNumber.ToString();

                    try
                    {
                        File.AppendAllText(proxyUrlFile, url + ",");
                        HandThePage(url);
                    }
                    catch (Exception ex)
                    {
                        ShowStatus(ex.Message);
                    }
                }
                else
                {
                    ShowStatus($"{url} 代理不可用");
                }
            }
        }

        private void WebBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (e.Url.ToString() == "https://www.baidu.com/")
            {
                ShowStatus($"访问{txtUrl.Text}中");
                this.webBrowser1.Navigate(txtUrl.Text);
            }

        }

        private void ShowStatus(string msg)
        {
            lblStatus.Text = msg;
        }

        private void HandThePage(string url)
        {


            lblIp.Text = $"当前代理服务器：{url}";
            new IEProxy(url).RefreshIESettings();

            ShowStatus("访问百度页面中");
            this.webBrowser1.Navigate("http://www.baidu.com");

        }

        private void btnSwitch_Click(object sender, EventArgs e)
        {
            if (btnSwitch.Text == "开始")
            {
                int interval = 5;
                if (!int.TryParse(txtInterval.Text, out interval))
                {
                    interval = 5;
                    txtInterval.Text = "5";
                }

                _timer.Interval = interval * 1000;//5s
                _timer.Tick += _timer_Tick;
                _timer.Start();
                btnSwitch.Text = "结束";
            }
            else
            {
                _timer.Stop();
                btnSwitch.Text = "开始";
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }

    //private bool IsNeedRefresh()
    //{
    //    TimeSpan span = DateTime.Now - new FileInfo(proxyUrlFile).LastWriteTime;
    //    if (span.TotalDays > 7)
    //        return true;
    //    else
    //        return false;
    //}
}

