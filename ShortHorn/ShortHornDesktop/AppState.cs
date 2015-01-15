using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortHorn.Desktop
{
    /// <summary>
    /// Stores current settings regarding user and application status.
    /// </summary>
    public class AppState
    {

        #region public fields

        /// <summary>
        /// The API token for user authorization.
        /// </summary>
        public static string ApiLoginToken { get; set; }

        public static string Country { get; set; }

        public static string City { get; set; }

        #endregion

        /// <summary>
        /// The constructor.
        /// </summary>
        public AppState()
        {

        }
    }
}
