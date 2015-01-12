using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ShortHorn.Desktop.Services
{
    public class BaseService
    {
        /// <summary>
        /// The API base URL.
        /// </summary>
        protected readonly string apiBaseUrl;

        /// <summary>
        /// The timeout of API in seconds.
        /// </summary>
        protected readonly int apiTimeout = 5;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="apiBaseUrl">The base URL of API.</param>
        public BaseService(string apiBaseUrl)
        {
            this.apiBaseUrl = apiBaseUrl;
        }

        /// <summary>
        /// Resets HTTP client properties to default values for next request.
        /// </summary>
        /// <param name="client"></param>
        protected void resetClient(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(0, 0, 5);
        }
    }
}
