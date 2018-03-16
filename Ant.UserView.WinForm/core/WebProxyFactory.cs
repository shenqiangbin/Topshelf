using NSoup.Nodes;
using System;
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
                    //list.Add($"http://{ipStr}:{portStr}");
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

        public List<string> GetFrom_Goubanjia(int page)
        {
            List<string> list = new List<string>();

            string urlFormat = "http://www.goubanjia.com/free/gngn/index{0}.shtml";

            string url = string.Format(urlFormat, page);
            if (page == 1)
                url = "http://www.goubanjia.com/free/gngn/index.shtml";

            SendResult result = _sendHelper.Send(url);

            if (result.Success)
            {
                var content = result.Msg;
                NSoup.Nodes.Document htmlDoc = NSoup.NSoupClient.Parse(content);

                var tableEle = htmlDoc.GetElementsByTag("table").Last;

                var trs = tableEle.GetElementsByTag("tr");
                for (int j = 1; j < trs.Count; j++)
                {
                    var tr = trs[j];
                    var td = tr.ChildNodes[1];
                    var ipStr = GetIP(td);

                    //list.Add($"http://{ipStr}:{portStr}");
                }
            }

            return list;
        }

        private string GetIP(Node td)
        {
            List<string> list = new List<string>();
            foreach (Node item in td.ChildNodes)
            {
                if (item.Attributes["style"].Contains("none"))
                    continue;

                var str = item.OuterHtml().Replace("\n", "");

                if (str.Trim() == ":")
                {
                    list.Add(str);
                    continue;
                }

                string RegexStr = ">(.+)<";

                //string tmpStr = string.Format("<{0}[^>]*?>(?<Text>[^<]*)</{1}>", title, title); //获取<title>之间内容  

                //Match TitleMatch = Regex.Match(str, tmpStr, RegexOptions.IgnoreCase);

                //string result = TitleMatch.Groups["Text"].Value;
                //return result;

                if (Regex.IsMatch(str, RegexStr))
                {
                    Match match = Regex.Match(str, RegexStr);
                    str = match.Value.Replace(">", "").Replace("<", "");
                    if (!string.IsNullOrEmpty(str))
                        list.Add(str.Trim());
                }
            }
            return string.Join("", list.ToArray());

        }

        public string NoHTML(string Htmlstring)
        {
            MatchCollection mcImg = Regex.Matches(Htmlstring, @"<(\bimg\b)[^>]*>");
            List<string> imgList = new List<string>();
            for (int i = 0; i < mcImg.Count; i++)
            {
                imgList.Add(mcImg[i].Value);
            }
            Htmlstring = Regex.Replace(Htmlstring, @"<(\bimg\b)[^>]*>", "imgHtmlString", RegexOptions.IgnoreCase);

            MatchCollection mctable = Regex.Matches(Htmlstring, @"<table[\s\S]*?>[\s\S]*?<\/table>");
            List<string> tableList = new List<string>();
            for (int i = 0; i < mctable.Count; i++)
            {
                tableList.Add(mctable[i].Value);
            }
            Htmlstring = Regex.Replace(Htmlstring, @"<table[\s\S]*?>[\s\S]*?<\/table>", "tableHtmlString", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"<title>[\s\S]*</title>|<[^>]*>", "", RegexOptions.IgnoreCase);
            //Htmlstring = Regex.Replace(Htmlstring, @"<(?!\bimg\b)[^>]*>", "", RegexOptions.IgnoreCase);
            //换行替换br
            Htmlstring = Regex.Replace(Htmlstring, @"[\r\n]+", "<br/>", RegexOptions.IgnoreCase);
            //将img标签内的br 去掉
            //Htmlstring = Regex.Replace(Htmlstring, @"\b<*<br/>*>\b", " ", RegexOptions.IgnoreCase);

            //Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            //Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);



            Regex r = new Regex("tableHtmlString");
            for (int i = 0; i < tableList.Count; i++)
            {
                Htmlstring = r.Replace(Htmlstring, tableList[i], 1, 0);
            }
            Regex r1 = new Regex("imgHtmlString");
            for (int i = 0; i < imgList.Count; i++)
            {
                Htmlstring = r1.Replace(Htmlstring, imgList[i], 1, 0);
            }
            //双引号换成单引号
            Htmlstring = Regex.Replace(Htmlstring, "\"", "\'", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"src='", "src='/html/", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"<br/>src", "src", RegexOptions.IgnoreCase);

            return Htmlstring;
        }
    }
}
