using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;

namespace Law.Monitor.Core
{
    class SendHelper
    {
        public SendResult Send(string url)
        {
            SendResult result = new SendResult();
            try
            {
                HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;

                request.Method = "Get";
                request.ContentType = "text/html,application/xhtml+xml,application/xml,application/json";
                WebResponse response = request.GetResponse();
                HttpWebResponse webResponse = (HttpWebResponse)response;
                result.Code = webResponse.StatusCode;
                if (webResponse.StatusCode == HttpStatusCode.OK)
                {
                    result.Success = true;
                }
            }
            catch (WebException ex)
            {
                HttpWebResponse response = (HttpWebResponse)ex.Response;
                result.Code = response.StatusCode;

                if (response.StatusCode == HttpStatusCode.ServiceUnavailable)
                    result.Success = false;
                else
                    result.Success = true;

                result.Msg = ex.Message;
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
