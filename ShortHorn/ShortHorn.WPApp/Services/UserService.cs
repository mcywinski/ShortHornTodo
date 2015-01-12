using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortHorn.WPApp.Services
{
    public class UserService : BaseService
    {
        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="serviceBaseUrl">The base URL of the service.</param>
        public UserService(string serviceBaseUrl)
            : base(serviceBaseUrl)
        {

        }

        public string Login(string login, string password)
        {

            return string.Empty;
        }
    }
}
