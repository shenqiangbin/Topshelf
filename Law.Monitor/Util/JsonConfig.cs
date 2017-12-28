using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf.Logging;

namespace Law.Monitor.Util
{
    public class JsonConfig
    {
        private static LogWriter logger = HostLogger.Get<JsonConfig>();

        private static string JsonPath = AppDomain.CurrentDomain.BaseDirectory + "config.json";
        private static object getLocker = new object();
        private static object addLocker = new object();

        public static string Get(string key)
        {
            lock (getLocker)
            {
                JObject obj = ReadJsonObj(JsonPath);
                JToken token = obj.GetValue(key);
                if (token == null)
                    return "";
                else
                    return token.Value<string>();
            }
        }

        public static void Add(string key, string value)
        {
            lock (addLocker)
            {
                JObject obj = ReadJsonObj(JsonPath);
                if (obj == null)
                    obj = new JObject();

                JToken token = obj.GetValue(key);
                if (token != null)
                {
                    obj.Remove(key);
                }

                obj.Add(new JProperty(key, value));
                //obj.Add(key, JToken.Parse(string.Format("{{'{0}':'{1}'}}", key, value)));

                Write(obj.ToString(), JsonPath);
            }
        }

        /// <summary>
        /// 读取JSON文件
        /// </summary>
        /// <param name="jsonPath">json文件路径</param>
        /// <returns>json字符串</returns>
        private static string ReadJsonString(string jsonPath)
        {
            if (!File.Exists(jsonPath))
            {
                System.IO.File.Create(JsonPath);
            }
            return File.ReadAllText(jsonPath, Encoding.Default);
        }

        /// <summary>
        ///读取JSON文件
        /// </summary>
        /// <param name="jsonPath">json文件路径</param>
        /// <returns>JObject对象</returns>
        private static JObject ReadJsonObj(string jsonPath)
        {
            string json = ReadJsonString(jsonPath);
            JObject jsonObj = null;
            if (!string.IsNullOrEmpty(json))
            {
                jsonObj = (JObject)JsonConvert.DeserializeObject(json);
            }
            return jsonObj;
        }

        /// <summary>
        /// 写入JSON
        /// </summary>
        /// <returns></returns>
        public static bool Write(string jsonStr, string jsonPath)
        {
            try
            {
                System.IO.File.WriteAllText(jsonPath, jsonStr, Encoding.UTF8);
                return true;
            }
            catch (System.Exception ex)
            {
                logger.Error(ex.Message + ex.StackTrace);
                return false;
            }

        }

        /// <summary>
        /// 格式化JSON字符串
        /// </summary>
        /// <param name="str">输入字符串</param>
        /// <returns>输出字符串</returns>
        public static string FormatJsonStr(string str)
        {
            JsonSerializer serializer = new JsonSerializer();
            TextReader tr = new StringReader(str);
            JsonTextReader jtr = new JsonTextReader(tr);
            object obj = serializer.Deserialize(jtr);
            if (obj != null)
            {
                StringWriter textWriter = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' '
                };
                serializer.Serialize(jsonWriter, obj);
                return textWriter.ToString();
            }
            else
            {
                return str;
            }
        }
    }
}
