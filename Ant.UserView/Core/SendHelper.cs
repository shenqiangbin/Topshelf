using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;

namespace Ant.UserView.Core
{
    class SendHelper
    {
        public SendResult Send(string url, WebProxyItem webProxy = null)
        {
            SendResult result = new SendResult();
            WebResponse response = null;
            try
            {
                HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;

                request.Method = "Get";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36";
                request.ContentType = "text/html,application/xhtml+xml,application/xml,application/json";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
                request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
                request.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.9");
                request.Headers.Add(HttpRequestHeader.Cookie, "Hm_lvt_ef18c725ee26ac659f672367d8d541ee=1514188431,1514442064,1514940225,1515385421; __RequestVerificationToken=505QE0wRDCRLRyf4ued8iI6KktTGlN_IwgRVdMf-2mt2MoSY8eKm7WykEgbwWU9HSsj4jCZKrdxGnJJnC7eprZbtG8xJSwmtZ7aAqLm2m0o1; ASP.NET_SessionId=2xs1vepcqxmd1jkg3jauq5xa; aid=8548772D1AE124872574911C1BBE0F6E122FB7CE66151F05DFBBDBDD12EA16D5D5F7F2DCE6D4C2D4CF9821151C1DF030C3CC6F514995303814D1F3B7AAA29D358D29B46CB4F9B161048E4AB08620FFA74A6CE3E149160DDDA7D19E8720C0AF64E53822C3ACF7B54C2E7CDC5694E49E7015503FB4E3CB39CACFD75B70DAE4E4682F8848B11D2A0B1D19312B8CE886D073; Hm_lpvt_ef18c725ee26ac659f672367d8d541ee=1515385919");
                request.Headers.Add(HttpRequestHeader.Upgrade, "1");
                request.Headers.Add(HttpRequestHeader.Pragma, "no-cache");
               // request.Headers.Add(HttpRequestHeader.Connection, "keep-alive");                

                request.Timeout = 1000 * 8; //8s

                if (webProxy != null)
                    request.Proxy = webProxy.ToWebProxy();

                response = request.GetResponse();
                HttpWebResponse webResponse = (HttpWebResponse)response;
                result.Code = webResponse.StatusCode;
                if (webResponse.StatusCode == HttpStatusCode.OK)
                {
                    result.Success = true;
                }
                else
                {
                    result.Success = false;
                }
            }
            catch (WebException ex)
            {
                HttpWebResponse webResponse = (HttpWebResponse)ex.Response;
                if (webResponse == null)
                {
                    result.Success = false;
                }
                else
                {
                    result.Code = webResponse.StatusCode;

                    if (webResponse.StatusCode == HttpStatusCode.ServiceUnavailable)
                        result.Success = false;
                    else
                        result.Success = true;
                }

                result.Msg = ex.Status + ex.Message;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                    response.Dispose();
                }
            }

            return result;
        }

        public static bool TestNetConnectity(string strIP)
        {
            //if (!NetUtil.CheckIPAddr(strIP))
            //{
            //    return false;
            //}
            // Windows L2TP VPN和非Windows VPN使用ping VPN服务端的方式获取是否可以连通  
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();

            // Use the default Ttl value which is 128,  
            // but change the fragmentation behavior.  
            options.DontFragment = true;

            // Create a buffer of 32 bytes of data to be transmitted.  
            string data = "testtesttesttesttesttesttesttest";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;
            PingReply reply = pingSender.Send(strIP, timeout, buffer, options);

            return (reply.Status == IPStatus.Success);
        }
    }

    class SendResult
    {
        public bool Success { get; set; }
        public HttpStatusCode Code { get; set; }
        public string Msg { get; set; }
    }
}
