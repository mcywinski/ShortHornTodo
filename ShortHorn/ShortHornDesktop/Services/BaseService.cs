using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortHornDesktop.Services
{
    public class BaseService
    {
        protected readonly string apiBaseUrl;

        public BaseService(string apiBaseUrl)
        {
            this.apiBaseUrl = apiBaseUrl;
        }
    }
}
