using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ShortHorn.Desktop
{
    /// <summary>
    /// Manages application configuration.
    /// </summary>
    public class ConfigurationManager
    {
        /// <summary>
        /// Reads and returns API root URL from the application configuration file.
        /// </summary>
        /// <returns></returns>
        public static string GetApiBaseAddress()
        {
            return ConfigurationSettings.AppSettings["apiBaseAddress"].ToString();
        }
    }
}
