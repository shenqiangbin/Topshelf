﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ant.UserView.WinForm.core
{
    public class WebProxyFactory
    {
        private SendHelper _sendHelper = new SendHelper();

        public List<string> Get(int page)
        {
            List<string> list = new List<string>();

            string urlFormat = "http://www.66ip.cn/{0}.html";
            urlFormat = "http://www.66ip.cn/areaindex_19/{0}.html";
            SendResult result = _sendHelper.Send(string.Format(urlFormat, page));

            if (result.Success)
            {
                var content = result.Msg;
                NSoup.Nodes.Document htmlDoc = NSoup.NSoupClient.Parse(content);

                var tableEle = htmlDoc.GetElementsByTag("table").Last;

                var trs = tableEle.GetElementsByTag("tr");
                for (int j = 1; j < trs.Count; j++)
                {
                    var tr = trs[j];
                    string ipStr = tr.ChildNodes[0].ToString().Replace("<td>", "").Replace("</td>", "");
                    string portStr = tr.ChildNodes[1].ToString().Replace("<td>", "").Replace("</td>", "");
                    list.Add($"http://{ipStr}:{portStr}");
                }
            }

            //list.Add("http://111.230.113.142:1080");
            list.Add("http://166.111.131.52:3128");

            return list;
        }

        //http://www.goubanjia.com/free/gngn/index.shtml

        public int GetTotalPage()
        {
            int totalPage = 1;
            string urlFormat = "http://www.66ip.cn/{0}.html";
            SendResult result = _sendHelper.Send(string.Format(urlFormat, 1));
            if (result.Success)
            {
                var content = result.Msg;
                NSoup.Nodes.Document htmlDoc = NSoup.NSoupClient.Parse(content);

                var elements = htmlDoc.GetElementById("PageList");
                string pageStr = elements.ChildNodes[elements.ChildNodes.Count - 3].OuterHtml();

                //<a href="/1120.html">1120</a>
                string RegexStr = ">(.+)<";

                if (Regex.IsMatch(pageStr, RegexStr))
                {
                    Match match = Regex.Match(pageStr, RegexStr);
                    totalPage = Convert.ToInt32(match.Value.Replace(">", "").Replace("<", ""));
                }
            }
            return totalPage;
        }
    }
}