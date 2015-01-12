using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ShortHorn.WPApp.Services
{
    public abstract class BaseService
    {
        #region private/protected fields

        /// <summary>
        /// The base url of the service.
        /// </summary>
        protected readonly string serviceBaseUrl;

        #endregion

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="serviceBaseUrl">The base url of the service.</param>
        public BaseService(string serviceBaseUrl)
        {
            this.serviceBaseUrl = serviceBaseUrl;
        }
    }
}
