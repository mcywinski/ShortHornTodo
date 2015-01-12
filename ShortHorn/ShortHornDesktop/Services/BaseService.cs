using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortHorn.Desktop.Services
{
    public class BaseService
    {
        /// <summary>
        /// The API base URL.
        /// </summary>
        protected readonly string apiBaseUrl;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="apiBaseUrl">The base URL of API.</param>
        public BaseService(string apiBaseUrl)
        {
            this.apiBaseUrl = apiBaseUrl;
        }
    }
}
