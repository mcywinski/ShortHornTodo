using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ShortHorn.Desktop
{
    public class ConfigurationManager
    {
        public static string GetApiBaseAddress()
        {
            return ConfigurationSettings.AppSettings["apiBaseAddress"].ToString();
        }
    }
}
