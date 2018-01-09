using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ant.UserView.WinForm.core
{
    public class AppConfigHelper
    {
        public static void saveValue(string Name, string Value)
        {
            ConfigurationManager.AppSettings.Set(Name, Value);
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            config.AppSettings.Settings[Name].Value = Value;
            config.Save(ConfigurationSaveMode.Modified);
            config = null;
        }
    }
}
