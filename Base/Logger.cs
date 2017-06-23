using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = @"Log4net.config", Watch = true)]
namespace Base
{

    //注意：MySql.Data.dll、log4net.dll、log4net.config 必须设定为 始终拷贝，当然如果其它有，也可以
    public class Logger
    {
        public static void Log(string msg)
        {
            ILog _log = LogManager.GetLogger("mvclog");
            _log.Error(msg);
        }
    }

    public class SqlLogger
    {
        public static void Log(string msg)
        {
            ILog _log = LogManager.GetLogger("SysLogger");
            _log.Error(msg);
        }

        public static void Log(object msg)
        {
            ILog _log = LogManager.GetLogger("SysLogger");
            _log.Error(msg);
        }

        public static void Log(Exception ex)
        {
            ILog _log = LogManager.GetLogger("SysLogger");
            _log.Error(ex);
        }
    }
}
