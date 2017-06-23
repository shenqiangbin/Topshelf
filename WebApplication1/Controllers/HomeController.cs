using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //貌似web的log4net的配置文件得到bin目录的外面
            Base.SqlLogger.Log("mvc test");

            return Content("ok");
        }
    }
}